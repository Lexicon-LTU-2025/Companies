using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Infractructure.Repositories;
using Companies.Presentation;
using Companies.Services;
using Domain.Contracts.Repositories;
using Domain.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using System.Configuration;
using System.Security.Claims;
using System.Text;

namespace Companies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSql(builder.Configuration);
            builder.Services.ConfigureControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();
            
            builder.Services.ConfigureAuthentication(builder.Configuration);
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureAuthorization();

            builder.Services.AddHostedService<DataSeedHostingService>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.ConfigureCors();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            app.ConfigureExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseDemoMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}
