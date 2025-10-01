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

        //GET: api/employees/5
        //[HttpGet]
        //public async Task<ActionResult<List<Employee>>> GetEmployes(Guid id)
        //{
        //    var employees = await _context.Employees.ToListAsync();
        //    return employees;
        //}

        //Post
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
    }
}
