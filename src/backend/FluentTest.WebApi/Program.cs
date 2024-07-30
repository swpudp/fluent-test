using FluentTest.WebExtension;
using FluentTest.Identity;
using Microsoft.AspNetCore.Identity;
using FluentTest.Infrastructure;
using FluentTest.Identity.NpgSql;
using FluentTest.WebApi.Controllers;

namespace FluentTest.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddApiControllers()
                .AddIdentityPart()
                .AddScheduledPart();

            builder.Services
                .AddCustomerIdentity(x => IdentityNpgSqlBuilderExtensions.UseNpgSql(x))
                .AddDefaultTokenProviders();

            builder.Services.AddScheduledServices(x => ScheduledNpgSqlBuilderExtensions.UseNpgSql(x));

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                // Password settings.
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;

                // Lockout settings.
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;
            });

            builder.Services.Configure<AppOption>(builder.Configuration.GetSection(nameof(AppOption)));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.AddLog4Net();

            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(err => err.UseCustomErrors());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}