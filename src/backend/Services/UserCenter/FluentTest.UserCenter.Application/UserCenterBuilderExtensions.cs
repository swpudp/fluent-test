using FluentTest.UserCenter.Application.Controllers;
using FluentTest.UserCenter.DataAccess.Contract;
using FluentTest.UserCenter.DataAccess.Impl;
using FluentTest.UserCenter.Model.Entity;
using FluentTest.UserCenter.Service.Contract;
using FluentTest.UserCenter.Service.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.UserCenter.Application
{
    public static class UserCenterBuilderExtensions
    {
        public static IMvcBuilder AddUserCenterPart(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UserController).Assembly);
            return builder;
        }

        public static IServiceCollection AddUserCenterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IdentityBuilder AddCustomerIdentity(this IServiceCollection services)
        {
            IdentityBuilder identityBuilder = services.AddIdentity<User, Role>(options => { options.SignIn.RequireConfirmedPhoneNumber = true; })
                .AddUserStore<UserRepository>()
                .AddRoleStore<RoleRepository>();

            return identityBuilder;
        }
    }
}
