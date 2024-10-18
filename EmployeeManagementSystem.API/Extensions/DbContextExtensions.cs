using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Extensions
{
    public static class DbContextExtensions
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
    }
}
