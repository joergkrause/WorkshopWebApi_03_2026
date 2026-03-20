using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Workshop.BusinessLogicLayer;
using Workshop.BusinessLogicLayer.Mappings;
using Workshop.DataAccessLayer;
using Workshop.DataTransferObjects;
using Workshop.DomainModels;

namespace Workshop.UnitTests;

public class ProjectServiceTests : IDisposable
{
  private readonly ProjectDbContext _context;
  private readonly IMapper _mapper;
  private readonly ProjectService _service;

  public ProjectServiceTests()
  {
    var options = new DbContextOptionsBuilder<ProjectDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

    _context = new ProjectDbContext(options);

    var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
    _mapper = config.CreateMapper();

    _service = new ProjectService(_context, _mapper);
  }

  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
  }

  // ── GetProjects ──

  [Fact]
  public async Task GetProjects_ReturnsEmpty_WhenNoProjectsExist()
  {
    var result = await _service.GetProjects();

    Assert.Empty(result);
  }

  [Fact]
  public async Task GetProjects_ReturnsAllProjects()
  {
    _context.Set<Project>().AddRange(
        new Project { Name = "Alpha" },
        new Project { Name = "Beta" });
    await _context.SaveChangesAsync();

    var result = (await _service.GetProjects()).ToList();

    Assert.Equal(2, result.Count);
    Assert.Contains(result, p => p.Name == "Alpha");
    Assert.Contains(result, p => p.Name == "Beta");
  }

  // ── GetProjectById ──

  [Fact]
  public async Task GetProjectById_ReturnsProject_WhenExists()
  {
    var project = new Project { Name = "Test", Description = "Desc" };
    _context.Set<Project>().Add(project);
    await _context.SaveChangesAsync();

    var result = await _service.GetProjectById(project.Id);

    Assert.NotNull(result);
    Assert.Equal("Test", result.Name);
    Assert.Equal("Desc", result.Description);
  }

  [Fact]
  public async Task GetProjectById_ReturnsNull_WhenNotFound()
  {
    var result = await _service.GetProjectById(999);

    Assert.Null(result);
  }

  // ── CreateProject ──

  [Fact]
  public async Task CreateProject_AddsToDatabase_AndReturnsDto()
  {
    var newDto = new ProjectNewDto { Name = "New Project", Description = "Details" };

    var result = await _service.CreateProject(newDto);

    Assert.Equal("New Project", result.Name);
    Assert.Equal("Details", result.Description);
    Assert.True(result.Id > 0);
    Assert.Single(_context.Set<Project>());
  }

  [Fact]
  public async Task CreateProject_WithoutDescription_SetsDescriptionNull()
  {
    var newDto = new ProjectNewDto { Name = "Minimal" };

    var result = await _service.CreateProject(newDto);

    Assert.Equal("Minimal", result.Name);
    Assert.Null(result.Description);
  }

  // ── UpdateProject ──

  [Fact]
  public async Task UpdateProject_ReturnsTrue_WhenProjectExists()
  {
    var project = new Project { Name = "Old" };
    _context.Set<Project>().Add(project);
    await _context.SaveChangesAsync();

    var updateDto = new ProjectUpdateDto
    {
      Id = project.Id,
      Name = "Updated",
      Description = "New desc"
    };

    var result = await _service.UpdateProject(updateDto);

    Assert.True(result);
    var updated = await _context.Set<Project>().FindAsync(project.Id);
    Assert.Equal("Updated", updated!.Name);
    Assert.Equal("New desc", updated.Description);
  }

  [Fact]
  public async Task UpdateProject_ReturnsFalse_WhenProjectNotFound()
  {
    var updateDto = new ProjectUpdateDto { Id = 999, Name = "Ghost" };

    var result = await _service.UpdateProject(updateDto);

    Assert.False(result);
  }

  // ── DeleteProject ──

  [Fact]
  public async Task DeleteProject_ReturnsTrue_AndRemovesFromDatabase()
  {
    var project = new Project { Name = "ToDelete" };
    _context.Set<Project>().Add(project);
    await _context.SaveChangesAsync();

    var result = await _service.DeleteProject(project.Id);

    Assert.True(result);
    Assert.Empty(_context.Set<Project>());
  }

  [Fact]
  public async Task DeleteProject_ReturnsFalse_WhenProjectNotFound()
  {
    var result = await _service.DeleteProject(999);

    Assert.False(result);
  }
}