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

        [HttpPost]
        //[Authorize(Policy = "TenantAdminPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        //[Authorize(Policy = "Staff")]
        public async Task<IActionResult> CreateDocumentType(DocumentTypeRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await DocTypeBL.SaveDocumentType(request);
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

        //[Authorize(Policy = "TenantAdminPolicy")]
        //[Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public async Task<IActionResult> UpdateDocumentType(DocumentTypeRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await DocTypeBL.UpdateDocumentTypes(request);
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

        //[Authorize(Policy = "TenantAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await DocTypeBL.DeleteDocumentType(id);
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
