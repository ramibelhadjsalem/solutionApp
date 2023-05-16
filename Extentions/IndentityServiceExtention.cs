using Microsoft.AspNetCore.Identity;
using solution.Data;
using solution.Data.Entities;

namespace solution.Extentions
{
    public static class IndentityServiceExtention
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;

            }).AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
                options.AddPolicy("RequireTechnicienRole", policy => policy.RequireRole("Technicien"));

            });
            services
                .AddAuthentication("Identity.Application")
                .AddCookie("Identity.Application", opt =>
                {
                    opt.LoginPath = "/account/login";
                    opt.LogoutPath = "/account/logout";
                    opt.AccessDeniedPath = "/error/forbiden";
                    
                });

            return services;
        }
    }
}
