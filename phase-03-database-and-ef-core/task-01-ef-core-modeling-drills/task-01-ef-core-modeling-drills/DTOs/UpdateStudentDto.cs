namespace task_01_ef_core_modeling_drills.DTOs
{
    public class UpdateStudentDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
