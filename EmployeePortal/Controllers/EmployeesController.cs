using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }


        //GET: api/employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }


        //Post
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if(employee.Id == Guid.Empty)
            {
                employee.Id = Guid.NewGuid();
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        //Post
        //[HttpPost]
        //public async Task<ActionResult<Employee>> Create(Employee employee)
        //{
        //    employee.Id = Guid.NewGuid();
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!await _context.Employees.AnyAsync(e=> e.Id == id)) 
                    return NotFound();
                throw;
            }
            return NoContent();
        }

    }
}
