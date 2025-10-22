using Eshop.Data;

namespace Eshop.Dto.UserModel
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? OtherName { get; set; }
        public string PhoneNumber { get; set; } 
        public required string Email { get; set; }
        public string Address { get; set; }

        public Gender Gender { get; set; }
    }
}
