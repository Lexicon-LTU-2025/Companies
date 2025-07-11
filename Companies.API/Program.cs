using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Infractructure.Repositories;
using Companies.Presentation;
using Companies.Services;
using Domain.Contracts.Repositories;
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
