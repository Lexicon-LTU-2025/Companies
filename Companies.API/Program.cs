using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Infractructure.Repositories;
using Companies.Presentation;
using Companies.Services;
using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
           
            builder.Services.AddHostedService<DataSeedHostingService>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
            builder.Services.ConfigureCors();

            var app = builder.Build();




            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = context.Response.StatusCode,
                            Title = "Internal Server Error",
                            Detail = contextFeature.Error.Message,
                            Instance = context.Request.Path,
                            Type = "https://httpstatuses.com/500"
                        };

                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            // app.UseDemoMiddleware();


            app.MapControllers();



            app.Run();
        }
    }
}
