using Domain_Employee_Task_Management_System.DTOs;
using EmployeeTaskManagementSystem.Db_Con_Datas;
using EmployeeTaskManagementSystem.Db_Entity_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DbData _db;
        public TasksController(DbData db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasksWithEmployees()
        {
            var tasks = await _db.Tasks
                .Include(t => t.Employee)
                .Select(t => new TasksDto
                {
                    TaskID = t.TaskID,
                    Title = t.Title,
                    Description = t.Description,
                    TaskStatus = t.TaskStatus,
                    DueDate = t.DueDate,

                        EmployeeID = t.Employee.EmployeeID,
                        Name = t.Employee.Name,
                        Email = t.Employee.Email,
                        Department = t.Employee.Department,
                        Role = t.Employee.Role.ToString()
                    
                })
                .ToListAsync();

            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _db.Tasks.Include(t => t.Employee)
                                      .FirstOrDefaultAsync(t => t.TaskID == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TasksDto taskDto)
        {
            var task = new Tasks
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                AssignedTo_EmployeeId = taskDto.AssignedTo_EmployeeId,
                TaskStatus = taskDto.TaskStatus,
                DueDate = taskDto.DueDate
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return Ok(task);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TasksDto taskDto)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.AssignedTo_EmployeeId = taskDto.AssignedTo_EmployeeId;
            task.TaskStatus = taskDto.TaskStatus;
            task.DueDate = taskDto.DueDate;

            await _db.SaveChangesAsync();
            return Ok(task);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
