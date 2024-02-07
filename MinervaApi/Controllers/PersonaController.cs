using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;

namespace MinervaApi.Controllers
{
    [Route("persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        IPersona persona;
        public PersonaController(IPersona _pers)
        {
            persona = _pers;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var personas = await persona.GetALLPersonas();

            if (personas != null)
            {
                return Ok(personas);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
    }
}
