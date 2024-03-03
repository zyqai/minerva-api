using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MinervaApi.BusinessLayer.Interface;

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
        public async Task<IActionResult> Get(int projectId)
        {
            var  res= await projectRequest.GetALLAsync(projectId);

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
    }
}
