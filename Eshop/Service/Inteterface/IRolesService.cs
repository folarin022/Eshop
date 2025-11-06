using Eshop.Dto.RoleModel;
using Eshop.Dto;
using System.Threading;
using Eshop.Data;

namespace Eshop.Service.Inteterface
{
    public interface IRolesService
    {
        Task<BaseResponse<List<Roles>>> GetAllRoles(CancellationToken cancellationToken);
        Task<BaseResponse<bool>> AssignRolesToUser(Guid userId, CreateRoleDto request, CancellationToken cancellationToken);
    }
}
