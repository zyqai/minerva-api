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
        [Route("/Business")]
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

        [HttpGet]
        [Route("/Business/{id}")]
        public Task<Business?> GetBusiness(int id)
        {
            return BusinessBL.GetBusiness(id);
        }
        [HttpGet]
        [Route("/Business")]
        public Task<List<Business?>> GetBusiness()
        {
            return BusinessBL.GetALLBusiness();
        }
        [HttpPut]
        [Route("/Business")]
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
        [HttpDelete]
        [Route("/Business/{BusinessId}")]
        public IActionResult DeleteUser(int BusinessId)
        {
            try
            {
                if (BusinessId > 0)
                {
                    bool b = BusinessBL.DeleteBusiness(BusinessId);
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
