using AutoMapper;
using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Persistence.Context;
using GuitarShopApp.Persistence.Repositories;

namespace GuitarShopApp.Persistence.UnitOfWorks;
public class UnitOfWork : IUnitOfWork
{
    private readonly ShopAppDbContext _context;
    public UnitOfWork(ShopAppDbContext context)
    {
        _context = context;
    }

    private ProductRepository? _productRepository;
    private CategoryRepository? _categoryRepository;
    private OrderRepository? _orderRepository;
    private UserRepository? _userRepository;

    public IProductRepository Products => 
            _productRepository = _productRepository ?? new ProductRepository(_context);
    public ICategoryRepository Categories => 
            _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
    public IOrderRepository Orders => 
            _orderRepository = _orderRepository ?? new OrderRepository(_context);
    public IUserRepository Users => 
            _userRepository = _userRepository ?? new UserRepository(_context);


    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}