using Microsoft.AspNetCore.Mvc;

namespace Minerva.Controllers
{
    [ApiController]
    public class HealthController: ControllerBase
    {
        [HttpGet]
        [Route("/health")]
        public string GetHealth()
        {
            return "Hello World";
        }
    }
}
