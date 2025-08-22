using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class AuthLoginResponseDto
    {
        public string Message { get; set; }
        public string Token { get; set; }   // later for JWT
        public int EmployeeID { get; set; }
        public string Name { get; set; }
    }
}
