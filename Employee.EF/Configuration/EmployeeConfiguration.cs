using Employee.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.EF.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Email);
            builder.Property(x => x.Gender);
            builder.Property(x => x.Designation);
            builder.Property(x => x.DOB);
            builder.Property(x => x.JoiningDate);
            builder.Property(x => x.Image);
            builder.Property(x => x.Description);

            builder
                .HasMany(x => x.Skills)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
