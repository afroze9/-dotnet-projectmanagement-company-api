using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectManagement.Company.Api.Data.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Company>
{
    public void Configure(EntityTypeBuilder<Domain.Company> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();
    }
}