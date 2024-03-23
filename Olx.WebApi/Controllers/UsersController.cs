using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olx.Service.DTOs.Users;
using Olx.Service.Interfaces;
using Olx.Service.DTOs.PropertyValues;
using Olx.Service.Services;

namespace Olx.WebApi.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewDto>> GetUserById(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserViewDto>> AddUser(UserCreateDto userCreateDto)
        {
            try
            {
                var addedUser = await _userService.CreateAsync(userCreateDto);
                return Ok(addedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserViewDto>> UpdateUser(long id, UserUpdateDto userUpdateDto)
        {
            var updatedUser = await _userService.UpdateAsync(id, userUpdateDto);
            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            var isDeleted = await _userService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }
    }
}