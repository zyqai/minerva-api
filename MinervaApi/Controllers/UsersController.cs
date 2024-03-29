﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Controllers;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

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

        [HttpGet("{id}")]
        public Task<User?> GetUser(string id)
        {
            UsersRequest user = new UsersRequest
            {
                UserId = id
            };
            return userBL.GetUser(user);
        }
        [HttpGet]
        [Authorize(Policy = "StaffPolicy")]
        //[Authorize]
        public Task<List<User?>> GetUses()
        {

            return userBL.GetALLUsers();

        }
        
        [HttpPost]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> SaveUsers(UsersRequest user)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            user.CreatedBy = email;
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(user));
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
                        var res1 = userBL.verifyemail(user.Email);
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(user) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPut]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> UpdateUser(UsersRequest user)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            user.ModifiedBy = email;
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(user));

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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(user) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpDelete("{userId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    bool b =await userBL.DeleteUser(userId);
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

        [HttpGet("getUser/{userName}")]
        public Task<User?> GetUserFromEmail(string userName)
        {
            UsersRequest user = new UsersRequest
            {
                UserName = userName
            };
            return userBL.GetUserusingUserName(user);
        }

        //[HttpGet("getTenantUserList/{tenantId}")]
        //public Task<List<User?>> GetTenantUserList(int tenantId)
        //{
        //    return userBL.GetTenantUserList(tenantId);
        //}
        [HttpGet("forgotPassword/{emailId}")]
        [Authorize(Policy = "StaffPolicy")]
        public Task<APIStatus> Forgetpassword(string emailId) 
        {
            return userBL.Forgetpassword(emailId);
        }
        [HttpGet("verifyEmail/{emailId}")]
        [Authorize(Policy = "StaffPolicy")]
        public Task<APIStatus> verifyemail(string emailId)
        {
            return userBL.verifyemail(emailId);
        }
    }
}
