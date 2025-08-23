using Domain_Employee_Task_Management_System.DTOs;
using Domain_Employee_Task_Management_System.ResponseDTOs;
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
                    AssignedTo_EmployeeId = t.AssignedTo_EmployeeId,
                    Employee = t.Employee != null ? new EmployeeDetialsDto
                    {
                        EmployeeID = t.Employee.EmployeeID,
                        Name = t.Employee.Name,
                        Email = t.Employee.Email,
                        Department = t.Employee.Department,
                        Role = t.Employee.Role,
                        Joining_Date = t.Employee.Joining_Date
                    } : null
                })
                .ToListAsync();

            return Ok(tasks);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _db.Tasks
                                .Include(t => t.Employee)
                                .FirstOrDefaultAsync(t => t.TaskID == id);

            if (task == null) return NotFound();

            var dto = new TasksDto
            {
                TaskID = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                TaskStatus = task.TaskStatus,
                DueDate = task.DueDate,
                AssignedTo_EmployeeId = task.AssignedTo_EmployeeId,
                Employee = task.Employee != null ? new EmployeeDetialsDto
                {
                    EmployeeID = task.Employee.EmployeeID,
                    Name = task.Employee.Name,
                    Email = task.Employee.Email,
                    Department = task.Employee.Department,
                    Role = task.Employee.Role,
                    Joining_Date = task.Employee.Joining_Date
                } : null
            };

            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TasksDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            if (!DateTime.TryParse(dto.DueDate, out var parsedDate))
            {
                return BadRequest("Invalid DueDate format. Expected yyyy-MM-dd.");
            }

            var task = new Tasks
            {
                Title = dto.Title,
                Description = dto.Description,
                AssignedTo_EmployeeId = dto.AssignedTo_EmployeeId,
                TaskStatus = dto.TaskStatus,
                DueDate = dto.DueDate
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            await _db.Entry(task).Reference(t => t.Employee).LoadAsync();

            var taskResponse = new TasksResponseDto
            {
                Rp_TaskID = task.TaskID,
                Rp_Title = task.Title,
                Rp_Description = task.Description,
                Rp_TaskStatus = task.TaskStatus,
                Rp_DueDate = task.DueDate
            };

            var employeeResponse = task.Employee != null ? new EmployeeDetialsResponseDto
            {
                Rp_EmployeeID = task.Employee.EmployeeID,
                Rp_Name = task.Employee.Name,
                Rp_Email = task.Employee.Email,
                Rp_Department = task.Employee.Department,
                Rp_Role = task.Employee.Role,
                Rp_Joining_Date = task.Employee.Joining_Date,
                Tasks = new List<TasksResponseDto> { taskResponse }
            } : null;

            return Ok(new
            {
                Task = taskResponse,
                Employee = employeeResponse
            });
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TasksDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            if (!DateTime.TryParse(dto.DueDate, out var parsedDate))
            {
                return BadRequest("Invalid DueDate format. Expected yyyy-MM-dd.");
            }

            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.AssignedTo_EmployeeId = dto.AssignedTo_EmployeeId;
            task.TaskStatus = dto.TaskStatus;
            task.DueDate = dto.DueDate;

            await _db.SaveChangesAsync();
            await _db.Entry(task).Reference(t => t.Employee).LoadAsync();

            var taskResponse = new TasksResponseDto
            {
                Rp_TaskID = task.TaskID,
                Rp_Title = task.Title,
                Rp_Description = task.Description,
                Rp_TaskStatus = task.TaskStatus,
                Rp_DueDate = task.DueDate
            };

            var employeeResponse = task.Employee != null ? new EmployeeDetialsResponseDto
            {
                Rp_EmployeeID = task.Employee.EmployeeID,
                Rp_Name = task.Employee.Name,
                Rp_Email = task.Employee.Email,
                Rp_Department = task.Employee.Department,
                Rp_Role = task.Employee.Role,
                Rp_Joining_Date = task.Employee.Joining_Date,
                Tasks = new List<TasksResponseDto> { taskResponse }
            } : null;

            return Ok(new
            {
                Task = taskResponse,
                Employee = employeeResponse
            });
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
