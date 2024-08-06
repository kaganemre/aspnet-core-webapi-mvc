using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Interfaces.UnitOfWork;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(Order entity)
    {
        await _unitOfWork.Orders.CreateAsync(entity);
        await _unitOfWork.SaveAsync();
    }
    public void Update(Order entity)
    {
        _unitOfWork.Orders.Update(entity);
        _unitOfWork.Save();
    }

    public async Task UpdateAsync(Order entity)
    {
        await _unitOfWork.Orders.UpdateAsync(entity);
        await _unitOfWork.SaveAsync();
    }
}