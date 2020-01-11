using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class UniversityContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Works> Works { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Statement> Statements { get; set; }


        public DbSet<PassWorks> PassWorks { get; set; }
   
      
        public DbSet<StatementStudentList> StatementStudents { get; set; }
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            string adminRoleName = "Admin";
            string lecturerRoleName = "Lecturer";
            string studentRoleName = "Student";

            string adminLogin = "admin";
            string adminPassword = "123456";

            // добавляем роли
           
            Role adminRole = new Role { RoleId = 1, Name = adminRoleName };
            Role lecturerRole = new Role { RoleId = 2, Name = lecturerRoleName };
            Role studentRole = new Role { RoleId = 3, Name = studentRoleName };
            Lecturer adminUser = new Lecturer {LecturerId = 1, Login = adminLogin, Password = adminPassword, RoleID = adminRole.RoleId};

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, lecturerRole, studentRole });
            modelBuilder.Entity<Lecturer>().HasData(new Client[] { adminUser });
            //modelBuilder.Entity<Student>().HasMany(s => s.StatementStudentLists).WithOne().HasForeignKey(d => d.RecordNumber).IsRequired().HasConstraintName("FK_Student");
            //modelBuilder.Entity<Statement>().HasMany(s => s.StatementStudentLists).WithOne().HasForeignKey(d => d.StatementId).OnDelete(DeleteBehavior.NoAction);
           
            modelBuilder.Entity<StatementStudentList>().HasKey(u => new { u.StatementId, u.RecordNumber }).HasName("PK_StudentStatement");

            base.OnModelCreating(modelBuilder);
        }
    }
}
