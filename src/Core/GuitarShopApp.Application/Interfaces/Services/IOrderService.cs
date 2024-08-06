using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Services;

public interface IOrderService
{
    Task CreateAsync(Order entity);
    void Update(Order entity);
    Task UpdateAsync(Order entity);
}