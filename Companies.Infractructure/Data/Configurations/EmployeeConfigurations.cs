using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infractructure.Data.Configurations;

public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

       // builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.Age)
            .IsRequired()
            .HasMaxLength(90);

        builder.Property(e => e.Position)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.CompanyId)
            .IsRequired();

        //Relationship already configured in CompanyConfiguration
    }
}
