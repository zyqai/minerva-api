using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;

namespace MinervaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        IClientBL client;
        public ClientsController(IClientBL bL)
        { 
            client = bL;
        }
        [HttpGet]
        [Route("/Client/{ClientId}")]
        public Task<Client?> GetUser(int ClientId)
        {
            return client.GetClient(ClientId);
        }
        [HttpGet]
        [Route("/Client")]
        public Task<List<Client?>> GetClient()
        {
            return client.GetALLClients();
        }
        [HttpPost]
        [Route("/Client")]
        public async Task<IActionResult> SaveClinet(ClientRequest c)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool b = await client.SaveClient(c);
                    if (b)
                        return StatusCode(StatusCodes.Status201Created, c);
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
        [Route("/Client")]
        public async Task<IActionResult> UpdateClient(ClientRequest c)
        {
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("/Client/{ClientId}")]
        public async Task<IActionResult> DeleteClient(int ClientId)
        {
            try
            {
                if (ClientId>0)
                {
                    bool b = await client.DeleteClient(ClientId);
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
