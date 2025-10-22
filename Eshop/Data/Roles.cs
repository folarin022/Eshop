namespace Eshop.Data
{
    public class Roles : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
