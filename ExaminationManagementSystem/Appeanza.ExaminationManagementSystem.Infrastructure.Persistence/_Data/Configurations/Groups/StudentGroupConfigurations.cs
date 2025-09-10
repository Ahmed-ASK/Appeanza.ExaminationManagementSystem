using Appeanza.ExaminationManagementSystem.Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Groups
{
    public class StudentGroupConfigurations : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(nameof(StudentGroup.StudentId), nameof(StudentGroup.GroupId));

            builder.Property(SG => SG.JoinedDate)
                .HasColumnType("DATETIME")
                .HasComputedColumnSql("SYSUTCDATETIME()");
        
            
        }
    }
}
