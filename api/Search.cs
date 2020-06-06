using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace api
{
    public static class Search
    {
        private static string secret = System.Environment.GetEnvironmentVariable("Secret");

        [FunctionName("Search")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string text = req.Query["text"];

            using var client = new HttpClient();
            var responseMessage = await client.GetAsync($"https://api.openrouteservice.org/geocode/search?api_key={secret}&text="+ text);

            dynamic result = await responseMessage.Content.ReadAsAsync<dynamic>();
            System.Console.WriteLine(result);

            return new JsonResult(result);
        }
    }
}
