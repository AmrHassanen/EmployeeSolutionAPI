using EmployeeManagementSystem.API.Mapping;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Services;

namespace EmployeeManagementSystem.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(typeof(MappingProfiles));

        }
    }
}
