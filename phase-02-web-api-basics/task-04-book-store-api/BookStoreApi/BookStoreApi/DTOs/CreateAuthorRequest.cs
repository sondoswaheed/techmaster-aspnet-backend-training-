using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class CreateAuthorRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }
    }
}
