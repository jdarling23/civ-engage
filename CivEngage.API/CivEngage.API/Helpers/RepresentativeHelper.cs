using CivEngage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace CivEngage.API.Helpers
{
    public class RepresentativeHelper
    {
        private IConfiguration _config;

        public RepresentativeHelper(IConfiguration config)
        {
            _config = config;
        }

        public string CallGoogleApi(string address)
        {
            var queystring = GetQueryString(address);
            var client = new RestClient(_config["GoogleUri"] + queystring);

            var request = new RestRequest(Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = client.Execute(request);

            return response.Content;
        }

        public List<Representative> ProcessGoogleResult(string result)
        {
            return new List<Representative>();
        }

        private string GetQueryString(string address)
        {
            var queryString = address.Replace(" ", "%20");
            return queryString;
        }
    }
}
