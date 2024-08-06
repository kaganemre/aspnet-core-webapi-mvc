using AutoMapper;
using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GuitarShopApp.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly IMapper _mapper;
    public OrderRepository(ShopAppDbContext context, IMapper mapper = null!): base(context)
    {
        _mapper = mapper;
    }

    private ShopAppDbContext? ShopContext
    {
        get { return context as ShopAppDbContext ?? ShopContext!; }
    }

    public async Task UpdateAsync(Order entity)
    {
        var model = await GetById(entity.Id) ?? new Order();
        _mapper.Map<Order>(model);
    }
}