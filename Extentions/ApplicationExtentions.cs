using Microsoft.EntityFrameworkCore;
using solution.Data;
using solutionApp.Data.Repositories.ReclamationRepo;
using solutionApp.Data.Repositories.UserRepository;

namespace solution.Extentions
{
    public static class ApplicationExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseMySQL(config.GetConnectionString("DefaultConnection"));
            });

            /*   services.AddScoped<IUnitOfWork, UnitOfWork>();*/
            services.AddScoped<IReclamationRepository, ReclamationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
