using EmployeeTaskManagementSystem.Db_Entity_Class;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementSystem.Db_Con_Datas
{
    public class DbData : DbContext
    {
        public DbData(DbContextOptions<DbData> options) : base(options)
        {
        }

        
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.AssignedTo_EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
