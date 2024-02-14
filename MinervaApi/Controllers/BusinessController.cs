using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;

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
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public IActionResult SaveBusines([FromBody] BusinessRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int b = BusinessBL.SaveBusines(request);
                    if (b>0)
                    {
                        Business ?res = await BusinessBL.GetBusiness(b);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public Task<Business?> GetBusiness(int id)
        {
            return BusinessBL.GetBusiness(id);
        }
        
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public Task<List<Business?>> GetBusiness()
        {
            return BusinessBL.GetALLBusiness();
        }
        
        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UpdateBusiness(BusinessRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = BusinessBL.UpdateBusiness(request);
                    return StatusCode(StatusCodes.Status200OK, request);
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
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
