using System.ComponentModel.DataAnnotations;

namespace Eshop.Dto.AuthModel
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Iinvalid email format")]
        public string Email { get; set; } = string.Empty;
        [MinLength(8, ErrorMessage = "Invaid password format")]
        public string Password { get; set; } = string.Empty;
    }
}

