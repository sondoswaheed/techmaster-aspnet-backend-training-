
namespace task_01_ef_core_modeling_drills.Models
{
    public class StudentProfile
    {
        public int Id {get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string EmergencyPhone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int StudentId { get; set; }
        public Student student { get; set; }

    }
}
