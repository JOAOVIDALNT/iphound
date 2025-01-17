using iphound.API.Data.Context;
using iphound.API.Data.Repositories.Interfaces;

namespace iphound.API.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected AppDbContext dbContext;

    public UnitOfWork(AppDbContext dbContext) => this.dbContext = dbContext; 
    public async Task Commit() => await dbContext.SaveChangesAsync();
}