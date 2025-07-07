
using Companies.API.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Companies.API.Data;
using Companies.API.Services;

namespace Companies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHostedService<DataSeedHostingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.Map("/demo", builder =>
            //{
            //    builder.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("1. log BEFORE the next delegate");

            //        await next.Invoke();

            //        Console.WriteLine("3. log AFTER the next delegate");
            //    });

            //    builder.Run(async context =>
            //    {
            //        Console.WriteLine($"2. log in the Run method");
            //        await context.Response.WriteAsync("Hello from /demo path");
            //    });
            //});

            // app.UseDemoMiddleware();


            app.MapControllers();

            app.Run();
        }
    }
}
