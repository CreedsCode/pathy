using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace api
{
    public static class Directions
    {
        private static JsonWriter writer;


        private static string secret = System.Environment.GetEnvironmentVariable("Secret");

        [FunctionName("Directions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
        
            log.LogInformation("C# HTTP trigger function processed a request.");
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openrouteservice.org/v2/directions/foot-walking/geojson?api_key={secret}");
            var reader = new StreamReader(req.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var stringContent = new StringContent(reader.ReadToEnd(), System.Text.Encoding.UTF8, "application/json");
            request.Content = stringContent;
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            string resultString = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject<dynamic>(resultString);

            System.Console.WriteLine(result);

            return new JsonResult(result);
        }
    }
}
