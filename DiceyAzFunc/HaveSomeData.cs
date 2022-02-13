using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiceyAzFunc
{
    public class HaveSomeData
    {
        [FunctionName("HaveSomeData")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OpenApi" }, Description = "Simply returns an object with some date params. Swagger UI will reveal extensive usage of OpenApi extensions to document this function. (OpenApiOperation > Description)")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(HotDate), Description = "A Hot Date. (Set this under OpenApiResponseWithBody > Description)")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = null)] HttpRequest req)
        {
            var response =
                new HotDate
                {
                    TodaysDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    CurrentTime = DateTime.Now.ToString("T")
                };

            return new OkObjectResult(response);
        }
    }

    [OpenApiExample(typeof(HotDateExample))]
    public class HotDate
    {
        [OpenApiProperty(Description = "Today's date in the format dd/MM/yyyy  (OpenApiProperty > Description)")]
        [JsonProperty("TodaysDate")]
        public string TodaysDate { get; set; }

        [OpenApiProperty(Description = "Current time in the format HH:mm:ss  (OpenApiProperty > Description)")]
        [JsonProperty("CurrentTime")]
        public string CurrentTime { get; set; }
    }

    public class HotDateExample : OpenApiExample<HotDate>
    {
        public override IOpenApiExample<HotDate> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "HotDateExample",
                    new HotDate()
                    {
                        TodaysDate = "20/10/2021",
                        CurrentTime = "17:45:45"
                    },
                    namingStrategy
                ));

            return this;
        }
    }
}
