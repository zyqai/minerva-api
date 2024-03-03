using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using Minerva.Models.Returns;
using MinervaApi.ExternalApi;
using MinervaApi.Models.Requests;
using Newtonsoft.Json;
using System.Security.Claims;

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
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            request.CreatedByUserId = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    int b = await ProtBL.SaveProject(request);
                    if (b>1)
                    {
                        Project? p=await ProtBL.GetProjects(b);
                        return StatusCode(StatusCodes.Status201Created, p);
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
            request.ModifiedByUserId = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
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

        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProjectDetails(int id)
        {
            var projects = await ProtBL.GetProjectDetails(id);

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


        [HttpGet("projectList")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetprojectList()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            var projects = await ProtBL.GetAllProjectsWithDetails(email);

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpPost("projectWithDetails")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateProjectWithDetails(ProjectwithDetailsRequest request)
        {
            string ?CreatedBy = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    int b = await ProtBL.SaveProjectWithDetails(request,CreatedBy);
                    if (b > 1)
                    {
                        Project? p = await ProtBL.GetProjects(b);
                        return StatusCode(StatusCodes.Status201Created, p);
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

        [HttpGet("getProjectWithDetails/{id}")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> getProjectWithDetails(int id)
        {
            var projects = await ProtBL.getProjectWithDetails(id);

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


        [HttpPost("createProjectRequest")]
        [Authorize(Policy = "TenantAdminPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> createProjectRequest(ProjectRequestData request)
        {
            string? emails = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    Apistatus aPI = new Apistatus();
                    aPI = await ProtBL.SaveProjectRequest(request, emails);
                    if (aPI!=null)
                    {
                        return StatusCode(StatusCodes.Status201Created, aPI);
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

    }
}
