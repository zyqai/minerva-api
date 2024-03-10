using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MinervaApi.Controllers
{
    [Route("master")]
    [ApiController]
    public class masterController : ControllerBase
    {
        IMasterBL bl;
        public masterController(IMasterBL _masterBL)
        {
            bl = _masterBL;
        }
        [HttpGet("getIndustrys")]
        public async Task<IActionResult> Getindustrys()
        {
            var res = await bl.GetindustryAsync();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("getLoanTypes")]
        public async Task<IActionResult> GetloanTypes()
        {
            var res = await bl.GetloanTypesAsync();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("getStatues")]
        public async Task<IActionResult> getStatues()
        {
            var res = await bl.getStatues();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("getProjectRequestTemplateStatus/{projectRequeststatus}")]
        public async Task<IActionResult> getStatues(int projectRequeststatus)
        {
            var res = await bl.getStatues(projectRequeststatus);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }




        [HttpPost("CreateNotes")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateNotes(Notes request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    request.createdByUserId = email;
                    var b = await bl.SaveNotes(request);
                    if (b != null)
                    {
                        return StatusCode(StatusCodes.Status201Created, b);
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

    }
}
