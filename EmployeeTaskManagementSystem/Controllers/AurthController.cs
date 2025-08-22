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
    public class AuthController : ControllerBase
    {
        private readonly DbData _db;

        public AuthController(DbData db)
        {
            _db = db;  
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _db.Employees.AnyAsync(e => e.Email == registerDto.Email))
            {
                return BadRequest(new AuthRegisterResponseDto
                {
                    Message = "Email already exists",
                });
            }

            
        

            var emp = new Employees
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Role = registerDto.Role,
                Department = registerDto.Department,
                Joining_Date = registerDto.Joining_Date
            };

            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();


            var response = new AuthRegisterResponseDto
            {
                EmployeeID = emp.EmployeeID,
                Name = emp.Name,
                Role = emp.Role,
                Email = emp.Email,
                Department = emp.Department,
                Message = "Registered Successfully."
            };

            return Ok(response);
           
           
        }





        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var employee = await _db.Employees
                .FirstOrDefaultAsync(e => e.Email == loginDto.Email && e.Password == loginDto.Password);

            if (employee == null)
            {
                return Unauthorized(new AuthLoginResponseDto
                {
                    Message = "Invalid email or password",
                  
                });
            }

            return Ok(new AuthLoginResponseDto
            {
                Message = "Login Successful",
                Token = null,
                EmployeeID = employee.EmployeeID,
                Name = employee.Name
            });
        }











    }
}
