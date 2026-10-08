namespace task_02_requirements_to_erd.DTOs
{
    public class TrackCapacityDto
    {
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; }
        public int Capacity { get; set; }
        public int EnrolledCount { get; set; }
        public int RemainingCapacity { get; set; }
    }
}
