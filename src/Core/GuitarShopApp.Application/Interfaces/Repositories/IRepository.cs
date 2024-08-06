using GuitarShopApp.Domain.Common;

namespace GuitarShopApp.Application.Interfaces.Repositories;
public interface IRepository<T> where T : BaseEntity, new()
{
    Task<T?> GetById(int? id);
    Task<IEnumerable<T>> GetAll();
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}