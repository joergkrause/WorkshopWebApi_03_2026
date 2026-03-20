using Microsoft.EntityFrameworkCore;
using Workshop.BusinessLogicLayer;
using Workshop.BusinessLogicLayer.Mappings;
using Workshop.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ProjectDbContext)));
  options.EnableSensitiveDataLogging();
  options.EnableDetailedErrors();
  options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
