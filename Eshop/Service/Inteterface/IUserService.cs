using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.UserModel;

namespace Eshop.Service.Inteterface
{
    public interface IUserService
    {
        Task<BaseResponse<bool>> AddUser(CreateUserDto request, CancellationToken cancellationToken);
        Task<BaseResponse<List<Users>>> GetAllUser(CancellationToken cancellationToken);
        Task<BaseResponse<Users?>> GetUserById(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateUser(Guid userId, UpdateUserDto user, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteUser(Guid id, CancellationToken cancellationToken);
    }
}
