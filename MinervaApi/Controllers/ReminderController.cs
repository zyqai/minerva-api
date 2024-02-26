using Microsoft.AspNetCore.Mvc;
using Minerva.BusinessLayer.Interface;

namespace MinervaApi.Controllers
{


        [Route("Reminder")]
        [ApiController]
        public class ReminderController : Controller
        {
            IReminderBL Reminder;
            public ReminderController(IReminderBL _Reminder)
            {
                Reminder = _Reminder;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var r = await Reminder.GetALLReminders();

                if (r != null)
                {
                    return Ok(r);
                }
                else
                {
                    return NotFound(); // or another appropriate status
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var r = await Reminder.GetReminder(id);

                if (r != null)
                {
                    return Ok(r);
                }
                else
                {
                    return NotFound(); // or another appropriate status
                }
            }
        }

        
}
