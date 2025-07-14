using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Huntask.Assets.Service.Presentation.Swagger;

public class FileUploadOperationFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    var hasFileParameter = context.MethodInfo.GetParameters()
      .Any(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IFormFile[]));

    if (!hasFileParameter)
      return;

    operation.RequestBody = new OpenApiRequestBody
    {
      Content = new Dictionary<string, OpenApiMediaType>
      {
        ["multipart/form-data"] = new OpenApiMediaType
        {
          Schema = new OpenApiSchema
          {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
              ["file"] = new OpenApiSchema
              {
                Type = "string",
                Format = "binary",
                Description = "The file to upload"
              }
            },
            Required = new HashSet<string> { "file" }
          }
        }
      }
    };

    // Remove the file parameter from the parameters list to avoid conflicts
    operation.Parameters = operation.Parameters?.Where(p => 
      !string.Equals(p.Name, "file", StringComparison.OrdinalIgnoreCase)).ToList();
  }
}
