using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Infractructure.Repositories;
using Companies.Presentation;
using Companies.Services;
using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;

namespace Companies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSql(builder.Configuration);

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                            // .AddXmlDataContractSerializerFormatters()
                            .AddNewtonsoftJson()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddOpenApi();
            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();

            builder.Services.AddAuthentication();
            builder.Services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;

                opt.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

           
            builder.Services.AddHostedService<DataSeedHostingService>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.ConfigureCors();

            var app = builder.Build();




            app.ConfigureExceptionHandler();

            // Configure the HTTP request pipeline.
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
