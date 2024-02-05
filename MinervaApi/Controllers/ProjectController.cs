using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;

namespace MinervaApi.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectsBL ProtBL;
        public ProjectController(IProjectsBL projectBL)
        {
            ProtBL = projectBL;
        }

        [HttpPost]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "Staff")]
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await ProtBL.SaveProject(request);
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
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await ProtBL.GetAllProjects();

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var projects = await ProtBL.GetProjects(id);

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject(ProjectRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await ProtBL.UpdateProject(request);
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

        [Authorize(Policy = "TenantAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await ProtBL.DeleteProject(id);
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
