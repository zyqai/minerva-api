using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Responce;

namespace MinervaApi.Controllers
{
    [Route("tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        ITenant tenant;
        public TenantController(ITenant _tenant)
        {
            tenant = _tenant;
        }
        
        [HttpPost]
        //[Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> CreateTenent(TenantRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await tenant.SaveTenant(request);
                    if (b > 0)
                    {
                        Tenant? ten = await tenant.GetTenantAsync(b);
                        if (ten != null)
                        {
                            return StatusCode(StatusCodes.Status201Created, ten);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, ten);
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
        //[Authorize(Policy = "TenantAdminPolicy")]
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
        //[Authorize(Policy = "TenantAdminPolicy")]
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

        [HttpGet("businessesByTenant/{tenantId}")]
        public async Task<IActionResult> BusinessesForTenant(int tenantId)
        {
            TenantBusiness res=new TenantBusiness ();
            res = await tenant.BusinessesForTenant(tenantId);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("peoplesByTenant/{tenantId}")]
        public async Task<IActionResult> PeoplesForTenant(int tenantId)
        {
            PeopleBusiness peopleBusiness = new PeopleBusiness ();
            peopleBusiness = await tenant.PeoplesForTenant(tenantId);
            if (peopleBusiness != null)
            {
                return Ok(peopleBusiness);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("usersByTenant/{tenantId}")]
        public async Task<IActionResult> UsersForTenant(int tenantId)
        {
            TenantUsers tenantUsers=new TenantUsers ();
            tenantUsers = await tenant.UsersForTenant(tenantId);
            if (tenantUsers != null)
            {
                return Ok(tenantUsers);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
    }
}
