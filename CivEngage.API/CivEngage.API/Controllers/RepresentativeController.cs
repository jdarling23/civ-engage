using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CivEngage.API.Helpers;
using CivEngage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CivEngage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativeController : ControllerBase
    {
        private RepresentativeHelper _helper;

        public RepresentativeController(RepresentativeHelper helper)
        {
            _helper = helper;
        }

        [HttpGet]
        public ActionResult<List<Representative>> GetReps(string address)
        {
            try
            {
                var googleResult = _helper.CallGoogleApi(address);
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
