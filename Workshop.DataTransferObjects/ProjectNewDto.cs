using System.ComponentModel.DataAnnotations;

namespace Workshop.DataTransferObjects;

public class ProjectNewDto
{
  [Required]
  public required string Name { get; set; }
  public string? Description { get; set; }
}
