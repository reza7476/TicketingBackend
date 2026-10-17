using Microsoft.OpenApi.Models;

namespace Ticketing.Api.Configurations.SwaggerSettings;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfigurationGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "JWT Api",
                Description = "Secures API using JWT",
                Contact = new OpenApiContact
                {
                    Name = "Reza dehghani",
                    Email = "rdehghani.akorn@gmail.com",
                    Url = new Uri("https://www.google/")
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eydlnbs\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement

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
                            new string[] {}
                    }
                });
        });




        return services;
    }
}
