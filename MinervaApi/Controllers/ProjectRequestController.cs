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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ProjectRequestDetailsInsert(ProjectRequestDetail request)
        {
            Comman.logEvent(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request));
            try
            {
                if (ModelState.IsValid)
                {
                    APIStatus aPIStatus = new APIStatus();

                    aPIStatus = await projectRequest.SaveProjectRequestDetails(request);
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
                Comman.logError(System.Reflection.MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(request) + " error " + ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
