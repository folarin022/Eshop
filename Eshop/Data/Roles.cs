namespace Eshop.Data
{
    public class Roles : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
