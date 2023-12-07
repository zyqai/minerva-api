using Microsoft.AspNetCore.Mvc;

namespace Minerva.Controllers
{
    [ApiController]
    public class ProjectsController: ControllerBase
    {
        [HttpGet]
        [Route("/projects")]
        public string GetHealth()
        {
            return "Hello World";
        }
    }
}
