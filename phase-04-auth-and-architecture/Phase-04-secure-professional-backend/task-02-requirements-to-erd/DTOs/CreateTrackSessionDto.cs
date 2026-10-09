using System.ComponentModel.DataAnnotations;

public class CreateTrackSessionDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateTime SessionDate { get; set; }
}