using Microsoft.AspNetCore.Mvc;
using Olx.Service.DTOs.Transactions;
using Olx.Service.DTOs.Users;
using Olx.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewDto>> GetUserById(long id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, userUpdateDto);
                if (updatedUser == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            try
            {
                var isDeleted = await _userService.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound("User not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/transactions")]
        public async Task<ActionResult<IEnumerable<TransactionViewDto>>> GetTransactionsByUserId(long userId)
        {
            try
            {
                var transactions = await _userService.GetTransactionsByUserIdAsync(userId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("transactions")]
        public async Task<ActionResult<TransactionViewDto>> CreateTransaction(TransactionCreateDto transactionCreateDto)
        {
            try
            {
                var createdTransaction = await _userService.CreateTransactionAsync(transactionCreateDto);
                return Ok(createdTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}