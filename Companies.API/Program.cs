using Companies.API.Extensions;
using Companies.API.Services;
using Companies.Infractructure.Repositories;
using Companies.Presentation;
using Companies.Services;
using Domain.Contracts.Repositories;
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
using System.Text;

namespace Companies.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSql(builder.Configuration);

            builder.Services.AddControllers(opt =>
            {
                opt.ReturnHttpNotAcceptable = true;

                var policy = new AuthorizationPolicyBuilder()
                                   .RequireAuthenticatedUser()
                                   .Build();

                opt.Filters.Add(new AuthorizeFilter(policy));

            })
                            // .AddXmlDataContractSerializerFormatters()
                            .AddNewtonsoftJson()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);

            builder.Services.AddOpenApi();
            builder.Services.AddRepositories();
            builder.Services.AddServiceLayer();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("JwtSettings");
                ArgumentNullException.ThrowIfNull(jwtSettings, nameof(jwtSettings));

                var secretkey = builder.Configuration["secretkey"];
                ArgumentNullException.ThrowIfNull(secretkey, nameof(secretkey));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
                };
            });



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
