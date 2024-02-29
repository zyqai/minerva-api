using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("requesttemplatedetails")]
    [ApiController]
    public class RequestTemplateDetailsController : Controller
    {

        IRequestTemplateDetailsBL _requestTemplateDetailsBL;
        public RequestTemplateDetailsController(IRequestTemplateDetailsBL requestTemplateDetailsBL)
        {
            _requestTemplateDetailsBL = requestTemplateDetailsBL;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var RequestTemplateDetailss = await _requestTemplateDetailsBL.GetALLRequestTemplateDetails();

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
            var RequestTemplateDetails = await _requestTemplateDetailsBL.GetRequestTemplateDetails(id);

            if (RequestTemplateDetails != null)
            {
                return Ok(RequestTemplateDetails);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }       
    }
}
