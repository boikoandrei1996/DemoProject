using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class DiscountConfiguration : IEntityTypeConfiguration<Discount>
  {
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
      builder
        .HasIndex(x => x.Title)
        .IsUnique();

      builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.Order)
        .IsRequired();
    }
  }
}
