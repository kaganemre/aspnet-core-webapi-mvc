using GuitarShopApp.Application.Exceptions;
using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetById(int? id)
    {
        return await _unitOfWork.Users.GetById(id) ?? throw new NotFoundException();
    }
    public async Task<User> GetUserByEmail(string entity)
    {
       return await _unitOfWork.Users.GetUserByEmail(entity) ?? throw new NotFoundException();
    }
    public async Task<User> CheckPassword(User entity)
    {
        return await _unitOfWork.Users.CheckPassword(entity) ?? throw new NotFoundException();
    }
    public async Task CreateAsync(User entity)
    {
        await _unitOfWork.Users.CreateAsync(entity);
        await _unitOfWork.SaveAsync();
    }
    public async Task UpdateAsync(User entity)
    {
        await _unitOfWork.Users.UpdateAsync(entity);
        await _unitOfWork.SaveAsync();
    }
    
}