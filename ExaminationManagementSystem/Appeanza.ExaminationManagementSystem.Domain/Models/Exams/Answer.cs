using Appeanza.ExaminationManagementSystem.Domain.Common;

namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Exams
{
    public class Answer : BaseEntity<int>
    {
        public required string Body { get; set; }
        public required bool IsRightAnswer { get; set; }
        public required int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
    }
}
