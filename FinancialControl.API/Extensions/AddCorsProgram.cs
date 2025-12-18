namespace FamilyFinancialControl.API.Extensions;

public static class AddCorsProgram
{
    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        string[] corsUrls = configuration.GetSection("CorsUrl").Get<string[]>();


        corsUrls = corsUrls.Append(configuration["SSLURL"] + configuration["HostedURL"]).ToArray();
        corsUrls = corsUrls.Append(configuration["SSLURL"] + "www." + configuration["HostedURL"]).ToArray();

        services.AddCors(opt =>
        {
            opt.AddPolicy("ClientPermission", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(
                        corsUrls)
                    .AllowCredentials();
            });
        });

        return services;
    }
}

