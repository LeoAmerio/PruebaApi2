using ApiMinimaEFPostgresql.Data;
using ApiMinimaEFPostgresql.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<OfficeDb>(options => 
options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/employees/", async (Employee e, OfficeDb db) =>
{
    db.Employees.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{e.Id}", e);
});

app.MapGet("/employee/{id:int}", async (int id, OfficeDb db) =>
{
    return await db.Employees.FindAsync(id)
        is Employee e
        ? Results.Ok()
        : Results.NotFound();
});
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}