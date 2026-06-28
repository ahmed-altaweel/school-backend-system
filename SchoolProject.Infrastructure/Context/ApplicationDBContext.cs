using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchooProject.Data.Entities;

namespace SchoolProject.Infrastructure.Context
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<DepartmentSubject> departmentSubjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Ins_Subject> ins_Subjects { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(x => new { x.SubID, x.DID });
            modelBuilder.Entity<Ins_Subject>()
               .HasKey(x => new { x.SubId, x.InsId });
            modelBuilder.Entity<StudentSubject>()
               .HasKey(x => new { x.StudID, x.SubID });
            modelBuilder.Entity<Instructor>()
                .HasOne(x => x.Supervisor)
                .WithMany(x => x.Instructors)
                .HasForeignKey(x => x.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);
            foreach (var model in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                model.DeleteBehavior = DeleteBehavior.NoAction;


            }




        }

    }
}
