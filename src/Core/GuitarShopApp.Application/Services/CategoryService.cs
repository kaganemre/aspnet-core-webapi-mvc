using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
           return await _unitOfWork.Categories.GetAll();
        }

    }
}