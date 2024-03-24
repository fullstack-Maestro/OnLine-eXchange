using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Entities;
using Olx.Service.DTOs.Transactions;
using Olx.Service.DTOs.Users;
using Olx.Service.Exceptions;
using Olx.Service.Extentions;
using Olx.Service.Interfaces;

namespace Olx.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Transaction> transactionRepository;
        private readonly IRepository<Post> postRepository;

        public UserService(
            IRepository<User> userRepository,
            IRepository<Transaction> transactionRepository,
            IRepository<Post> postRepository)
        {
            this.userRepository = userRepository;
            this.transactionRepository = transactionRepository;
            this.postRepository = postRepository;
        }

        public async Task<UserViewDto> CreateAsync(UserCreateDto user)
        {
            var existUser = await userRepository
                .SelectAllAsQueryable()
                .FirstOrDefaultAsync(u => u.Gmail == user.Gmail);

            if (existUser != null && existUser.IsDeleted)
                return await UpdateAsync(existUser.Id, user.MapTo<UserUpdateDto>(), true);

            if (existUser != null)
                throw new CustomException(409, "User already exists");

            existUser = user.MapTo<User>();

            var createUser = await userRepository.InsertAsync(existUser);
            await userRepository.SaveAsync();

            return createUser.MapTo<UserViewDto>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existUser = await userRepository.SelectByIdAsync(id)
                ?? throw new CustomException(404, "User not found");

            existUser.IsDeleted = true;
            existUser.DeletedAt = DateTime.UtcNow;

            await userRepository.DeleteAsync(existUser);
            await userRepository.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<UserViewDto>> GetAllAsync()
        {
            return await Task.FromResult(userRepository.SelectAllAsQueryable()
                .Where(c => !c.IsDeleted)
                .MapTo<UserViewDto>());
        }

        public async Task<UserViewDto> GetByIdAsync(long id)
        {
            var existUser = await userRepository.SelectByIdAsync(id)
                ?? throw new CustomException(404, "User not found");

            return existUser.MapTo<UserViewDto>();
        }

        public async Task<UserViewDto> UpdateAsync(long id, UserUpdateDto user, bool isDeleted = false)
        {
            var existUser = new User();

            if (isDeleted)
            {
                existUser = await userRepository
                    .SelectAllAsQueryable()
                    .FirstOrDefaultAsync(u => u.Id == id);

                existUser.IsDeleted = false;
            }

            existUser.Name = user.Name;
            existUser.Gmail = user.Gmail;
            existUser.Password = user.Password;
            existUser.UpdatedAt = DateTime.UtcNow;
            existUser.PhoneNumber = user.PhoneNumber;
            existUser.ProfilePicture = user.ProfilePicture;

            await userRepository.UpdateAsync(existUser);
            await userRepository.SaveAsync();

            return existUser.MapTo<UserViewDto>();
        }

        public async Task<IEnumerable<TransactionViewDto>> GetTransactionsByUserIdAsync(long userId)
        {
            var user = await userRepository.SelectByIdAsync(userId)
                       ?? throw new CustomException(404, "User not found");

            var transactions = await transactionRepository
                .SelectAllAsQueryable()
                .Where(t => t.CustomerId == user.Id || t.SellerId == user.Id)
                .ToListAsync();

            var transactionViewDtos = transactions.MapTo<TransactionViewDto>();

            foreach (var transactionViewDto in transactionViewDtos)
            {
                var seller = await userRepository.SelectByIdAsync(transactionViewDto.SellerId);

                if (transactionViewDto.CustomerId == user.Id)
                {
                    transactionViewDto.CustomerName = user.Name;
                }

                if (transactionViewDto.SellerId == user.Id)
                {
                    transactionViewDto.SellerName = user.Name;
                }
            }

            return transactionViewDtos;
        }

        public async Task<TransactionViewDto> CreateTransactionAsync(TransactionCreateDto transaction)
        {
            var customer = await userRepository.SelectByIdAsync(transaction.CustomerId)
                           ?? throw new CustomException(404, "Customer not found");

            var seller = await userRepository.SelectByIdAsync(transaction.SellerId)
                         ?? throw new CustomException(404, "Seller not found");

            var post = await postRepository.SelectByIdAsync(transaction.PostId)
                       ?? throw new CustomException(404, "Post not found");

            var newTransaction = new Transaction
            {
                CustomerId = customer.Id,
                SellerId = seller.Id,
                PostId = post.Id,
                Amount = post.Price // Assign the Price property of the Post entity to the Amount property of the newTransaction
            };

            await transactionRepository.InsertAsync(newTransaction);
            await transactionRepository.SaveAsync();

            var transactionView = new TransactionViewDto
            {
                Id = newTransaction.Id,
                CustomerId = newTransaction.CustomerId,
                SellerId = newTransaction.SellerId,
                PostId = newTransaction.PostId,
                Amount = newTransaction.Amount,
                CustomerName = customer.Name,
                SellerName = seller.Name
            };

            return transactionView;
        }
    }
}