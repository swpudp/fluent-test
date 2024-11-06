using FluentTest.Identity;
using FluentTest.Identity.NpgSql;
using FluentTest.Infrastructure;
using FluentTest.Scheduled;
using FluentTest.Scheduled.NpgSql;
using FluentTest.WebExtension;
using System.Runtime.Loader;

namespace FluentTest.Mgt.P.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApiControllers()
            .AddIdentityPart()
            .AddScheduledPart();

        builder.Services.AddCustomerIdentity().UseNpgSql();
        builder.Services.AddScheduledServices(builder.Configuration).UseNpgSql();

        builder.Services.Configure<AppOption>(builder.Configuration.GetSection(nameof(AppOption)));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyLoadContext.Default.Assemblies.Where(x => x.GetName().FullName.StartsWith("FluentTest")).ToArray()));
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