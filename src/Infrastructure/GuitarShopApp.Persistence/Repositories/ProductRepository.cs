using AutoMapper;
using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GuitarShopApp.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ShopAppDbContext context) : base(context)
        {

        }
        private ShopAppDbContext ShopContext
        {
            get { return context as ShopAppDbContext ?? ShopContext!; }
        }
        public async Task<IEnumerable<Product>> GetHomePageProducts()
        {
            return await ShopContext.Products.Where(i => i.IsHome).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLastFiveProducts()
        {
            return await ShopContext.Products.OrderByDescending(p => p.Id).Take(5).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var products = ShopContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = ShopContext.Products
                             .Include(c => c.Category)
                             .Where(p => p.Category.Url == category);
            }

            return await products.ToListAsync();
        }
        public async Task UpdateAsync(Product entity)
        {
            var model = await GetById(entity.Id) ?? new Product();
            ShopContext.Entry(model).CurrentValues.SetValues(entity);
        }


    }
}