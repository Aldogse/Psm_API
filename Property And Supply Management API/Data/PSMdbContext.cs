using Microsoft.EntityFrameworkCore;
using Model;

namespace Property_And_Supply_Management_API.Data
{
    public class PSMdbContext : DbContext
    {
        public PSMdbContext(DbContextOptions <PSMdbContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Department>Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1 , department_name = "OR",departmental_email = "or@gmail.com"},
                new Department { Id = 2, department_name = "Cardiology", departmental_email = "cardiology@gmail.com" },
                new Department { Id = 3, department_name = "Neurology ", departmental_email = "neurology@gmail.com" },
                new Department { Id = 4, department_name = "Orthopedics", departmental_email = "orthopedics@gmail.com" },
                new Department { Id = 5, department_name = "OB/GYN", departmental_email = "obgyn@gmail.com" },
                new Department { Id = 6, department_name = "Radiology ", departmental_email = "radiology@gmail.com" },
                new Department { Id = 7, department_name = "Oncology", departmental_email = "oncology@email.com" },
                new Department { Id = 8, department_name = "Anesthesiology ", departmental_email = "ezekiel.lamoste@gmail.com" },
                new Department { Id = 9, department_name = "Intensive Care Unit", departmental_email = "icu@gmail.com" },
                new Department { Id = 10, department_name = "General Surgery ", departmental_email = "generalsurgery@gmail.com" },
                new Department { Id = 11, department_name = "Nephrology", departmental_email = "nephrology@gmail.com" },
                new Department { Id = 12, department_name = "Psychiatry", departmental_email = "psychiatry@gmail.com" },
                new Department { Id = 13, department_name = "ER", departmental_email = "er@gmail.com" }
                );
        }
    }
}
