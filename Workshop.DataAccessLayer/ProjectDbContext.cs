using Microsoft.EntityFrameworkCore;
using Workshop.DomainModels;

namespace Workshop.DataAccessLayer;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{

  public override int SaveChanges()
  {
    throw new NotSupportedException("Sync calls are strictly forbidden");
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Project>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
      entity.Property(e => e.Description).HasMaxLength(1000).IsRequired(false);
    });

  }
}
