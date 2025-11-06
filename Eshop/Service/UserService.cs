using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.ProductModel;
using Eshop.Dto.UserModel;
using Eshop.Migrations;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;
using Users = Eshop.Data.Users;

namespace Eshop.Service
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger, ApplicationDbContext dbContext) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly ILogger<UserService> logger = logger;
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task<BaseResponse<bool>> AddUser(CreateUserDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {  
                var user = new Data.Users
                {
                    UserId = Guid.NewGuid(),
                    //roles = Roles.roles()
                    FirstName = request.FirstName,
                    Lastname = request.LastName,
                    OtherName = request.OtherName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Gender = request.Gender
                };

                await userRepository.AddUser(user, cancellationToken);
                await userRepository.AddAsync(user);
                await userRepository.SaveChangesAsync();

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "User created successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error creating user: {ex.Message}";
            }

            return response;
        }

        
        public async Task<BaseResponse<bool>> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await userRepository.DeleteUser(id, cancellationToken);
                if (!isDeleted)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Failed to delete user",
                        Data = false
                    };
                }
                


                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "User deleted successfully",
                    Data = true
                };


            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting user");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while deleting the user",
                    Data = false
                };
            }
        }

        
        public async Task<BaseResponse<Data.Users>> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Data.Users>();

            try
            {
                var user = await userRepository.GetUserByID(id, cancellationToken);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = user;
                response.Message = "User retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving user: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<List<Data.Users>>> GetAllUser(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<Data.Users>>();

            try
            {
                var users = await userRepository.GetAllUser();

                response.IsSuccess = true;
                response.Data = users;
                response.Message = "Users retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error retrieving users: {ex.Message}";
            }

            return response;
        }


        public async Task<BaseResponse<bool>> UpdateUser(Guid userId, UpdateUserDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var users = await userRepository.GetUserByID(userId, cancellationToken);
                if (users == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found";
                    return response;
                }


                users.FirstName = request.FirstName;
                users.Lastname = request.LastName;
                users.OtherName = request.OtherName;
                users.PhoneNumber = request.PhoneNumber;
                users.Email = request.Email;
                users.Address = request.Address;
                users.Gender = request.Gender;


                _ = userRepository.UpdateUser(userId, cancellationToken);
                //await userRepository.AddAsync(user);
                //await userRepository.SaveChangesAsync();

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error updating user: {ex.Message}";
            }

            return response;
        }

        

        //public Task<BaseResponse<bool>> GetAllUser(Guid id, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
