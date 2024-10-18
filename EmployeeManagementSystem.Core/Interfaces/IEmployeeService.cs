using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<int> GetTotalEmployeesCountAsync();
    }
}
