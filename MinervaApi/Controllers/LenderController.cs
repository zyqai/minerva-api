using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;
using MinervaApi.Models.Requests;
using Minerva.BusinessLayer.Interface;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.Models;
using Minerva.BusinessLayer;
using Minerva.Models.Returns;

namespace MinervaApi.Controllers
{
    [Route("lender")]
    [ApiController]
    public class LenderController : ControllerBase
    {
        ILenderBL lender;
        public LenderController(ILenderBL _Lender)
        {
            lender = _Lender;
        }
        [HttpPost]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> CreateLender(LenderRequest request)
        {
           string userid = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await lender.SaveLender(request, userid);
                    if (b > 0)
                    {
                        Lender? len = await lender.GetLender(b);
                        if (len != null)
                        {
                            return StatusCode(StatusCodes.Status201Created, len);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, len);
                        }
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

        [Authorize(Policy = "StaffPolicy")]
        [HttpGet("{id}")]
        public Task<Lender?> GetLender(int id)
        {
            return lender.GetLender(id);
        }

        [HttpGet]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<List<Lender?>> GetLenders()
        {
            return await lender.GetALLLenders();
        }

        [HttpPut]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> UpdateLender(LenderRequest c)
        {
            Apistatus status = new Apistatus();
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c));
            try
            {
                if (ModelState.IsValid)
                {
                    status = await lender.UpdateLender(c);
                    return StatusCode(StatusCodes.Status200OK, status);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Policy = "TenantAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLender(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await lender.DeleteLender(id);
                    return StatusCode(StatusCodes.Status202Accepted,b);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, " error "+ id.ToString() + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
