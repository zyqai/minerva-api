﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Minerva.Controllers
{
    [Route("business")]
    [ApiController]
    // [Authorize]
    public class BusinessController : ControllerBase
    {
        IBusinessBL BusinessBL;
        public BusinessController(IBusinessBL _BusinessBL)
        {
            this.BusinessBL = _BusinessBL;
        }

        [HttpPost]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> SaveBusines([FromBody] BusinessRequest request)
        {
            try
            {
                request.CreatedBy = User.FindFirstValue(ClaimTypes.Email);
                Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
                if (ModelState.IsValid)
                {
                    int? b = await BusinessBL.SaveBusines(request);
                    if (b > 0)
                    {
                        Business? res = await BusinessBL.GetBusiness((int)b);
                        Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(res));
                        return StatusCode(StatusCodes.Status201Created, res);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, request);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "StaffPolicy")]
        public Task<Business?> GetBusiness(int id)
        {
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, id.ToString());
            return BusinessBL.GetBusiness(id);
        }

        [HttpGet]
        [Authorize(Policy = "StaffPolicy")]
        public Task<List<Business?>> GetBusiness()
        {
            return BusinessBL.GetALLBusiness();
        }

        [HttpPut]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> UpdateBusiness(BusinessRequest request)
        {
            request.UpdatedBy = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    bool ?b = await BusinessBL.UpdateBusiness(request);
                    return StatusCode(StatusCodes.Status200OK, request);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBusiness(int id)
        {
            try
            {
                if (id > 0)
                {
                    Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, id + "delete By " + User.FindFirstValue(ClaimTypes.Email));
                    bool b = BusinessBL.DeleteBusiness(id);
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, id + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
