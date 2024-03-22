using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Users;
using Olx.Service.Extentions;

namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<List<UserViewDto>> GetAllUsers()
    {
        var users = _userRepository.SelectAllAsEnumerable()
            .Where(user => !user.IsDeleted)
            .ToList();
        List<UserViewDto> viewDtos = new List<UserViewDto>();
        foreach (var user in users)
        {
            viewDtos.Add(user.MapTo<UserViewDto>());
        }
        return Ok(viewDtos);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<UserViewDto>> GetUserById(long Id)
    {
        var user = await _userRepository.SelectByIdAsync(Id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        var userView = user.MapTo<UserViewDto>();
        return Ok(userView);
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser(UserCreateDto createUser)
    {
        var user = createUser.MapTo<User>();
        await _userRepository.InsertAsync(user);
        await _userRepository.SaveAsync();

        return Ok(createUser);
    }

    [HttpPut("{id}/{password}")]
    public async Task<ActionResult<User>> UpdateUser(long id, string password, UserUpdateDto userInput)
    {
        var existingUser = await _userRepository.SelectByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound("User not found.");
        }

        if (existingUser.Password != password)
        {
            return Unauthorized("Invalid password.");
        }

        // Update the user's information based on the userInput
        existingUser.Name = userInput.Name;
        existingUser.PhoneNumber = userInput.PhoneNumber;
        existingUser.Gmail = userInput.Gmail;
        existingUser.Password = userInput.Password;

        // Save the changes to the database
        await _userRepository.SaveAsync();

        return Ok(existingUser);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(long id)
    {
        var existingUser = await _userRepository.SelectByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound("User not found.");
        }

        await _userRepository.DeleteAsync(existingUser);
        await _userRepository.SaveAsync();

        return NoContent();
    }
}
