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

        public async Task<EmployeeDTO?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeDTO?> UpdateAsync(int id, EmployeeDTO updated)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return null;

            employee.EmployeeName = updated.EmployeeName;
            employee.FatherName = updated.FatherName;
            employee.MobileNumber = updated.MobileNumber;
            employee.Email = updated.Email;
            employee.Department = updated.Department;
            employee.Salary = updated.Salary;
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
