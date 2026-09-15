namespace task_01_ef_core_modeling_drills.DTOs
{
    public class CreateStudentDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
