using GuitarShopApp.Application.Exceptions;
using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Services;

public class ProductService : IProductService
{

    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _unitOfWork.Products.GetAll();
    }
    public async Task<IEnumerable<Product>> GetHomePageProducts()
    {
        return await _unitOfWork.Products.GetHomePageProducts();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        return await _unitOfWork.Products.GetProductsByCategory(category);
    }
    public async Task<Product> GetById(int? id)
    {
        var product = await _unitOfWork.Products.GetById(id) ?? throw new NotFoundException();
        return product;
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        await _unitOfWork.Products.CreateAsync(entity);
        await _unitOfWork.SaveAsync();
        return entity;
    }
    public void Update(Product entity)
    {
        _unitOfWork.Products.Update(entity);
        _unitOfWork.Save();
    }
    public async Task UpdateAsync(Product entity)
    {
        await _unitOfWork.Products.UpdateAsync(entity);
        await _unitOfWork.SaveAsync();
    }

    public void Delete(Product entity)
    {
        _unitOfWork.Products.Delete(entity);
        _unitOfWork.Save();
    }

    public async Task<IEnumerable<Product>> GetLastFiveProducts()
    {
       return await _unitOfWork.Products.GetLastFiveProducts();
    }
}