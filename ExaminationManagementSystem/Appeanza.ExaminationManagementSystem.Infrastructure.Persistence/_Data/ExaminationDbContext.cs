using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Exams;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Configurations.Groups;
using Microsoft.EntityFrameworkCore;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data
{
    public class ExaminationDbContext : DbContext
    {

        public ExaminationDbContext(DbContextOptions<ExaminationDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ExamConfigurations());
            modelBuilder.ApplyConfiguration(new StudentExamConfigurations());
            modelBuilder.ApplyConfiguration(new QuestionConfigurations());
            modelBuilder.ApplyConfiguration(new AnswerConfigurations());
            modelBuilder.ApplyConfiguration(new GroupConfigurations());
            modelBuilder.ApplyConfiguration(new StudentGroupConfigurations());
        }
    }
}
