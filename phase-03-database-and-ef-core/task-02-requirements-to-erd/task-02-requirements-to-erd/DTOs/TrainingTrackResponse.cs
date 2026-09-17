using System.ComponentModel.DataAnnotations;
using task_02_requirements_to_erd.Models;
using task_02_requirements_to_erd.Models.Enums;

namespace task_02_requirements_to_erd.DTOs
{
    public class TrainingTrackResponse
    {
        public int TrainingTrackId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public int Capacity { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public TrainingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int InstructorId { get; set; }
        public int RemainingCapacity { get; set; }
        public int EnrolledCount { get; set; }
        public string InstructorName { get; set; }
    }
}
