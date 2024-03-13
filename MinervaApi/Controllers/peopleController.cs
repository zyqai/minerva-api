using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;

namespace MinervaApi.Controllers
{
    [Route("people")]
    [ApiController]
    public class peopleController : ControllerBase
    {
        IClientBL client;
        public peopleController(IClientBL bL)
        {
            client = bL;
        }


        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        [HttpGet("{id}")]
        public Task<Client?> GetClient(int id)
        {
            return client.GetClient(id);
        }
        

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public Task<List<Client?>> GetClient()
        {
            return client.GetALLClients();
        }
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> SaveClinet(ClientRequest c)
        {
            c.CreatedBy = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c));
            try
            {
                if (ModelState.IsValid)
                {
                    int ClientId = await client.SaveClient(c);
                        if (ClientId > 0)
                        {
                            Client ?clients= await client.GetClient(ClientId);
                            List<Client?> clist = new List<Client?>();
                            clist.Add(clients);
                            return StatusCode(StatusCodes.Status201Created,clist);

                        }
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, c);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> UpdateClient(ClientRequest c)
        {
            c.ModifiedBy = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c));
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = await client.UpdateClient(c);
                    return StatusCode(StatusCodes.Status200OK, c);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(c) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool b = await client.DeleteClient(id);
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
