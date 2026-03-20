using System.ComponentModel.DataAnnotations;
using Workshop.DataTransferObjects.Validation;

namespace Workshop.DataTransferObjects;

public class ProjectNewDto
{
  [Required, StringLength(30, ErrorMessage = "19999")]
  [IgzCustomerNumber(Prefix = "PE")]
  public required string Name { get; set; }

  [StringLength(1000)]
  public string? Description { get; set; }
}
