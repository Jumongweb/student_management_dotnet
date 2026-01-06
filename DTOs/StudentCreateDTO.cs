using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DTOs
{
    public class StudentCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Course { get; set; } = string.Empty;
    }
}
