namespace Appeanza.ExaminationManagementSystem.Domain.Entities.Groups
{


    public class StudentGroup
    {

        public required int GroupId{ get; set; }
        public required string StudentId{ get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.UtcNow; // TODO : Edit This Later and initialize the value with interceptor
        public virtual Group Group { get; set; } = null!;

    }
}
