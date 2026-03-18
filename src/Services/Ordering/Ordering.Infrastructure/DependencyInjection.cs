using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<OrderingDbContext>(options =>
                options.UseSqlServer(connectionString));

            //services.AddScoped<IOrderingDbContext, OrderingDbContext>();
            return services;
        }
    }
}
