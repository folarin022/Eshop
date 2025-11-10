using Eshop.Dto;
using Eshop.Dto.AuthModel;
using EssenceShop.Models;

namespace Eshop.Service.Inteterface
{
    public interface IAuthService
    {
         Task<BaseResponse<bool>> RegisterUser(RegisterUserDto request, CancellationToken cancellationToken);
        Task<BaseResponse<TokenResponse>> LoginClients (LoginUserDto request, CancellationToken cancellationToken);
    }
}
