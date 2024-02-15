﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MinervaApi.Controllers
{
    [Route("client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        IClientBL client;
        public ClientsController(IClientBL bL)
        {
            client = bL;
        }
        [HttpGet("{clientId}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public Task<Client?> GetClient(int clientid)
        {
            return client.GetClient(clientid);
        }
        

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public Task<List<Client?>> GetClient()
        {
            return client.GetALLClients();
        }
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        public async Task<IActionResult> SaveClinet(ClientRequest c)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    string? email = User.FindFirstValue(ClaimTypes.Email);
                    c.CreatedBy = email;
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateClient(ClientRequest c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string? email = User.FindFirstValue(ClaimTypes.Email);
                    c.ModifiedBy = email;
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{clientId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteClient(int clientid)
        {
            try
            {
                if (clientid > 0)
                {
                    bool b = await client.DeleteClient(clientid);
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
