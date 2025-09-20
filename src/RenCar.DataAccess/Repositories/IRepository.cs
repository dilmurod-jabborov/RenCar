using System.Linq;
namespace RenCar.DataAccess.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> SelectAsync(int id);
    IQueryable<TEntity> SelectAllAsQueryable();
}