using EmployeeApi.Data;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly CsvExportService _csvExportService;
        
        public EmployeesController(AppDbContext context, CsvExportService csvExportService)
        {
            _context = context;
            _csvExportService = csvExportService;
        }

        [HttpGet("View All Employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> Get() => await _context.Employees.ToListAsync();

        [HttpGet("View Specific Employee")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            return emp == null ? NotFound() : emp;
        }

        [HttpPost("Add New Employee")]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("Update Employee")]
        public async Task<ActionResult<Employee>> Put(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Position = employee.Position;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
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

        [HttpGet("Download CSV")]
        public async Task<IActionResult> DownloadCsv()
        {
            var employees = await _context.Employees.ToListAsync();
            
            if (employees == null || !employees.Any())
                return NotFound("No employees found to export.");
            
            var csvBytes = _csvExportService.GenerateEmployeeCsv(employees);
            var fileName = $"employees_{DateTime.Now:yyyyMMddHHmmss}.csv";
            
            return File(csvBytes, "text/csv", fileName);
        }
    }
}
