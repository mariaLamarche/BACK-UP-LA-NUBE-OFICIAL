// Models/RegisterRequest.cs
using System.ComponentModel.DataAnnotations;

namespace AuthSystemBackend.Models
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}