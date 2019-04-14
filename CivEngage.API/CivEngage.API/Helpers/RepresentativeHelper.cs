using CivEngage.API.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CivEngage.API.Helpers
{
    public class RepresentativeHelper
    {
        public RepresentativeHelper()
        {

        }

        public string CallGoogleApi(string address, string googleUri)
        {
            var queystring = GetQueryString(address);
            var client = new RestClient(googleUri + queystring);

            var request = new RestRequest(Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = client.Execute(request);

            return response.Content;
        }

        public List<Representative> ProcessGoogleResult(string result)
        {
            List<Representative> reps = new List<Representative>();

            var obj = JsonConvert.DeserializeObject<JObject>(result);
            int i = 0;

            foreach(var record in obj["offices"])
            {
                var party = obj["officials"][i]["party"] ?? "None";
                var phone = obj["officials"][i]["phones"] != null ? obj["officials"][i]["phones"][0] ?? "-" : "-";
                var email = obj["officials"][i]["emails"] != null ? obj["officials"][i]["emails"][0] ?? "-" : "-";
                var imageUrl = obj["officials"][i]["photoUrl"] ?? "-";

                Representative rep = new Representative()
                {
                    Name = obj["officials"][i]["name"].ToString(),
                    Office = obj["offices"][i]["name"].ToString(),
                    Party = party.ToString(),
                    PhoneNumber = phone.ToString(),
                    Email = email.ToString(),
                    ImageUrl = imageUrl.ToString()
                };
                i++;
                reps.Add(rep);
            }

            return reps;
        }

        private string GetQueryString(string address)
        {
            var queryString = address.Replace(" ", "%20");
            return queryString;
        }
    }
}
