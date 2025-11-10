using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.AuthModel;
using Eshop.Service.Inteterface;
using EssenceShop.Models;
using EssenceShop.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace Eshop.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly string _hmacKey = "ThisIsASecretKeyForHMAC";

        public AuthService(ApplicationDbContext dbContext, JwtService jwtService, ILogger<AuthService> logger)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<BaseResponse<TokenResponse>> LoginClients(LoginUserDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<TokenResponse>();

            try
            {
                var user = await _dbContext.Auths
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Roles)
                    .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                    return response;
                }


                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

                if (!isPasswordValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid password.";
                    return response;
                }

                var roles = user.UserRoles.Select(ur => ur.Roles.Role).ToList();

                var accessToken = _jwtService.GenerateToken(user.UserName, roles);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("User '{Username}' logged in successfully.", user.UserName);

                response.IsSuccess = true;
                response.Message = "Login successful.";
                response.Data = new TokenResponse
                {
                    AccessToken = accessToken,
                    ExpiresAt = DateTime.Now.AddHours(2),
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryDate = DateTime.Now.AddDays(7)
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user");
                return new BaseResponse<TokenResponse>
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
                    response.Message = "User already exists.";
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

                var passwordHash = HashPassword(request.Password);

                var auth = new Auth
                {
                    UserName = request.Username,
                    Email = request.Email,
                    Password = passwordHash
                };

                await _dbContext.Auths.AddAsync(auth, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // Assign default role "User"
                var userRoleEntity = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Role == "User", cancellationToken);
                if (userRoleEntity != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = auth.Id,
                        RoleId = userRoleEntity.Id
                    };
                    await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                _logger.LogInformation("New user '{Username}' registered successfully.", request.Username);

                response.IsSuccess = true;
                response.Message = "User registered successfully!";
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
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
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
