using EmployeesCollaboration.Core.Services;

namespace EmployeesCollaboration.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            services.AddScoped<ICsvFileValidatorService, CsvFileValidatorService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
