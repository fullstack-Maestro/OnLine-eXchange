using Microsoft.AspNetCore.Mvc;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Users;

namespace Olx.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> GetUserById(long Id)
    {
        var user = await _userRepository.GetUserById(Id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        _userRepository.AddUser(user);
        await _userRepository.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPut("{gmail}/{password}")]
    public async Task<ActionResult<User>> UpdateUser(string gmail, string password, UserUpdateDto userInput)
    {
        var existingUser = await _userRepository.GetUserByGmail(gmail);
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
        await _userRepository.SaveChangesAsync();

        return Ok(existingUser);
    }
}
