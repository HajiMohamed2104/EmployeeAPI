using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EmployeeDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Employees.Any())
    {
        db.Employees.AddRange(
            new Employee { Id = 1, Name = "Alice", Position = "Developer" },
            new Employee { Id = 2, Name = "Bob", Position = "Tester" },
            new Employee { Id = 3, Name = "Charlie", Position = "Manager" },
            new Employee { Id = 4, Name = "Diana", Position = "Designer" },
            new Employee { Id = 5, Name = "Ethan", Position = "Support" }
        );
        db.SaveChanges();
    }
}

app.Run();
