namespace task_02_requirements_to_erd.DTOs
{
    public class TrackProgressResponse
    {
        public int TrainingTrackId { get; set; }

        public string TrackTitle { get; set; } = string.Empty;

        public int TotalEnrollments { get; set; }

        public int ActiveEnrollments { get; set; }

        public int CompletedEnrollments { get; set; }

        public decimal AverageProgress { get; set; }
    }
}