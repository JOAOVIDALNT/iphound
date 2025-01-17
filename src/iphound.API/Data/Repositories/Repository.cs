using System.Linq.Expressions;
using iphound.API.Data.Context;
using iphound.API.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iphound.API.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext dbContext;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = this.dbContext.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
    {
        IQueryable<T> query = dbSet;
        
        if (filter != null)
            query = query.Where(filter);
        
        if (pageSize > 0)
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        
        return await query.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = dbSet;
        
        if (!tracked)
            query = query.AsNoTracking();
        
        if (filter != null)
            query = query.Where(filter);
        
        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);

    public void Update(T entity) => dbSet.Update(entity);

    public void Delete(T entity) => dbSet.Remove(entity);
}