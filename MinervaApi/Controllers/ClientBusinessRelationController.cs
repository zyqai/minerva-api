using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.ExternalApi;
using MinervaApi.Models.Requests;
using Newtonsoft.Json;

namespace Minerva.Controllers
{
    [Route("clientBusinessRelation")]
    [ApiController]
    public class ClientBusinessRelationController : ControllerBase
    {
        ICBRelation relation;
        public ClientBusinessRelationController(ICBRelation cBRelation)
        {
            relation = cBRelation;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            comman.logEvent("clientBusinessRelationGet", id.ToString());
            try
            {
                var ten = await relation.GetAync(id);
                if (ten != null)
                {
                    comman.logRes("clientBusinessRelationGet", JsonConvert.SerializeObject(ten));
                    return Ok(ten);
                }
                else
                {
                    return NotFound(); // or another appropriate status
                }
            }
            catch (Exception ex)
            {
                comman.logError("clientBusinessRelationGet", ex.Message.ToString());
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<CBRelationRequest?> requests)
        {
            List<CBRelation?> resList = new List<CBRelation?>();
            try
            {
                foreach (var request in requests)
                {
                    if (ModelState.IsValid)
                    {
                        var b = await relation.Save(request);

                        if (b > 0)
                        {
                            CBRelation? res = await relation.GetAync(b);
                            if (res != null)
                            {
                                resList.Add(res);
                            }
                        }
                    }
                }
                if (resList != null)
                    return StatusCode(StatusCodes.Status201Created, resList);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(List<CBRelationRequest> requests)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool b = false;
                    foreach (var request in requests)
                    {
                        b = await relation.Update(request);
                    }

                    if (b)
                    {
                        return StatusCode(StatusCodes.Status205ResetContent, requests);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, requests);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await relation.Delete(id);
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ten = await relation.GetALLAync();

            if (ten != null)
            {
                return Ok(ten);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("businessRelation/{businessId}")]
        public async Task<IActionResult> GetBusinessRelation(int ?businessId)
        {
            var BusinessRelationList = await relation.GetBusinessRelationList(businessId);
            if (BusinessRelationList != null)
            {
                return Ok(BusinessRelationList);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
