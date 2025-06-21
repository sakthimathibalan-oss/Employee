using Employee.Models;
using Employee.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeServices _service;

        public EmployeeController(EmployeeServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{employeeName}")]
        public async Task<IActionResult> GetByName(string employeeName)
        {
            var emp = await _service.GetByNameAsync(employeeName);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDTO employee)
        {
            var created = await _service.CreateAsync(employee);
            return CreatedAtAction(nameof(Get), new { employeeName = created.EmployeeName }, created);
        }

        [HttpPut("{employeeName}")]
        public async Task<IActionResult> UpdateByName(string employeeName, EmployeeDTO employee)
        {
            var updated = await _service.UpdateByNameAsync(employeeName, employee);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{employeeName}")]
        public async Task<IActionResult> DeleteByName(string employeeName)
        {
            var deleted = await _service.DeleteByNameAsync(employeeName);
            return deleted ? NoContent() : NotFound();
        }
    }
}
