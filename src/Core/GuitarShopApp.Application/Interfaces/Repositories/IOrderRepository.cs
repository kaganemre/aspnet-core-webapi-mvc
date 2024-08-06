using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Interfaces.Repositories;

public interface IOrderRepository:IRepository<Order>
{
    Task UpdateAsync(Order entity);
}