using Appeanza.ExaminationManagementSystem.Domain.Entities.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Exams
{
    public class AnswerConfigurations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(A => A.Id);
            
            builder.Property(Q => Q.Id)
                .UseIdentityColumn();

            builder.Property(A => A.Body)
                .HasColumnType("NVARCHAR")
                .IsRequired();

            builder.Property(A => A.IsRightAnswer)
                .HasColumnType("BIT")
                .IsRequired();
            
            builder.HasOne(A => A.Question)
                .WithMany(Q => Q.Answers)
                .HasForeignKey(A => A.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
