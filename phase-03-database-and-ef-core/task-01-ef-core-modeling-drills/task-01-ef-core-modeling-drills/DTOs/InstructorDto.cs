namespace task_01_ef_core_modeling_drills.DTOs
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<TrainingTrackDto> TrainingTracks { get; set; }
    }
}