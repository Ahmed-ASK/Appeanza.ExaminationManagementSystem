using Appeanza.ExaminationManagementSystem.Domain.Common;
using Appeanza.ExaminationManagementSystem.Domain.Entities.Enums;
using Appeanza.ExaminationManagementSystem.Domain.Entities.Groups;

namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Exams
{
    public class Exam : BaseEntity<int>
    {

        public required string Title { get; set; }
        public required int TimeInminutes { get; set; }
        public required DateTime CreationDate { get; set; }
        public DateTime? PublishedAt { get; set; }
        public required ExamStatus Status { get; set; } = (ExamStatus)1;
        public required int GroupId { get; set; }
        public required bool IsDeleted { get; set; }
        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<StudentsExams> SubmittedExams { get; set; } = new List<StudentsExams>();
        public virtual ICollection<Question>? Questions { get; set; }

    }
}
