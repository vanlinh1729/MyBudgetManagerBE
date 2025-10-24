namespace MyBudgetManager.Application.Interfaces.Repositories;

public interface IRepository <T> where T : class
{
    IQueryable<T> GetQuery(bool asTracking = false);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}