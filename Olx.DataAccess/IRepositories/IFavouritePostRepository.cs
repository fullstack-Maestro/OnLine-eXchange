using Olx.Domain.Entities;

namespace Olx.DataAccess.IRepositories
{
    public interface IFavouritePostRepository
    {
        void AddFavouritePost(FavouritePost favouritePost);
        void DeleteFavouritePost(FavouritePost favouritePost);
        Task<List<FavouritePost>> GetAllFavouritePosts();
        Task<FavouritePost> GetFavouritePostById(long favouritePostId);
        Task SaveChangesAsync();
        void UpdateFavouritePost(FavouritePost favouritePost);
    }
}