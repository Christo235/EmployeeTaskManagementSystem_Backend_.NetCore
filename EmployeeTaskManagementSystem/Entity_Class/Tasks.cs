
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTaskManagementSystem.Db_Entity_Class
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Employee")]
        public int AssignedTo_EmployeeId { get; set; }
        public Employees Employee { get; set; }
        public string TaskStatus { get; set; }
        public string DueDate { get; set; }
    }
}
