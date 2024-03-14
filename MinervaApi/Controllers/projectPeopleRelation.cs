using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.Models.Requests;
using System.Security.Claims;

namespace MinervaApi.Controllers
{
    [Route("projectPeopleRelation")]
    [ApiController]
    public class projectPeopleRelation : ControllerBase
    {
        IprojectPeopleRelation ppr;
        IUserBL user;
        public projectPeopleRelation(IprojectPeopleRelation iprojectPeople,IUserBL _user)
        {
            ppr = iprojectPeople;
            user = _user;
        }
        [HttpPost]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> Createppr(List<projectPeopleRelationRequest?> requests)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            User ?u = new User();
            if (email != null)
            {
                u = await user.GetUserusingUserName(email);
            }
            projectPeopleRelationResponcestatus res = new projectPeopleRelationResponcestatus();
            try
            { 
                foreach (var request in requests)
                {
                    request.tenantId = u.TenantId;
                    int req =await ppr.Create(request);
                    if (req >= 1)
                    {
                        res.code = "201"; res.message = "Project people relation created sussfully";
                    }
                    else
                    {
                        res.code = "300"; res.message = "Project people relation not created!";
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
        [HttpGet("getProjectByPeople/{projectId}")]
        public async Task<IActionResult> GetProjectByPeople(int? projectId)
        {
            var res = await ppr.GetProjectByPeople(projectId);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> DeleteProjectPeopleRelation(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await ppr.DeleteProjectPeopleRelation(id);
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
