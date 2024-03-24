using Olx.Service.DTOs.Transactions;
using Olx.Service.DTOs.Users;

namespace Olx.Service.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="user">The user data.</param>
    /// <returns>The created user.</returns>
    Task<UserViewDto> CreateAsync(UserCreateDto user);

    /// <summary>
    /// Update an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user data.</param>
    /// <param name="isDeleted">Flag indicating if the user is marked as deleted.</param>
    /// <returns>The updated user.</returns>
    Task<UserViewDto> UpdateAsync(long id, UserUpdateDto user, bool isDeleted = false);

    /// <summary>
    /// Delete an existing user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>True if the user is deleted successfully, false otherwise.</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// Get an existing user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The retrieved user.</returns>
    Task<UserViewDto> GetByIdAsync(long id);

    /// <summary>
    /// Get a list of all existing users.
    /// </summary>
    /// <returns>The list of users.</returns>
    Task<IEnumerable<UserViewDto>> GetAllAsync();

    /// <summary>
    /// Get transactions associated with a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The list of transactions.</returns>
    Task<IEnumerable<TransactionViewDto>> GetTransactionsByUserIdAsync(long userId);

    /// <summary>
    /// Create a new transaction.
    /// </summary>
    /// <param name="transaction">The transaction data.</param>
    /// <returns>The created transaction.</returns>
    Task<TransactionViewDto> CreateTransactionAsync(TransactionCreateDto transaction);

    /// <summary>
    /// Add money to the balance of a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="amount">The amount of money to add.</param>
    Task AddMoneyToBalance(long userId, decimal amount);
}