using System.Reflection;
using Faketory.API.Authentication;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Middleware;
using Faketory.API.Middleware.ExceptionHandlingMiddleware;
using Faketory.API.SwaggerSettings;
using Faketory.Application.Installation;
using Faketory.Domain.Installation;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.Installation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Faketory.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSwagger();

            services.AddApplication(Configuration);
            services.AddDomain();
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();
            services.AddCustomAuthentication(Configuration);
            services.AddScoped<IUserDataProvider, UserDataProvider>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddAutoMapper(GetType().Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddTimeMeasuringMiddleware(Configuration);

            app.UseCors("AllowAllOrigins");

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Faketory.API v1")); ;

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}