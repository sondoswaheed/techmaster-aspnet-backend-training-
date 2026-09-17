using System.ComponentModel.DataAnnotations;
using task_02_requirements_to_erd.Models;

namespace task_02_requirements_to_erd.DTOs
{
    public class InstructorTracksDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Bio { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<TrainingTrackResponse> TrainingTracks { get; set; } = new List<TrainingTrackResponse>();
    }
}
