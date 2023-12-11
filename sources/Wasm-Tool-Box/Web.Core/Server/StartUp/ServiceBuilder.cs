using Data.Database;
using Data.Shared;
using Data.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Web.Core.Server.StartUp
{
    public static class ServiceBuilder
    {
        public static void RegisterServices(IServiceCollection services, string databaseConnection)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseMySQL(databaseConnection);
            });

            services.AddScoped<ILogRepository, LogRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();

        }
    }
}
