using Domain_Employee_Task_Management_System.DTOs;
using Domain_Employee_Task_Management_System.ResponseDTOs;
using EmployeeTaskManagementSystem.Db_Con_Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly DbData _db;

        public ProfileController(DbData db)
        {
            _db = db;
        }

        [HttpPost("GetProfile")]
        public async Task<IActionResult> GetProfile([FromBody] ProfileDto profileDto)
        {
            try
            {
                var employee = await _db.Employees
                    .Where(e => e.EmployeeID == profileDto.EmployeeID)
                    .Select(e => new ProfileResponseDto
                    {
                        Rp_EmployeeID = e.EmployeeID,
                        Rp_Name = e.Name,
                        Rp_Email = e.Email,
                        Rp_Department = e.Department,
                        Rp_Role = e.Role,
                        Rp_Joining_Date = e.Joining_Date
                    })
                    .FirstOrDefaultAsync();

                if (employee == null)
                    return NotFound(new { rp_Message = "Profile not found" });

                return Ok(employee);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, new { error = ex.Message });
            }
        }






    }
}
