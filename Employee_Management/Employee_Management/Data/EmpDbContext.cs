using Employee_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {

        }
        public DbSet<Country>Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeDetails> Employees { get; set; }
        public DbSet<EmployeeHobby> EmployeeHobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeHobby>()
                .HasKey(eh => new { eh.EmployeeId, eh.HobbyId });

            modelBuilder.Entity<EmployeeHobby>()
                .HasOne(eh => eh.Employee)
                .WithMany(e => e.EmployeeHobbies)
                .HasForeignKey(eh => eh.EmployeeId);

            modelBuilder.Entity<EmployeeHobby>()
                .HasOne(eh => eh.Hobby)
                .WithMany(h => h.EmployeeHobbies)
                .HasForeignKey(eh => eh.HobbyId);

            modelBuilder.Entity<EmployeeDetails>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDetails>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
