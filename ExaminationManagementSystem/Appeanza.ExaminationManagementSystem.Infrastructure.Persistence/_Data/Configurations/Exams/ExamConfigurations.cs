using Microsoft.EntityFrameworkCore;
using Appeanza.ExaminationManagementSystem.Domain.Entities.Exams;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Exams
{
    public class ExamConfigurations : IEntityTypeConfiguration<Exam>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Exam> builder)
        {

            builder.Property(E => E.Id)
                .UseIdentityColumn();
        
            builder.HasKey(E => E.Id);

            builder.Property(E => E.Title)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(E => E.TimeInminutes)
                .HasColumnType("INT");

            builder.Property(E => E.CreationDate)
                .HasColumnType("DATETIME")
                .HasComputedColumnSql("SYSUTCDATETIME()");

            builder.Property(E => E.Status)
                .HasColumnType("INT");

            builder.Property(E => E.PublishedAt)
                .HasColumnType("DATETIME")
                .IsRequired(true);

            builder.HasOne(e => e.Group)
                .WithMany(g => g.Exams)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasCheckConstraint("CK_Exam_Status", "[Status] >= 0 AND [Status] <= 4");
        }
    }
}
