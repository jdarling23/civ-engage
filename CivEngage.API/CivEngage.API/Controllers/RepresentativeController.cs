using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CivEngage.API.Helpers;
using CivEngage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CivEngage.API.Controllers
{
    [Route("api/[controller]")]
    public class RepresentativeController : Controller
    {
        private RepresentativeHelper _helper;
        private IConfiguration _config;

        public RepresentativeController(
            RepresentativeHelper helper,
            IConfiguration config
        )
        {
            _helper = helper;
            _config = config;
        }

        [HttpGet, Route("TestGet")]
        public ActionResult<bool> TestGet()
        {
            return Ok("Yeet");
        }

        [HttpGet, Route("GetReps")]
        public ActionResult<List<Representative>> GetReps(string address)
        {
            try
            {
                var googleUri = _config["GoogleUri"];

                var googleResult = _helper.CallGoogleApi(address, googleUri);
                var reps = _helper.ProcessGoogleResult(googleResult);
                return reps;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error returning representative data: {ex.Message}");
            }
        }
    }
}
