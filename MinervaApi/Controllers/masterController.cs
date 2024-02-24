﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minerva.Models;
using MinervaApi.BusinessLayer;
using MinervaApi.BusinessLayer.Interface;

namespace MinervaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class masterController : ControllerBase
    {
        IMasterBL bl;
        public masterController(IMasterBL _masterBL) 
        {
            bl = _masterBL;
        }
        [HttpGet]
        public async Task<IActionResult> Getindustrys()
        {
            var res = await bl.GetindustryAsync();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetloanTypes()
        {
            var res = await bl.GetloanTypesAsync();

            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return NotFound(); // or another appropriate status
            }
        }

    }
}
