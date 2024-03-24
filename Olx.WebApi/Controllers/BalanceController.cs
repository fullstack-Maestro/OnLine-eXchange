using Microsoft.AspNetCore.Mvc;
using Olx.Service.Exceptions;
using Olx.Service.Interfaces;

namespace Olx.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class BalanceController : ControllerBase
{
    private readonly IUserService _userService;

    public BalanceController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("{userId}/add-money")]
    public async Task<IActionResult> AddMoneyToUserBalance(long userId, [FromBody] decimal amount)
    {
        try
        {
            await _userService.AddMoneyToBalance(userId, amount);
            return Ok("Money added successfully");
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while adding money to the user's balance");
        }
    }
}