namespace task_02_requirements_to_erd.DTOs
{
    public class AvailableSeatsDto
    {
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; }
        public int Capacity { get; set; }
        public int ActiveEnrollments { get; set; }
        public int RemainingSeats { get; set; }
    }
}