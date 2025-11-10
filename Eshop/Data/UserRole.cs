namespace Eshop.Data
{
    public class UserRole : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public Auth User { get; set; }
        public Roles Roles { get; set; }
    }
}
