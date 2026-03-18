namespace Workshop.DataTransferObjects;

public class ProjectDto : DtoBase
{
  public string Name { get; set; } = null!;

  public string? Description { get; set; }
}
