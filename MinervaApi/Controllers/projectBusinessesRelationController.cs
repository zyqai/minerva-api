using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.Models.Returns;
using Minerva.Models;
using MinervaApi.Models.Requests;
using System.Security.Claims;
using Minerva.BusinessLayer.Interface;
using MinervaApi.BusinessLayer.Interface;

namespace MinervaApi.Controllers
{
    [Route("projectBusinessesRelation")]
    [ApiController]
    public class projectBusinessesRelationController : ControllerBase
    {
        IUserBL user;
        IprojectBusinessesRelation ipbr;
        public projectBusinessesRelationController(IUserBL _user, IprojectBusinessesRelation _ipbr) 
        {
            user = _user;
            ipbr = _ipbr;
        }
        [HttpPost]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        //[Authorize(Policy = "Staff")]
        public async Task<IActionResult> Createppr(List<projectBusinessesRelationRequest?> requests)
        {
            Apistatus res = new Apistatus();
            string? email = User.FindFirstValue(ClaimTypes.Email);
            User? u = new User();
            if (email != null)
            {
                u = await user.GetUserusingUserName(email);
            }
            try
            {
                foreach (var request in requests)
                {
                    request.tenantId = u?.TenantId;
                    int req = await ipbr.Create(request);
                    if (req >= 1)
                    {
                        res.code = "201"; res.message = "Project businesses relation created sussfully";
                    }
                    else
                    {
                        res.code = "300"; res.message = "Project businesses relation not created!";
                    }
                }
                if (res.code == "201")
                    return StatusCode(StatusCodes.Status201Created, res);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
