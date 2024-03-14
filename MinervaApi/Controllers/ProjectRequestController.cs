using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.ExternalApi;
using MinervaApi.Models.Requests;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MinervaApi.Controllers
{
    [Route("ProjectRequest")]
    [ApiController]
    public class ProjectRequestController : ControllerBase
    {
        IProjectRequestBL projectRequest;
        public ProjectRequestController(IProjectRequestBL project)
        {
            projectRequest = project;
        }
        [HttpGet("projectId")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            var res = await projectRequest.GetALLAsync(projectId);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        [HttpGet("getProjectRequestById/{projectRequestId}")]
        public async Task<IActionResult> GetProjectRequestById(int projectRequestId)
        {
            var res = await projectRequest.GetALLProjectRequestByIdasync(projectRequestId);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


        [HttpPost("createProjectRequestDetails")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> ProjectRequestDetailsInsert(ProjectRequestDetail request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    APIStatus aPIStatus = new APIStatus();

                    aPIStatus = await projectRequest.SaveProjectRequestDetails(request, email);
                    if (aPIStatus != null)
                    {
                        if (aPIStatus.Code == "200")
                        {
                            return StatusCode(StatusCodes.Status201Created, aPIStatus);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, aPIStatus);
                        }
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updateProjectRequestDetails")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> ProjectRequestDetailsUpdate(ProjectRequestDetail request)
        {
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    APIStatus aPIStatus = new APIStatus();

                    aPIStatus = await projectRequest.UpdateProjectRequestDetails(request);
                    if (aPIStatus != null)
                    {
                        if (aPIStatus.Code == "200")
                        {
                            return StatusCode(StatusCodes.Status201Created, aPIStatus);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, aPIStatus);
                        }
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("createProjectRequestSentTo")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> ProjectRequestSentToInsert(ProjectRequestSentTo request)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    APIStatus aPIStatus = new APIStatus();

                    aPIStatus = await projectRequest.SaveProjectRequestSentTo(request, email);
                    if (aPIStatus != null)
                    {
                        if (aPIStatus.Code == "200")
                        {
                            return StatusCode(StatusCodes.Status201Created, aPIStatus);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, aPIStatus);
                        }
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updateProjectRequestSentTo")]
        [Authorize(Policy = "StaffPolicy")]
        public async Task<IActionResult> ProjectProjectRequestDetails(ProjectRequestSentTo request)
        {
            Comman.logEvent(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    APIStatus aPIStatus = new APIStatus();

                    aPIStatus = await projectRequest.UpdateProjectRequestSentTo(request);
                    if (aPIStatus != null)
                    {
                        if (aPIStatus.Code == "200")
                        {
                            return StatusCode(StatusCodes.Status201Created, aPIStatus);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, aPIStatus);
                        }
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
                Comman.logError(ControllerContext.ActionDescriptor.ActionName, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
