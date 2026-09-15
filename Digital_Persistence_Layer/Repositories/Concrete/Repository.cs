using System.Linq.Expressions;
using Digital_Domain_Layer.Extensions;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly ApplicationDbContext _context;
    private IRepository<T> _repositoryImplementation;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    private DbSet<T> Table
    {
        get => _context.Set<T>();
    }


    public async Task<T?> GetById(Guid id)
    {
        var entity = await Table.FindAsync(id);
        return entity;
    }

    public Task<bool> isAnyItem(Expression<Func<T, bool>> filter = null)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.AnyAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Table.ToListAsync(); //
    }

    public async Task<T?> Add(T entity)
    {
        await Table.AddAsync(entity);
        if (await _context.SaveChangesAsync() > 0)
        {
            return entity;
        }

        return null;
    }

    public async Task<List<T>> AddRange(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
        if (await _context.SaveChangesAsync() > 0)
        {
            return entities;
        }

        return null;
    }

    public async Task<T> GetWhere(Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table.AsQueryable();

        if (includeProperties.Length > 0)
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> Update(T entity, Guid id)
    {
        var existingEntity = await Table.FindAsync(id);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        return null;
    }

    public async Task Delete(Guid id)
    {
        var entity = await Table.FindAsync(id);
        if (entity != null)
        {
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<PagedResult<T>> GetAllPagedResult(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageNumber = 1, int pageSize = 10)
    {
        IQueryable<T> query = Table.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        int totalCount = await query.CountAsync();
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<T>
        {
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = items,
        };
    }

    public async Task<IEnumerable<T>> GetWithIncludeProperties(params Expression<Func<T, object>>[] includeProperties)
    {
        var query = Table.AsQueryable();

        if (includeProperties.Length > 0)
        {
            foreach (var item in includeProperties)
            {
                query = query.Include(item);
            }
        }

        return await query.ToListAsync();
    }
}