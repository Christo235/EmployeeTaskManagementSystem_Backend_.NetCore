using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.ResponseDTOs
{
    public class TasksResponseDto
    {
        public int Rp_TaskID { get; set; }
        public string Rp_Title { get; set; }
        public string Rp_Description { get; set; }
        public string Rp_TaskStatus { get; set; }  
        public string Rp_DueDate { get; set; }
    }
}
