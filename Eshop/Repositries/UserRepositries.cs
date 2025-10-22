using Azure.Core;
using Eshop.Context;
using Eshop.Data;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repositries
{
    public class userRepositries : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public userRepositries(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Users user)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddUser(Users user, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(user, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user == null)
                return false;
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Users>> GetAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserByID(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(new object[] { userId }, cancellationToken);
            if (user == null)
                return;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
