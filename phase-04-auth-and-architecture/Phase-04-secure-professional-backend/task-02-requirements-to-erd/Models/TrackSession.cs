using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.Models
{
    public class TrackSession
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public DateTime SessionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int TrainingTrackId { get; set; }

        public TrainingTrack TrainingTrack { get; set; } = null!;
    }
}