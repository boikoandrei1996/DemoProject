using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  class InfoObjectConfiguration : IEntityTypeConfiguration<InfoObject>
  {
    public void Configure(EntityTypeBuilder<InfoObject> builder)
    {
      builder.Property(x => x.Content)
        .IsRequired();
    }
  }
}
