using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.CompanyAPI.Domain;

namespace ProjectManagement.CompanyAPI.Data.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<CompanyAPI.Domain.Company>
{
    public void Configure(EntityTypeBuilder<CompanyAPI.Domain.Company> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.ToTable("Company");

        builder
            .HasMany<Tag>(c => c.Tags)
            .WithMany(t => t.Companies);
    }
}