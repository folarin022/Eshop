using Eshop.Context;
using Eshop.Dto.AuthModel;
using Eshop.Repositries.Interface;

namespace Eshop.Repositries
{
    public class AuthRepositries(ApplicationDbContext dbContext) : IAuthRepository
    {
        public async Task<bool> Login(LoginUserDto request, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(request, cancellationToken);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Register(RegisterUserDto request, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(request, cancellationToken);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
