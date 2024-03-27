using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C4.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C4.G02.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Empolyee>
    {
        public void Configure(EntityTypeBuilder<Empolyee> builder)
        {
            // Fluent APIS For "Employee" Domain

            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E => E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Genderr)
                   .HasConversion(

                    (Gender) => Gender.ToString(),
                    (GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString , true)
                );

            builder.Property(E => E.EmployeeType)
                  .HasConversion(

                   (Type) => Type.ToString(),
                   (TypeAsString) => (EmpType)Enum.Parse(typeof(EmpType), TypeAsString, true)
               );



        }
    }
}
