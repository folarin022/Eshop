using Eshop.Data;

namespace Eshop.Repositries.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(Users user);
        Task AddAsync(Guid id);

        //Task AddAsync(Guid id);
        Task<bool> AddUser(Users user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken);
        Task<List<Users>> GetAllUser();
        Task<Users?> GetUserByID(Guid userId, CancellationToken cancellationToken);
        Task SaveChangesAsync();
        Task UpdateUser(Guid userId, CancellationToken cancellationToken);
    }
}
