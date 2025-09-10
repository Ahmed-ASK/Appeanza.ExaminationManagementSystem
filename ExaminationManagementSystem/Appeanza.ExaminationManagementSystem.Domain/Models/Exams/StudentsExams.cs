namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Exams
{
    public class StudentsExams
    {
        public string UserId { get; set; } = null!;
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; } = null!;
        public DateTime SubmitAt { get; set; } = DateTime.UtcNow;
        public decimal Score { get; set; }
    }
}
