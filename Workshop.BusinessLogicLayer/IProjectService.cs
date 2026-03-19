using Workshop.DataTransferObjects;

namespace Workshop.BusinessLogicLayer
{
  public interface IProjectService
  {
    Task<ProjectDto> CreateProject(ProjectNewDto projectNew);
    Task<bool> DeleteProject(int id);
    Task<ProjectDto?> GetProjectById(int id);
    Task<IEnumerable<ProjectDto>> GetProjects();
    Task<bool> UpdateProject(ProjectUpdateDto projectUpdate);
  }
}