using Olx.Domain.Commons;

namespace Olx.DataAccess.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    /// <summary>
    /// Inserts a new entity into the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    /// <returns>A task representing the asynchronous operation, returning the inserted entity.</returns>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation, returning the updated entity.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an existing entity from the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted entity.</returns>
    Task<TEntity> DeleteAsync(TEntity entity);

    /// <summary>
    /// Retrieves an entity from the repository asynchronously based on its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, returning the retrieved entity.</returns>
    Task<TEntity> SelectByIdAsync(long id);

    /// <summary>
    /// Retrieves all entities from the repository as an enumerable collection.
    /// </summary>
    /// <returns>An enumerable collection of all entities in the repository.</returns>
    IEnumerable<TEntity> SelectAllAsEnumerable();

    /// <summary>
    /// Retrieves all entities from the repository as an queryable collection.
    /// </summary>
    /// <returns>An queryable collection of all entities in the repository.</returns>
    IQueryable<TEntity> SelectAllAsQueryable();

    /// <summary>
    /// Saves changes made to the repository asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveAsync();
}
