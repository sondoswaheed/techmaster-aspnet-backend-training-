namespace task_01_ef_core_modeling_drills.Models
{
    public class TrainingTrack
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
