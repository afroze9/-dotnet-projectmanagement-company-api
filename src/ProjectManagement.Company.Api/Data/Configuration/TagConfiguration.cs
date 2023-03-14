using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.CompanyAPI.Domain.Entities;

namespace ProjectManagement.CompanyAPI.Data.Configuration;

[ExcludeFromCodeCoverage]
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.ToTable("Tag");

        builder
            .HasMany<Company>(t => t.Companies)
            .WithMany(c => c.Tags);
    }
}