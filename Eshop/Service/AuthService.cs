using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.AuthModel;
using Eshop.Repositries;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;
using EssenceShop.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Eshop.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtService _jwtService;
        private readonly ApplicationDbContext _dbContext;

        private readonly string _hmacKey = "ThisIsASecretKeyForHMAC";

        public AuthService(ApplicationDbContext dbContext, JwtService jwtService, ILogger<AuthService> logger)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<BaseResponse<string>> LoginClients(LoginUserDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>();

            try
            {
                var client = await _dbContext.Auths.FirstOrDefaultAsync(a => a.UserName == request.Username, cancellationToken);
                if (client == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Clients not found.";
                    return response;
                }

                if (!VerifyPassword(request.Password, client.Password))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid password.";
                    return response;
                }

                var token = _jwtService.GenerateToken(client.UserName, client.Role);
                _logger.LogInformation("Admin '{Username}' logged in successfully.", client.UserName);

                response.IsSuccess = true;
                response.Message = "Login successful.";
                response.Data = token;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in clients");
                return new BaseResponse<string>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<BaseResponse<bool>> RegisterUser(RegisterUserDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                {
                    response.IsSuccess = false;
                    response.Message = "Username and password are required.";
                    return response;
                }

                if (await _dbContext.Auths.AnyAsync(a => a.UserName == request.Username, cancellationToken))
                {
                    response.IsSuccess = false;
                    response.Message = "Clients already exists.";
                    return response;
                }

                if (!ValidationService.IsValidEmail(request.Email))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid email format.";
                    return response;
                }

                if (!ValidationService.IsValidPassword(request.Password))
                {
                    response.IsSuccess = false;
                    response.Message = "Password must contain lowercase letters, numbers, and a symbol.";
                    return response;
                }

                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hmacKey));
                var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

                var auth = new Auth
                {
                    UserName = request.Username,
                    Password = passwordHash,
                    Email = request.Email,
                    Role = "Clients"
                };

                _dbContext.Auths.Add(auth);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("New admin '{Username}' registered successfully at {Time}", request.Username, DateTime.UtcNow);

                response.IsSuccess = true;
                response.Message = "Clients registered successfully!";
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering clients");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Data = false
                };
            }
        }





        private bool VerifyPassword(string password, string storedHash)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hmacKey));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }

        private string HashPassword(string password)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hmacKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

    }
}


