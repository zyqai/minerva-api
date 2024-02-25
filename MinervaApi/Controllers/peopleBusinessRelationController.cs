using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;
using MinervaApi.Models.Requests;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Minerva.Controllers
{
    [Route("peopleBusinessRelation")]
    [ApiController]
    public class peopleBusinessRelationController : ControllerBase
    {
        ICBRelation relation;
        IUserBL user;
        public peopleBusinessRelationController(ICBRelation cBRelation, IUserBL _user)
        {
            relation = cBRelation;
            user = _user;
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            Comman.logEvent("peopleBusinessRelationGet", id.ToString());
            try
            {
                var ten = await relation.GetAync(id);
                if (ten != null)
                {
                    Comman.logRes("peopleBusinessRelationGet", JsonConvert.SerializeObject(ten));
                    return Ok(ten);
                }
                else
                {
                    return NoContent(); // or another appropriate status
                }
            }
            catch (Exception ex)
            {
                Comman.logError("peopleBusinessRelationGet", ex.Message.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> Create(List<CBRelationRequest?> requests)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            User? u = new User();
            Comman.logRes("peopleBusinessRelationGet", JsonConvert.SerializeObject(requests));

            if (email != null)
            {
                u = await user.GetUserusingUserName(email);
            }

            List<CBRelation?> resList = new List<CBRelation?>();
            try
            {
                foreach (var request in requests)
                {
                    request.tenantId = u.TenantId;
                    if (ModelState.IsValid)
                    {
                        var b = await relation.Save(request);

                        //if (b > 0)
                        //{
                        //    CBRelation? res = await relation.GetAync(b);
                        //    if (res != null)
                        //    {
                        //        resList.Add(res);
                        //    }
                        //}
                    }
                }
                if (resList != null)
                    return StatusCode(StatusCodes.Status201Created, resList);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> Update(List<CBRelationRequest> requests)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = false;
                    foreach (var request in requests)
                    {
                        b = await relation.Update(request);
                    }

                    if (b)
                    {
                        return StatusCode(StatusCodes.Status205ResetContent, requests);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, requests);
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
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await relation.Delete(id);
                    if (b)
                    {
                        return StatusCode(StatusCodes.Status202Accepted);
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

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> Get()
        {
            var ten = await relation.GetALLAync();

            if (ten != null)
            {
                return Ok(ten);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("businessRelation/{businessId}")]
        public async Task<IActionResult> GetBusinessRelation(int ?businessId)
        {
            var BusinessRelationList = await relation.GetBusinessRelationList(businessId);
            if (BusinessRelationList != null)
            {
                return Ok(BusinessRelationList);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("clientsRelation/{clientId}")]
        public async Task<IActionResult> GetClientsRelation(int? clientId)
        {
            var BusinessRelationList = await relation.GetClientRelationList(clientId);
            if (BusinessRelationList != null)
            {
                return Ok(BusinessRelationList);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
