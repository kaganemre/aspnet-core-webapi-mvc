using AutoMapper;
using GuitarShopApp.Application.Interfaces.Repositories;
using GuitarShopApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GuitarShopApp.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    protected readonly DbContext context;
    public Repository(DbContext ctx)
    {
        context = ctx;
    }
    public async Task CreateAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int? id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}