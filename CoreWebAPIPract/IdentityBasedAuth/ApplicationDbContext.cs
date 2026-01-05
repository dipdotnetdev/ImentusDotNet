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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
