using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetById(int? id);
        Task<User> GetUserByEmail(string entity);
        Task<User> CheckPassword(User entity);
        Task CreateAsync(User entity);
        Task UpdateAsync(User entity);
    }
}