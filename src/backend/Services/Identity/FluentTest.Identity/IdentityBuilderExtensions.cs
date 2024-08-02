using FluentTest.Identity.Application;
using FluentTest.Identity.Policies;
using FluentTest.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluentTest.Identity
{
    public static class IdentityBuilderExtensions
    {
        public static IMvcBuilder AddIdentityPart(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(UserController).Assembly);
            return builder;
        }

        public static IdentityBuilder AddCustomerIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
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
            services.AddAuthorization(options => options.AddPolicy("", policy => policy.Requirements.Add(new PermissionRequirement())));
            IdentityBuilder identityBuilder = services.AddIdentity<IdentityUser, IdentityRole>(options => { options.SignIn.RequireConfirmedPhoneNumber = true; })
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            return identityBuilder;
        }
    }
}