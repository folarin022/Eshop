using Azure.Core;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.UserModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user, CancellationToken cancellationToken)
        {
            await _userService.AddUser(user, cancellationToken);
            return Ok("User created successfully.");
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _userService.GetUserById(id, cancellationToken);

            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers([FromRoute] CancellationToken cancellationToken)
        {
            var response = await _userService.GetAllUser(cancellationToken);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Update/{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto user, CancellationToken cancellationToken)
        {
            

            var response = await _userService.UpdateUser(id, user, cancellationToken);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok("User updated successfully.");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteUser(id, cancellationToken);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok("User deleted successfully.");
        }
    }
}
