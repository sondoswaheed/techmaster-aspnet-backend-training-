
using System.ComponentModel.DataAnnotations;

namespace task_02_requirements_to_erd.Models;

public class ActivityLog
{
    public int Id { get; set; }

    [MaxLength(450)]
    public string? UserId { get; set; }

    [MaxLength(50)]
    public string UserRole { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string EntityName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? EntityId { get; set; }

    [Required, MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(45)]
    public string? IpAddress { get; set; }

    public string? Metadata { get; set; }

    [MaxLength(100)]
    public string? RequestId { get; set; }
}
