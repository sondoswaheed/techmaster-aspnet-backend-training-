using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }  
        public DateTime CreatedAt { get; set; }
        public ICollection<TrainingTrack> TrainingTracks { get; set; } = new List<TrainingTrack>();
    }
}
