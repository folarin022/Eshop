using Eshop.Data;
using Eshop.Dto.RoleModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IRolesService roleService;

        public UserRolesController(IRolesService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var result = await roleService.GetAllRoles(cancellationToken);
            return Ok(result);
        }
        [HttpPost("{UserId:guid}")]
        public async Task<IActionResult> AssignRoleToUser(CreateRoleDto request, CancellationToken cancellationToken)
        {
            var resut = await roleService.AssignRolesToUser(request, cancellationToken);           
            return Ok (resut);
        }
    }
}
