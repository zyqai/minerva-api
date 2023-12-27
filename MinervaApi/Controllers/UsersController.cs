using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Controllers;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserBL userBL;
        public UsersController(IUserBL _userBL)
        {
            this.userBL = _userBL;
        }

        [HttpPost]
        [Route("/GetUser")]
        public Task<User?> GetUser([FromBody] UsersRequest user)
        {
            return userBL.GetUser(user);
        }
        [HttpPost]
        [Route("/GetUsers")]
        public Task<List<User?>> GetUses()
        {

            return userBL.GetALLUsers();

        }
        [HttpPost]
        [Route("/PostUsers")]
        public IActionResult SaveUsers(UsersRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool b = userBL.SaveUser(user);
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
        [HttpPost]
        [Route("/PutUser")]
        public IActionResult UpdateUser(UsersRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = userBL.UpdateUser(user);
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

    }
}
