namespace task_01_ef_core_modeling_drills.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<TrainingTrack> TrainingTracks { get; set; } = new List<TrainingTrack>(); 
    }
}
