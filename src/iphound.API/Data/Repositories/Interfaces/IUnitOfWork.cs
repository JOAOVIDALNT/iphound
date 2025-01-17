namespace iphound.API.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}