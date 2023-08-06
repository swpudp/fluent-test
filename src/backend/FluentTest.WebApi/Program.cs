using FluentTest.WebExtension;
using FluentTest.UserCenter.Application;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using FluentTest.WebExtension.Mvc;

namespace FluentTest.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddApiControllers()
                .AddUserCenterPart();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.AddLog4Net();
            builder.Services.AddUserCenterServices();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseExceptionHandler(err => err.UseCustomErrors());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}