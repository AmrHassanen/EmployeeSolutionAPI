using AutoMapper;
using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // Get all employees with pagination
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(int pageNumber = 1, int pageSize = 10)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);
            var totalEmployees = await _employeeService.GetTotalEmployeesCountAsync();

            var response = new
            {
                TotalEmployees = totalEmployees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Employees = employees.Select(employee => new
                {
                    employee.Id,          // Include the ID
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.Position,
                    employee.DateOfJoining
                })
            };

            return Ok(response);
        }

        // Get employee by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var response = new
            {
                employee.Id,          // Include the ID in the response
                employee.FirstName,
                employee.LastName,
                employee.Email,
                employee.Position,
                employee.DateOfJoining
            };

            return Ok(response);
        }

        // Add a new employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            var newEmployee = await _employeeService.AddEmployeeAsync(employeeDto);

            // Return a JSON response with a message
            return Ok(new { message = "The Employee Is Created Successfully", employee = newEmployee });
        }


        // Update an employee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return Ok(updatedEmployee);
        }

        // Delete an employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
