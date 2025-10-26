using Microsoft.EntityFrameworkCore;
using MyBudgetManager.Application.Interfaces.Repositories;
using MyBudgetManager.Infrastructure.Persistence;

namespace MyBudgetManager.Infrastructure.Repositories;

public class Repository <T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<T?> GetByIdAsync(Guid id)
        => await _context.Set<T>().FindAsync(id);

    public IQueryable<T> GetQuery(bool asTracking = false)
    {
        return asTracking ? _context.Set<T>().AsQueryable() : _context.Set<T>().AsNoTracking();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

    public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        await _context.AddRangeAsync(entities);
    }
    public void Update(T entity)
        => _context.Set<T>().Update(entity);

    public void Remove(T entity)
        => _context.Set<T>().Remove(entity);
}