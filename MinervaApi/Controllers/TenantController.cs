using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;

namespace MinervaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        ITenant tenant;
        public TenantController(ITenant _tenant)
        {
            tenant = _tenant;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(TenantRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await tenant.SaveTenant(request);
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
        public async Task<IActionResult> Get()
        {
            var _tenants = await tenant.GetALLAsync();

            if (_tenants != null)
            {
                return Ok(_tenants);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ten = await tenant.GetTenantAsync(id);

            if (ten != null)
            {
                return Ok(ten);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject(TenantRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await tenant.UpdateTenant(request);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await tenant.DeleteTenant(id);
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
