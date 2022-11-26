using System.ComponentModel.DataAnnotations;

namespace WorkHours.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$")]
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
