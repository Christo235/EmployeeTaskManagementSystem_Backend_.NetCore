using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class AuthLoginResponseDto
    {
        public string Rp_Message { get; set; }
        public string Rp_Token { get; set; }   // later for JWT
        public int Rp_EmployeeID { get; set; }
        public string Rp_Name { get; set; }
      
    }
}
