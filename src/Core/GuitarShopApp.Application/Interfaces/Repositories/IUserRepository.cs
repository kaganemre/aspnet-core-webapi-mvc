using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Repositories;
public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string entity);
    Task<User> CheckPassword(User entity);
    Task UpdateAsync(User entity);
}