using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class ContentGroupConfiguration : IEntityTypeConfiguration<ContentGroup>
  {
    public void Configure(EntityTypeBuilder<ContentGroup> builder)
    {
      builder.Property(x => x.Title)
        .HasMaxLength(100);
    }
  }
}
