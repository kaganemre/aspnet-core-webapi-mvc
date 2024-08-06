using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Repositories;
public interface IProductRepository : IRepository<Product>
{
       Task<IEnumerable<Product>> GetHomePageProducts();
       Task<IEnumerable<Product>> GetLastFiveProducts();
       Task<IEnumerable<Product>> GetProductsByCategory(string name);
       Task UpdateAsync(Product entity);

}