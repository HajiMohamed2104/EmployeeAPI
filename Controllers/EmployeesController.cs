using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get() => await _context.Employees.ToListAsync();

        [HttpGet("Employee Details")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            return emp == null ? NotFound() : emp;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("View Employee")]
        public async Task<IActionResult> Put(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("Remove Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
