using EmployeeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Infrastructure.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Specify the table name
            builder.ToTable("Employees");

            // Configure primary key
            builder.HasKey(e => e.Id);

            // Configure first name
            builder.Property(e => e.FirstName)
                .IsRequired() // Makes the property required (NOT NULL)
                .HasMaxLength(50); // Limits the length to 50 characters

            // Configure last name
            builder.Property(e => e.LastName)
                .IsRequired() // Makes the property required
                .HasMaxLength(50); // Limits the length to 50 characters

            // Configure email
            builder.Property(e => e.Email)
                .IsRequired() // Makes the property required
                .HasMaxLength(100); // Limits the length to 100 characters

            // Unique index on email with a name for the index
            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Employees_Email"); // Naming the index explicitly

            // Configure position
            builder.Property(e => e.Position)
                .IsRequired() // Makes the property required
                .HasMaxLength(100); // Limits the length to 100 characters

            // Configure date of joining
            builder.Property(e => e.DateOfJoining)
                .IsRequired(); // Makes the property required
        }
    }
}
