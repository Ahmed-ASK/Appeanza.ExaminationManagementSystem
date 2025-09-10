using Appeanza.ExaminationManagementSystem.Domain.Emtities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DisplayName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
