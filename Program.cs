using HelloApi;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
   // app.UseHttpsRedirection();

}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapGet("/", () => "OK");
app.MapGet("/test", () => "test");


app.MapGet("/Hello", () => "Hello WOrld");

app.MapGet("/db-check", async (IConfiguration cfg) =>
{
    var cs = cfg.GetConnectionString("Default");
    await using var conn = new NpgsqlConnection(cs);
    await conn.OpenAsync();
    await using var cmd = new NpgsqlCommand("SELECT 1", conn);
    var result = await cmd.ExecuteReaderAsync();
    return Results.Ok(new { ok = true, result });

    
});


app.Run();

//22



public partial class Program { }