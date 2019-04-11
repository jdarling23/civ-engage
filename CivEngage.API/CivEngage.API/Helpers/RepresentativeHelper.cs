using CivEngage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CivEngage.API.Helpers
{
    public class RepresentativeHelper
    {
        public JsonResult CallGoogleApi(string address)
        {
            return new JsonResult("result");
        }

        public List<Representative> ProcessGoogleResult(JsonResult result)
        {
            return new List<Representative>();
        }
    }
}
