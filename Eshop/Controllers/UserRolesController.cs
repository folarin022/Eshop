using Eshop.Data;
using Eshop.Dto.RoleModel;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IRolesService _roleService;

        public UserRolesController(IRolesService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllRoles(cancellationToken);
            return Ok(result);
        }

        [HttpPost("assign/{userId:guid}")]
        public async Task<IActionResult> AssignRoleToUser(
            [FromRoute] Guid userId,
            [FromBody] CreateRoleDto request,
            CancellationToken cancellationToken)
        {
            var result = await _roleService.AssignRolesToUser(userId, request, cancellationToken);
            return Ok(result);
        }
    }
}
