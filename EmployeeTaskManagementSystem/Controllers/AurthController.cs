using Domain_Employee_Task_Management_System.DTOs;
using Domain_Employee_Task_Management_System.ResponseDTOs;
using EmployeeTaskManagementSystem.Db_Con_Datas;
using EmployeeTaskManagementSystem.Db_Entity_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly DbData _db;
        private readonly IConfiguration _config;

        public AuthController(DbData db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _db.Employees.AnyAsync(e => e.Email == registerDto.Email))
            {
                return BadRequest(new AuthRegisterResponseDto
                {
                    Rp_Message = "Email already exists",
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
                Rp_EmployeeID = emp.EmployeeID,
                Rp_Name = emp.Name,
                Rp_Role = emp.Role,
                Rp_Email = emp.Email,
                Rp_Department = emp.Department,
                Rp_Message = "Registered Successfully."
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
                    Rp_Message = "Invalid email or password"
                });
            }

           
            string token = GenerateJwtToken(employee);

            return Ok(new AuthLoginResponseDto
            {
                Rp_Message = "Login Successful",
                Rp_Token = token,
                Rp_EmployeeID = employee.EmployeeID,
                Rp_Name = employee.Name,
               
            });
        }


        private string GenerateJwtToken(Employees employee)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, employee.EmployeeID.ToString()),
        new Claim(ClaimTypes.Name, employee.Name),
        new Claim(ClaimTypes.Email, employee.Email),
        new Claim(ClaimTypes.Role, employee.Role ?? "User") 
    };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
