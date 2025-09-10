using Appeanza.ExaminationManagementSystem.Domain.Entities.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Exams
{
    public class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {

            
            builder.HasKey(Q => Q.Id);

            builder.Property(Q => Q.Id)
                .UseIdentityColumn();

            builder.Property(Q => Q.Body)
                .HasColumnType("NVARCHAR")
                .IsRequired();

            builder.Property(Q => Q.Marks)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired();

            builder.Property(Q => Q.QuestionNumber)
                .IsRequired();

            builder.HasOne(Q => Q.Exam)
                .WithMany(E => E.Questions)
                .HasForeignKey(Q => Q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(Q => Q.Answers)
                .WithOne(A => A.Question)
                .HasForeignKey(A => A.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
