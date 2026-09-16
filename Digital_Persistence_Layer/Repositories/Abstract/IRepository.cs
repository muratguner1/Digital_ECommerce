using System.Linq.Expressions;
using Digital_Domain_Layer.Extensions;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface IRepository<T> where T : new()
{
    Task<T?> GetById(Guid id);

    //Task<T?> GetByIdGeneric<T>(Guid id) where T : class;
    Task<bool> isAnyItem(Expression<Func<T, bool>> filter = null);
    Task<IEnumerable<T>> GetAll();
    Task<T?> Add(T entity);
    Task<List<T>> AddRange(List<T> entities);

    Task<T> GetWhere(Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T?> Update(T entity, Guid id);
    Task Delete(Guid id);

    Task<PagedResult<T>> GetAllPagedResult(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int pageNumber = 1, int pageSize = 10);

    Task<IEnumerable<T>> GetWithIncludeProperties(params Expression<Func<T, object>>[] includeProperties);
}