using Eshop.Data;
using Eshop.Dto.ProductModel;

namespace Eshop.Dto.UserModel
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string? OtherName { get; set; }
        public required string Email { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Address{ get; set; }
        public Gender Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        public List<Guid> RoleIds { get; set; }

        //public static implicit operator UserDto(Task<BaseResponse<UserDto>> v)
        //{
        //    throw new NotImplementedException();
        //}

        //public static implicit operator UserDto(bool v)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
