using EmployeeTaskManagementSystem.Db_Con_Datas;
using EmployeeTaskManagementSystem.Db_Entity_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DbData _db;

        public EmployeesController(DbData db)
        {
            _db = db;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            return Ok(await _db.Employees.AsNoTracking().ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployee(int id)
        {
            var emp = await _db.Employees
                .Include(e => e.Tasks)   
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (emp == null) return NotFound();

            return Ok(emp);
        }



        [HttpPost]
        public async Task<ActionResult<Employees>> CreateEmployee([FromBody] Employees emp)
        {
            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.EmployeeID }, emp);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employees emp)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            
            existing.Name = emp.Name;
            existing.Email = emp.Email;
            existing.Department = emp.Department;
            existing.Joining_Date = emp.Joining_Date;

            await _db.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
