using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VP_Test_WebApp.Helper
{
    public static class Utilities
    {
        public static HttpResponseMessage HttpResponse(HttpStatusCode httpCode, string content)
        {
            string message = JsonConvert.SerializeObject(new { code = httpCode, message = content }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var resp = new HttpResponseMessage(httpCode)
            {
                Content = new StringContent(message, Encoding.UTF8, "application/json")
            };
            return resp;
        }
    }
}
