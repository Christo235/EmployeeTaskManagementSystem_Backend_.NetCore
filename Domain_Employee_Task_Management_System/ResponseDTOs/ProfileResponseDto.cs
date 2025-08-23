using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class ProfileResponseDto
    {
        public int Rp_EmployeeID { get; set; }
        public string Rp_Name { get; set; } = string.Empty;
        public string Rp_Email { get; set; } = string.Empty;
        public string Rp_Department { get; set; } = string.Empty;
        public string Rp_Role { get; set; } = string.Empty;
        public string Rp_Joining_Date { get; set; }
       
    }
}
