using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class EmployeeDetialsResponseDto
    {
        public int EmployeeID { get; set; }  
        public string Role { get; set; }       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Joining_Date { get; set; }
        public string Department { get; set; }

        public List<TasksResponseDto> Tasks { get; set; } = new();
    }
}
