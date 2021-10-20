using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;

namespace DiceyAzFunc.Configurations
{
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "sharpt00th's Dicey Az Func",
            Description = "Some (hopefully) useful examples for using OpenApi extensions in an Azure Function. (Set this data in OpenApiConfigurationOptions, using the override of Info)",
            Contact = new OpenApiContact { Name = "sharpt00th", Url = new System.Uri("https://github.com/sharpt00th")  }
        };
    }
}
