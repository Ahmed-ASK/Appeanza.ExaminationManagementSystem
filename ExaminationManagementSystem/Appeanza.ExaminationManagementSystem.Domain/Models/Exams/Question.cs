using Appeanza.ExaminationManagementSystem.Domain.Common;

namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Exams
{
    public class Question : BaseEntity<int>
    {
        public required int ExamId { get; set; }
        public required string Body { get; set; }
        public required decimal Marks { get; set; }
        public required int QuestionNumber { get; set; } // Will Be Later to sort the questions based on that number and will help in pagenation if i applied it 
        public virtual Exam Exam { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = null!;
    }
}
