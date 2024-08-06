using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.Persistence.Context;

namespace GuitarShopApp.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopAppDbContext context): base(context)
        {
            
        }

        private ShopAppDbContext? ShopContext
        {
            get { return context as ShopAppDbContext; }
        }

    }
}