using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("documenttype")]
    [ApiController]
    public class DocumentTypeController : Controller
    {
        IDocumentTypeBL DocTypeBL;
        public DocumentTypeController(IDocumentTypeBL DocumentTypeBL)
        {
            DocTypeBL = DocumentTypeBL;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var DocumentTypes = await DocTypeBL.GetALLDocumentTypes();

            if (DocumentTypes != null)
            {
                return Ok(DocumentTypes);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        //[Authorize(Policy = "TenantAdminPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var DocumentType = await DocTypeBL.GetDocumentType(id);

            if (DocumentType != null)
            {
                return Ok(DocumentType);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

    }
}
