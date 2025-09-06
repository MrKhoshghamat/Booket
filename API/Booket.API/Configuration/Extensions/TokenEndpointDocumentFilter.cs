using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Booket.API.Configuration.Extensions;

public class TokenEndpointDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Paths.Add("/connect/token", new OpenApiPathItem
        {
            Description = "Request an access token using phone number OTP",
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Post] = new OpenApiOperation
                {
                    Tags = new List<OpenApiTag> { new() { Name = "Authentication" } },
                    Summary = "Get JWT token",
                    Description = "Exchange credentials (phone number + OTP) for an access token.",
                    RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/x-www-form-urlencoded"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["grant_type"] = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("otp") },
                                        ["phoneNumber"] = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("") },
                                        ["otp"] = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("") },
                                        ["client_id"] = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("ro.client") },
                                        ["client_secret"] = new OpenApiSchema { Type = "string", Example = new Microsoft.OpenApi.Any.OpenApiString("secret") }
                                    },
                                    Required = new HashSet<string> { "grant_type", "phoneNumber", "otp", "client_id", "client_secret" }
                                }
                            }
                        }
                    },
                    Responses = new OpenApiResponses
                    {
                        ["200"] = new OpenApiResponse
                        {
                            Description = "Token Response",
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            ["access_token"] = new OpenApiSchema { Type = "string" },
                                            ["expires_in"] = new OpenApiSchema { Type = "integer" },
                                            ["token_type"] = new OpenApiSchema { Type = "string" },
                                            ["scope"] = new OpenApiSchema { Type = "string" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
    }
}