using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Controllers;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserBL userBL;
        public UsersController(IUserBL _userBL)
        {
            this.userBL = _userBL;
        }

        [HttpGet("{UserId}")]
        public Task<User?> GetUser(string UserId)
        {
            UsersRequest user = new UsersRequest
            {
                UserId = UserId
            };
            return userBL.GetUser(user);
        }
        [HttpGet]
        [Authorize(Policy= "TenantAdmin")]
        //[Authorize]
        public Task<List<User?>> GetUses()
        {

            return userBL.GetALLUsers();

        }
        
        [HttpPost]
        public async Task<IActionResult> SaveUsers(UsersRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserName = user.Email;
                    user.IsDeleted = false;
                    user.IsActive = true;

                    string userid = await userBL.SaveUser(user);
                    if (!string.IsNullOrEmpty(userid))
                    {
                        user.UserId = userid;
                        User? user1 = await userBL.GetUser(user);
                        List<User?> ulist = new List<User?>();
                        ulist.Add(user1);
                        return StatusCode(StatusCodes.Status201Created, ulist);
                    }
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UsersRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserName = user.Email;
                    bool b =await userBL.UpdateUser(user);
                    return StatusCode(StatusCodes.Status200OK, user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(string UserID)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    bool b =await userBL.DeleteUser(UserID);
                    if (b)
                        return StatusCode(StatusCodes.Status200OK);
                    else
                        return StatusCode(StatusCodes.Status204NoContent);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getuser/{UserName}")]
        public Task<User?> GetUserFromEmail(string UserName)
        {
            UsersRequest user = new UsersRequest
            {
                UserName = UserName
            };
            return userBL.GetUserusingUserName(user);
        }

        [HttpGet("resetpassword/emailid")]
        public Task<IActionResult> ResetPassword(string emailid) 
        {
            return userBL.ResetPassword(emailid);
        }
    }
}
