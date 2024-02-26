using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;

namespace Minerva.Controllers
{
    [Route("documentclassification")]
    [ApiController]
    public class DocumentClassificationController : Controller
    {
        IDocumentClassificationBL DocumentClassification;
        public DocumentClassificationController(IDocumentClassificationBL _DocumentClassification)
        {
            DocumentClassification = _DocumentClassification;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ft = await DocumentClassification.GetALLDocumentClassifications();

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
            var ft = await DocumentClassification.GetDocumentClassification(id);

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
