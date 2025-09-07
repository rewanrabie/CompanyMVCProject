using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_3_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_DAL.Data.Configurations
{
    public class Employee_Configuration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            builder.HasOne(E => E.WorkFor)
                .WithMany(E => E.Employees)
                .HasForeignKey(E => E.WorkForId)
                .OnDelete(DeleteBehavior.SetNull);
            // builder.Property(E => E.Salary).HasColumnType("decimal(18.2)");
            // builder.HasOne(E => E.Department)
            //.WithMany(E => E.Employees)
            // .HasForeignKey(E => E.DepartmentId)
            // .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
