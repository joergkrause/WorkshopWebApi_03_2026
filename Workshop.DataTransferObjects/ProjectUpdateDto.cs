using System.ComponentModel.DataAnnotations;

namespace Workshop.DataTransferObjects;

public class ProjectUpdateDto : DtoBase
{
  [Required, StringLength(30)]
  public required string Name { get; set; }

  [StringLength(1000)]
  public string? Description { get; set; }
}