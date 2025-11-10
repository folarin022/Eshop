using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.RoleModel;
using Eshop.Service.Inteterface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _dbContext;

        public RolesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<List<Roles>>> GetAllRoles(CancellationToken cancellationToken)
        {
            var roles = await _dbContext.Roles
                .Select(r => new Roles  
                {
                    Id = r.Id,
                    UserRoles = r.UserRoles
                })
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<Roles>>
            {
                IsSuccess = true,
                Message = "Roles retrieved successfully.",
                Data = roles
            };
        }

        public async Task<BaseResponse<bool>> AssignRolesToUser(Guid userId, CreateRoleDto request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
            {
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.Role == request.RoleName, cancellationToken);

            if (role == null)
            {
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Role not found"
                };
            }
            if (user.UserRoles.Any(ur => ur.RoleId == role.Id))
            {
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User already has this role"
                };
            }

            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new BaseResponse<bool>
            {
                IsSuccess = true,
                Message = "Role assigned successfully.",
                Data = true
            };
        }

    }
}
