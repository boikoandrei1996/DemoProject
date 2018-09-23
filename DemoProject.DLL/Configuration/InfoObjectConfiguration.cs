using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class InfoObjectConfiguration : IEntityTypeConfiguration<InfoObject>
  {
    public void Configure(EntityTypeBuilder<InfoObject> builder)
    {
      builder.Property(x => x.Content)
        .IsRequired();

      builder.Property(x => x.SubOrder)
        .IsRequired();
    }
  }
}
