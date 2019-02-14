using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  class ChangeHistoryConfiguration : IEntityTypeConfiguration<ChangeHistory>
  {
    public void Configure(EntityTypeBuilder<ChangeHistory> builder)
    {
      builder
        .Property(x => x.Table)
        .IsRequired();

      builder
        .Property(x => x.Action)
        .IsRequired();
    }
  }
}
