using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;



namespace CoreWebAPIPract.IdentityBasedAuth
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            //builder.Entity<Employees>()
            //    .ToTable("tbl_employee");

            //builder.Entity<Employee>()
            //    .HasKey(e => e.Id);

            //builder.Entity<Employee>()
            //    .Property(e => e.Name)
            //    .IsRequired()
            //    .HasMaxLength(50);

            //builder.Entity<Employee>()
            //    .HasIndex(e => e.Email)
            //    .IsUnique();

            //builder.Entity<Employee>()
            //    .Property(e=>e.Email)
            //    .IsRequired()
            //    .HasMaxLength(50);

            //builder.Entity<Employee>()
            //    .Property(e => e.CreatedAt)
            //    .HasDefaultValueSql("GETDATE()");
        }
    }
}
