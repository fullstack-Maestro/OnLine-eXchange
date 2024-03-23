using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.Domain.Commons;

namespace Olx.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<TEntity> entities;
    public Repository(AppDbContext context)
    {
        this.context = context;
        this.entities = context.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        return (await entities.AddAsync(entity)).Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entities.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entities.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }

    public async Task<TEntity> SelectByIdAsync(long id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await entities.FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public IEnumerable<TEntity> SelectAllAsEnumerable()
    {
        return entities.AsEnumerable();
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return entities.AsQueryable();
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
