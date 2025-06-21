using Employee.DbContext;
using Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Services
{
    public class EmployeeServices
    {
        private readonly ApplicationDbContext _context;

        public EmployeeServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDTO>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<EmployeeDTO?> GetByNameAsync(string employeeName)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeName == employeeName);
        }
        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeDTO?> UpdateByNameAsync(string employeeName, EmployeeDTO updatedEmployee)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeName == employeeName);
            if (employee == null) return null;

            // Update properties as needed
            employee.FatherName = updatedEmployee.FatherName;
            employee.Email = updatedEmployee.Email;
            employee.MobileNumber = updatedEmployee.MobileNumber;
            employee.Department = updatedEmployee.Department;
            employee.Salary = updatedEmployee.Salary;

            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteByNameAsync(string employeeName)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeName == employeeName);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
