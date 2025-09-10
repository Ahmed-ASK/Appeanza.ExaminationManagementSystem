using Appeanza.ExaminationManagementSystem.Domain.Common;
using Appeanza.ExaminationManagementSystem.Domain.Entities.Exams;

namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Groups
{
    public class Group : BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public required string TeacherId { get; set; }
        public required bool IsDeleted { get; set; }
        public virtual StudentGroup? StudentGroup { get; set; } = null!;
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
