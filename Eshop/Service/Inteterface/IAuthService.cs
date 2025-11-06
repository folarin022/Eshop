using Eshop.Dto;
using Eshop.Dto.AuthModel;

namespace Eshop.Service.Inteterface
{
    public interface IAuthService
    {
         Task<BaseResponse<bool>> RegisterUser(RegisterUserDto request, CancellationToken cancellationToken);
        Task<BaseResponse<string>> LoginClients (LoginUserDto request, CancellationToken cancellationToken);
    }
}
