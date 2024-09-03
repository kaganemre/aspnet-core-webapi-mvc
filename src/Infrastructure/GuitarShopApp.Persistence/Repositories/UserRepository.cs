using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GuitarShopApp.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(ShopAppDbContext context) : base(context)
        {

        }
        private ShopAppDbContext ShopContext
        {
            get { return context as ShopAppDbContext ?? ShopContext!; }
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await ShopContext.Users.FirstOrDefaultAsync(i => i.Email == email) ?? new User();
        }
        public async Task<User> CheckPassword(User entity)
        {
            return await ShopContext.Users.FirstOrDefaultAsync(i => i.Email == entity.Email && i.Password == entity.Password) ?? new User();
        }
        public async Task UpdateAsync(User entity)
        {
            var model = await GetById(entity.Id) ?? new User();
            ShopContext.Entry(model).CurrentValues.SetValues(entity);
        }
    }
}