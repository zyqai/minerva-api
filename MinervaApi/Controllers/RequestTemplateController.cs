using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

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

        [HttpPost]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateRequestTemplate(RequestTemplateRequest request)
        {
            
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    int b = await requestTemplateBL.SaveRequestTemplate(request);
                    if (b > 1)
                    {
                        RequestTemplate? p = await requestTemplateBL.GetRequestTemplate(b);
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

        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateRequestTemplate(RequestTemplateRequest request)
        {
            
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
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

        [Authorize(Policy = "TenantAdminPolicy")]
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
