namespace task_02_requirements_to_erd.DTOs
{
    public class StudentWithoutPaymentDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public int EnrollmentId { get; set; }
        public int TrainingTrackId { get; set; }
    }
}
