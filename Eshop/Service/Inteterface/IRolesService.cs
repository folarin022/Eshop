using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.RoleModel;

namespace Eshop.Service.Inteterface
{
    public interface IRolesService
    {
        Task<BaseResponse<List<Roles>>> GetAllRoles(CancellationToken cancellationToken);
        Task<BaseResponse<bool>> AssignRolesToUser(CreateRoleDto request,CancellationToken cancellationToken);
    }
}
