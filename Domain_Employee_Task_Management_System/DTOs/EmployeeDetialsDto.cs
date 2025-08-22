using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.DTOs
{
    public class EmployeeDetialsDto
    {
        public int EmployeeID { get; set; }
        public string Role { get; set; }   
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Joining_Date { get; set; }
    }
}
