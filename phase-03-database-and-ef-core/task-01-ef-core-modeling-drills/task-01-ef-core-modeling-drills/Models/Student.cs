namespace task_01_ef_core_modeling_drills.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive  { get; set; }
        public StudentProfile studentProfile { get; set; }
    }
}
