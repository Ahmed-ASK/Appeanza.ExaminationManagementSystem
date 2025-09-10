using Appeanza.ExaminationManagementSystem.Domain.Entities.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Exams
{
    public class StudentExamConfigurations : IEntityTypeConfiguration<StudentsExams>
    {
        public void Configure(EntityTypeBuilder<StudentsExams> builder)
        {
            builder.HasKey(nameof(StudentsExams.UserId), nameof(StudentsExams.ExamId));

            builder.Property(E => E.SubmitAt)
                .HasColumnType("DATETIME")
                .HasComputedColumnSql("SYSUTCDATETIME()")
                .IsRequired();

            builder.Property(E => E.Score)
                .HasColumnType("DECIMAL(5,2)")
                .IsRequired();
            builder.HasOne(se => se.Exam)
                .WithMany(e => e.SubmittedExams)
                .HasForeignKey(se => se.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
