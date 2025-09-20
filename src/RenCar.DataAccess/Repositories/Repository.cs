using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
using RenCar.DataAccess.Context;
using RenCar.Domain.Entities;

namespace RenCar.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : AudiTable
{
    private readonly AppDbContext context;
    public Repository()
    {
        context = new AppDbContext();
        context.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var createdEntity = (await context.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return createdEntity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.IsDeleted = true;
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<TEntity> SelectAsync(int id)
    {
        return await context.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);
    }

    public IQueryable<TEntity> SelectAllAsQueryable()
    {
        return context.Set<TEntity>().AsQueryable();
    }
}
