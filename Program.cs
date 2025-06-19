using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers(); // Enables attribute-based API controllers
builder.Services.AddEndpointsApiExplorer(); // For minimal APIs and Swagger
builder.Services.AddSwaggerGen(); // Adds Swagger for API testing

// Register EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map all API controllers like EventsController
app.MapControllers();

app.Run();
