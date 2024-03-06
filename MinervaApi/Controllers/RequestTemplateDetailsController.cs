using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Minerva.Controllers
{
    [Route("requesttemplatedetails")]
    [ApiController]
    public class RequestTemplateDetailsController : Controller
    {

        IRequestTemplateDetailsBL _rtdBL;
        public RequestTemplateDetailsController(IRequestTemplateDetailsBL requestTemplateDetailsBL)
        {
            _rtdBL = requestTemplateDetailsBL;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var RequestTemplateDetailss = await _rtdBL.GetALLRequestTemplateDetails();

            if (RequestTemplateDetailss != null)
            {
                return Ok(RequestTemplateDetailss);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        //[Authorize(Policy = "TenantAdminPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var RequestTemplateDetails = await _rtdBL.GetRequestTemplateDetails(id);

            if (RequestTemplateDetails != null)
            {
                return Ok(RequestTemplateDetails);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpPost]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> SaveRequestTemplateDetail(RequestTemplateDetailsRequest request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    request.email = email;

                    int b = await _rtdBL.SaveRequestTemplateDetails(request);
                    if (b > 1)
                    {
                        RequestTemplateDetails? p = await _rtdBL.GetRequestTemplateDetails(b);
                        return StatusCode(StatusCodes.Status201Created, p);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateRequestTemplateDetail(RequestTemplateDetailsRequest request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    request.email = email;

                    bool? b = await _rtdBL.UpdateRequestTemplateDetails(request);
                    return StatusCode(StatusCodes.Status200OK, request);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteRequestTemplateDetail(int id)
        {
            try
            {
                if (id > 0)
                {
                    Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, id + "delete By " + User.FindFirstValue(ClaimTypes.Email));
                    bool? b = await _rtdBL.DeleteRequestTemplateDetails(id);
                    if (b == true)
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
                Comman.logError(System.Reflection.MethodBase.GetCurrentMethod().Name, id + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




    }
}
