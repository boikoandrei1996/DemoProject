using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  internal sealed class ContentGroupConfiguration : IEntityTypeConfiguration<ContentGroup>
  {
    public void Configure(EntityTypeBuilder<ContentGroup> builder)
    {
      builder.Property(x => x.Title)
        .HasMaxLength(100);
    }
  }
}
