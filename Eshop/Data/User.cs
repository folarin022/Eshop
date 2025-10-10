using System.Reflection;

namespace Eshop.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string? OtherName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
    }
}
