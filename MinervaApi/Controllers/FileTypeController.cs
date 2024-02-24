using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using MinervaApi.BusinessLayer.Interface;

namespace Minerva.Controllers
{
    [Route("filetype")]
    [ApiController]
    public class FileTypeController : Controller
    {
        IFileTypeBL filetype;
        public FileTypeController(IFileTypeBL _filetype)
        {
            filetype = _filetype;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ft = await filetype.GetALLFileTypes();

            if (ft != null)
            {
                return Ok(ft);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ft = await filetype.GetFileType(id);

            if (ft != null)
            {
                return Ok(ft);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }


    }
}
