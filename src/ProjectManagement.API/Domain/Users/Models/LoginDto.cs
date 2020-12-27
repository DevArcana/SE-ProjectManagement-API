using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.API.Domain.Users.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Provide either username or email as login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}