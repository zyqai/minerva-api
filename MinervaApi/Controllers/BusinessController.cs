using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        IBusinessBL BusinessBL;
        public BusinessController(IBusinessBL _BusinessBL)
        {
            this.BusinessBL = _BusinessBL;
        }
        [HttpPost]
        [Route("/PostBusiness")]
        public IActionResult SaveBusines([FromBody] BusinessRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = BusinessBL.SaveBusines(request);
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
    }
}
