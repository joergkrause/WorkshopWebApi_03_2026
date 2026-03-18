namespace Workshop.DomainModels;

public class Project : EntityBase
{
  public string Name { get; set; } = null!;

  public string? Description { get; set; }
}