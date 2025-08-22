
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain_Employee_Task_Management_System.DTOs
{
    public class TasksDto:EmployeeDetialsDto
    {
        public string Title { get; set; }
        public int TaskID { get; set; }
        public string Description { get; set; }
        public int AssignedTo_EmployeeId { get; set; }
        public string TaskStatus { get; set; }
        public string DueDate { get; set; }
    }
}


