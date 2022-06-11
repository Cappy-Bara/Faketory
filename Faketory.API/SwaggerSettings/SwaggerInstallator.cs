using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Faketory.API.SwaggerSettings
{
    public static class SwaggerInstallator
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Faketory.API", Version = "v1" });
                c.AddCustomAuthorization();

            });

            return services;
        }
    }
}
