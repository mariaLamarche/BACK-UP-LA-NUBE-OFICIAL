// Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace AuthSystemBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
