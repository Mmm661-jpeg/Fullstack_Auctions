using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplicationAuctions.Api.Middleware.Extensions
{
    public static class SwaggerExtension
    {

        public static IServiceCollection AddSwaggerExtended(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Our Auction API with swagger",
                    Version = "v1"
                });

                
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token\nExample: \"Bearer eyJhbGciOiJIUz...\""
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

                
                options.OperationFilter<SwaggerFileUploadFilter>();

                
                options.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "file",
                    Format = "binary"
                });
            });

            return services;
        }



        public static IApplicationBuilder UseSwaggerExtended(this IApplicationBuilder app)
        {
           
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }

        public class SwaggerFileUploadFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var formFileParams = context.MethodInfo.GetParameters()
                    .Where(p => p.ParameterType == typeof(IFormFile))
                    .ToList();

                if (!formFileParams.Any()) return;

                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary",
                                    Description = "Upload file"
                                },
                                ["auctionID"] = new OpenApiSchema
                                {
                                    Type = "integer",
                                    Format = "int32"
                                }
                            },
                            Required = new HashSet<string> { "file", "auctionID" }
                        }
                    }
                }
                };
            }
        }
    }
}


