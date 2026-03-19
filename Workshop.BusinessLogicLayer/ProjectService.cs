using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Workshop.DataAccessLayer;
using Workshop.DataTransferObjects;
using Workshop.DomainModels;

namespace Workshop.BusinessLogicLayer;

public class ProjectService(ProjectDbContext context, IMapper mapper) : ServiceBase(context, mapper), IProjectService
{
  public async Task<IEnumerable<ProjectDto>> GetProjects()
  {
    var projects = await _context.Set<Project>().ToListAsync();
    var dtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
    return dtos;
  }

  public async Task<ProjectDto?> GetProjectById(int id)
  {
    var project = await _context.Set<Project>().FindAsync(id);
    if (project == null)
    {
      return null;
    }
    var dto = _mapper.Map<ProjectDto>(project);
    return dto;
  }

  public async Task<ProjectDto> CreateProject(ProjectNewDto projectNew)
  {
    var project = _mapper.Map<Project>(projectNew);
    _context.Set<Project>().Add(project);
    await _context.SaveChangesAsync();
    var dto = _mapper.Map<ProjectDto>(project);
    return dto;
  }

  public async Task<bool> UpdateProject(ProjectUpdateDto projectUpdate)
  {
    var project = await _context.Set<Project>().FindAsync(projectUpdate.Id);
    if (project == null)
    {
      return false;
    }
    _mapper.Map(projectUpdate, project);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteProject(int id)
  {
    var project = await _context.Set<Project>().FindAsync(id);
    if (project == null)
    {
      return false;
    }
    _context.Set<Project>().Remove(project);
    await _context.SaveChangesAsync();
    return true;
  }
}

public abstract class ServiceBase
{
  protected readonly ProjectDbContext _context;
  protected readonly IMapper _mapper;

  protected ServiceBase(ProjectDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

}