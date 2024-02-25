using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Responce;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateTenent(TenantRequest request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            request.CreatedBY = email;
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
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
                Comman.logError(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTenant(TenantRequest request)
        {
            request.UpdatedBY = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
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
                Comman.logError(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
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
            TenantBusiness res = new TenantBusiness();
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
            PeopleBusiness peopleBusiness = new PeopleBusiness();
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
            TenantUsers tenantUsers = new TenantUsers();
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

        [HttpGet("projectByTenant/{tenantId}")]
        public async Task<IActionResult> ProjectByTenant(int tenantId)
        {
            TenantProject res = new TenantProject();
            res = await tenant.ProjectByTenant(tenantId);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        [HttpGet("personasByTenant/{tenantId}")]
        public async Task<IActionResult> PersonasByTenant(int tenantId)
        {
            TenentPersonas res = new TenentPersonas();
            res = await tenant.PersonasByTenant(tenantId);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
    }
}
