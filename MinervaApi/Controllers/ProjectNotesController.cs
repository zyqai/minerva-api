using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.ExternalApi;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MinervaApi.Controllers
{
    [Route("ProjectNotes")]
    [ApiController]
    public class ProjectNotesController : ControllerBase
    {
        IProjectNotesBL PNBL;
        public ProjectNotesController(IProjectNotesBL ProjectNotesBL)
        {
            PNBL = ProjectNotesBL;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> CreateProjectNotes(ProjectNotesRequest request)
        {
            request.createdByUserId = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    int b = await PNBL.SaveProjectNotes(request);
                    if (b > 1)
                    {
                        ProjectNotes? p = await PNBL.GetProjectNotes(b);
                        Comman.logRes(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(p));
                        return StatusCode(StatusCodes.Status201Created, p);

                    }
                    else
                    {
                        Comman.logRes(ControllerContext.ActionDescriptor.ActionName, "error "+JsonConvert.SerializeObject(request));
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
                Comman.logRes(ControllerContext.ActionDescriptor.ActionName, "error " + ex.Message.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> Get()
        {
            var ProjectNotes = await PNBL.GetALLProjectNotes();

            if (ProjectNotes != null)
            {
                return Ok(ProjectNotes);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


        
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var ProjectNotes = await PNBL.GetProjectNotes(id);

            if (ProjectNotes != null)
            {
                return Ok(ProjectNotes);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


       
        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> UpdateProjectNotes(ProjectNotesRequest request)
        {
            request.ModifiedByUserId = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await PNBL.UpdateProjectNotes(request);
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
                Comman.logRes(ControllerContext.ActionDescriptor.ActionName, "error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> DeleteProjectNotes(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await PNBL.DeleteProjectNotes(id);
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
                Comman.logRes(ControllerContext.ActionDescriptor.ActionName, "error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
