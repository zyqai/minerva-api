using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;
using Minerva.Models.Returns;

namespace Minerva.Controllers
{
    [Route("requesttemplate")]
    [ApiController]
    public class RequestTemplateController : Controller
    {

        IRequestTemplateBL requestTemplateBL;
        public RequestTemplateController(IRequestTemplateBL RequestTemplateBL)
        {
            requestTemplateBL = RequestTemplateBL;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var RequestTemplate = await requestTemplateBL.GetALLRequestTemplates();

            if (RequestTemplate != null)
            {
                return Ok(RequestTemplate);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var RequestTemplate = await requestTemplateBL.GetRequestTemplate(id);

            if (RequestTemplate != null)
            {
                return Ok(RequestTemplate);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


        [HttpPost]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> SaveRequestTemplate(RequestTemplateRequestWhithDetails request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    Apistatus b = await requestTemplateBL.SaveRequestTemplate(request,email);
                    if (b?.code== "200")
                    {
                        return StatusCode(StatusCodes.Status201Created, b);
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

        
        [Authorize(Policy = "StaffPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateRequestTemplate(RequestTemplateRequest request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    request.email = email;

                    var b = await requestTemplateBL.UpdateRequestTemplates(request);
                    if (b)
                    {
                        return StatusCode(StatusCodes.Status201Created, request);
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

        [Authorize(Policy = "StaffPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestTemplate(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await requestTemplateBL.DeleteRequestTemplate(id);
                    if (b)
                    {
                        return StatusCode(StatusCodes.Status201Created);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
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

    }
}
