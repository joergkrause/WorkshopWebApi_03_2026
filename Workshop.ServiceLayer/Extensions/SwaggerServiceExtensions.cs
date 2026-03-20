using Microsoft.OpenApi;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class SwaggerServiceExtensions
{
  public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Workshop API",
        Version = "v1",
        Description = "API for managing workshop projects",
        Contact = new OpenApiContact
        {
          Name = "Workshop Team"
        }
      });
    });

    return services;
  }

  public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
  {
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workshop API v1");
      options.RoutePrefix = string.Empty;
    });

    return app;
  }
}
