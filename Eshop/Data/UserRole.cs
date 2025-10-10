namespace Eshop.Data
{
    public class UserRole : BaseEntity
    {
        public int RoleId { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public Roles Roles { get; set; }
    }
}
