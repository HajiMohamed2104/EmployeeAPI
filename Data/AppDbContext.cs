using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice", Position = "Developer" },
                new Employee { Id = 2, Name = "Bob", Position = "Tester" },
                new Employee { Id = 3, Name = "Charlie", Position = "Manager" },
                new Employee { Id = 4, Name = "Diana", Position = "Designer" },
                new Employee { Id = 5, Name = "Ethan", Position = "Support" }
            );
        }
    }
}
