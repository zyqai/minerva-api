using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;
using Minerva.Models.Requests;

namespace Minerva.Controllers
{
    [Route("states")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        IStatesBL states;
        public StatesController(IStatesBL _states) 
        {
            states = _states;
        }
        [HttpPost]
        //public async Task<IActionResult> CreateProject(StatesRequest request)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var b = await states.SaveState(request);
        //            if (b)
        //            {
        //                return StatusCode(StatusCodes.Status201Created, request);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError, request);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var state = await states.GetALLstates();

            if (state != null)
            {
                return Ok(state);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var state = await states.Getstates(id);

            if (state != null)
            {
                return Ok(state);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }
        //[HttpPut]
        //public async Task<IActionResult> Updatestate(StatesRequest request)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var b = await states.UpdateStates(request);
        //            if (b)
        //            {
        //                return StatusCode(StatusCodes.Status201Created, request);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError, request);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteState(int id)
        //{
        //    try
        //    {
        //        if (id>0)
        //        {
        //            var b = await states.DeleteStates(id);
        //            if (b)
        //            {
        //                return StatusCode(StatusCodes.Status201Created);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
