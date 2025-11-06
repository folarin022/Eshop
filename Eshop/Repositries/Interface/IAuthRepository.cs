using Eshop.Dto.AuthModel;

namespace Eshop.Repositries.Interface
{
    public interface IAuthRepository
    {
        Task<bool> Register(RegisterUserDto request, CancellationToken cancellationToken);
        Task<bool> Login(LoginUserDto request, CancellationToken cancellationToken);
    }
}


