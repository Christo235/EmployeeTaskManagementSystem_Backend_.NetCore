
using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagementSystem.Db_Entity_Class
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }  
        public string Role { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public string Joining_Date { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    }
}
