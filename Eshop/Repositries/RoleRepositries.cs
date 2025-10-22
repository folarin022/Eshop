using Eshop.Data;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;
using Eshop.Context;
using Eshop.Dto.RoleModel;

namespace Eshop.Repositries
{
    public class RoleRepositries(ApplicationDbContext dbContext) : IRoleRepository
    {
        public Task AddAsync(object user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddRole(Roles role, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(role, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;

        }

        public async Task<bool> AssignRoleToUser( CreateRoleDto request, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(request);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            dbContext.Remove(id);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;      
        }

        public async Task<Roles> GetROlesByID(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Roles.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<Roles>> Roles(CancellationToken cancellationToken)
        {
            return await dbContext.Roles.ToListAsync(cancellationToken);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRole(Roles role, CancellationToken cancellationToken)
        {
            dbContext.Roles.Update(role);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
