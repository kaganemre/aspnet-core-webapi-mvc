using GuitarShopApp.Application.Interfaces.Repositories;

namespace GuitarShopApp.Application.Interfaces.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IOrderRepository Orders { get; }
    void Save(); 
    Task<int> SaveAsync(); 
}