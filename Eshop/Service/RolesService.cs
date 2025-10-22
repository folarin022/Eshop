using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.RoleModel;
using Eshop.Migrations;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service
{
    public class RolesService(IRoleRepository roleRepository, ILogger<RolesService> logger, ApplicationDbContext dbContext) : IRolesService
    {
        private readonly IRoleRepository roleRepository = roleRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RolesService> logger;

        //public RoleService(ApplicationDbContext dbContext, ILogger<RoleService> logger)
        //{
        //    this.dbContext = dbContext;
        //    this.logger = logger;
        //}

        public async Task<BaseResponse<bool>> AssignRolesToUser(CreateRoleDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var role = new Data.Roles
                {
                    RoleName = request.RoleName
                };


                await roleRepository.AssignRoleToUser( request, cancellationToken);
                await roleRepository.AddAsync(request);
                await roleRepository.SaveChangesAsync();

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Role Assignd successful";
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error Assigning roles: {ex.Message}";
            }
            return response;
        }

        public async Task<BaseResponse<List<Roles>>> GetAllRoles(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<Roles>>();
            try
            {
                var roles = await dbContext.Roles.ToListAsync(cancellationToken);
                response.IsSuccess = true;
                response.Data = roles;
                response.Message = "Roles retrieved successfully";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving roles");
                response.IsSuccess = false;
                response.Message = $"Error retrieving roles: {ex.Message}";
            }
            return response;
        }
    }
}

