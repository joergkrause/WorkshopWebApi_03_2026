using System.ComponentModel.DataAnnotations;

namespace Workshop.DomainModels;

public abstract class EntityBase
{
  public int Id { get; set; }
}
