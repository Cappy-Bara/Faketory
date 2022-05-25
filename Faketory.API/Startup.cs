using System.Reflection;
using Faketory.API.Authentication;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Middleware;
using Faketory.API.Middleware.ExceptionHandlingMiddleware;
using Faketory.API.SwaggerSettings;
using Faketory.Application.Installation;
using Faketory.Application.Policies;
using Faketory.Application.Services.Implementations;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IPolicies;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddCustomAuthentication(Configuration);

            services.AddControllers()
                   .AddFluentValidation(x =>
                   x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Faketory.API", Version = "v1" });
                c.AddCustomAuthorization();
            });

            services.AddApplication();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IPlcRepository, PlcRepository>();
            services.AddScoped<IPlcEntityRepository, PlcEntityRepository>();
            services.AddScoped<IPlcModelRepository, PlcModelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IIORepository, IORepository>();
            services.AddScoped<IConveyorRepository, ConveyorRepository>();
            services.AddScoped<IPalletRepository, PalletRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<ITimestampService, TimestampService>();
            services.AddScoped<Scene>();
            services.AddScoped<IInputOccupiedPolicy, InputOccupiedPolicy>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserDataProvider, UserDataProvider>();

            services.AddDbContext<FaketoryDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"));
            });

            services.AddAutoMapper(this.GetType().Assembly);

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
            app.UseMiddleware<TimeMeasuringMiddleware>();
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