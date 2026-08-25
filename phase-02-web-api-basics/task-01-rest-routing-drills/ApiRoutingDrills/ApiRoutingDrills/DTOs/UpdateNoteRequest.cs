using System.ComponentModel.DataAnnotations;

namespace ApiRoutingDrills.DTOs
{
    public class UpdateNoteRequest
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
