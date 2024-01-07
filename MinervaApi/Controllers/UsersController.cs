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

                    bool b = await userBL.SaveUser(user);
                    if (b)
                        return StatusCode(StatusCodes.Status201Created, user);
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
    }
}
