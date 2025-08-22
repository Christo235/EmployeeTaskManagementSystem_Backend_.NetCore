using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class AuthRegisterResponseDto
    {
        public int Rp_EmployeeID { get; set; }
        public string Rp_Name { get; set; }
        public string Rp_Role { get; set; }
        public string Rp_Email { get; set; }
        public string Rp_Department { get; set; }

        //public string Rp_Password { get; set; }

        public string Rp_Message { get; set; }
     
    }
}
