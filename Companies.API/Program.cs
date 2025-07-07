
using Companies.API.Middleware;

namespace Companies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

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

            app.UseDemoMiddleware();


            app.MapControllers();

            app.Run();
        }
    }
}
