using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();
}