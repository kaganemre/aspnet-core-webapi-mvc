using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetHomePageProducts();
        Task<IEnumerable<Product>> GetLastFiveProducts();
        Task<IEnumerable<Product>> GetProductsByCategory(string name);
        Task<Product> GetById(int? id);
        Task<Product> CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
       
    }
}