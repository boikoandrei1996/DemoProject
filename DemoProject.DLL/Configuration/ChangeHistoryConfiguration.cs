using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class ChangeHistoryConfiguration : IEntityTypeConfiguration<ChangeHistory>
  {
    public void Configure(EntityTypeBuilder<ChangeHistory> builder)
    {
      builder
        .Property(x => x.TableName)
        .IsRequired();

      builder
        .Property(x => x.TimeOfChange)
        .IsRequired();
    }
  }
}
