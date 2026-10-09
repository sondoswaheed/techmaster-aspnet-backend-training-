using System.ComponentModel.DataAnnotations;

public class UpdateMyInstructorProfileDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    public string? Bio { get; set; }

    public string? Specialization { get; set; }
}