using AutoMapper;
using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            // Check if an employee with the same email already exists
            var existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == employeeDto.Email);

            if (existingEmployee != null)
            {
                throw new InvalidOperationException("An employee with this email already exists.");
                // Alternatively, you could return null or a special result if you don't want to throw an exception
            }

            // Map DTO to Entity
            var employee = _mapper.Map<Employee>(employeeDto);

            // Add the new employee
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Return the created Employee as a DTO
            return _mapper.Map<EmployeeDto>(employee);
        }


        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }

            _mapper.Map(employeeDto, existingEmployee);  // Map DTO to the existing entity
            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(existingEmployee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalEmployeesCountAsync()
        {
            return await _context.Employees.CountAsync();
        }
    }
}
