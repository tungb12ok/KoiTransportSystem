using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Configuration
{
    public static class DbContextConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KoiFishTransportationDBContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("local")));

        }
    }
}
