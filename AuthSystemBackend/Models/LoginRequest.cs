// Models/LoginRequest.cs
using System.ComponentModel.DataAnnotations;

namespace AuthSystemBackend.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
