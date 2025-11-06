using System.ComponentModel.DataAnnotations;

namespace Eshop.Dto.AuthModel
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Invalid email address.")]
        public string Username { get; set; } = string.Empty;

        
        [MinLength(8, ErrorMessage = "Invaid password format")]
        public string Password { get; set; } = string.Empty;
    }
}
