using Eshop.Data;
using Eshop.Dto.RoleModel;
using Eshop.Migrations;

namespace Eshop.Repositries.Interface
{
    public interface IRoleRepository
    {
        Task<List<Roles>> Roles(CancellationToken cancellationToken);
        Task<Roles> GetROlesByID(Guid id, CancellationToken cancellationToken);
        Task<bool> AddRole(Roles role, CancellationToken cancellationToken);
        Task<bool> UpdateRole(Roles role, CancellationToken cancellationToken);
        Task<bool> DeleteRole(Guid id, CancellationToken cancellationToken);
        Task<bool> AssignRoleToUser(Users users,CreateRoleDto request);
        Task SaveChangesAsync();
        Task AddAsync(object user);
    }
}
