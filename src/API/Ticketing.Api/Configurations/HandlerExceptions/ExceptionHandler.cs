
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text.Json;
using Ticketing.Shared.DTOS;
using Ticketing.Shared.Exceptions;

namespace Ticketing.Api.Configurations.HandlerExceptions;

public static class ExceptionHandler
{
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices
            .GetRequiredService<IWebHostEnvironment>();

        var logger = app.ApplicationServices
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("GlobalExceptionHandler");

        var jsonOptions = app.ApplicationServices
            .GetService<IOptions<JsonOptions>>()?.Value.SerializerOptions
            ?? new JsonSerializerOptions(JsonSerializerDefaults.Web);

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()?
                    .Error;

                var response = new ExceptionErrorDto()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Error = "InternalServerError",
                    Description = exception?.ToString() ?? "Unknown error"
                };

                if (exception is CustomException customEx)
                {
                    response.Error = exception.GetType()
                        .Name.Replace("Exception", "");

                    response.Description = exception.ToString();
                }

                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response, jsonOptions));
            });
        });

        if (!environment.IsDevelopment())
            app.UseHsts();

        return app;
    }
}
