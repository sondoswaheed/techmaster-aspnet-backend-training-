namespace task_02_requirements_to_erd.DTOs
{
    public class UnpaidEnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; }
    }
}
