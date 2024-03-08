using BOs;
using Microsoft.EntityFrameworkCore;

namespace DAOs;

public interface IBaseDAO<T>
{
    public IList<T> GetAll();
    public IList<T> GetAll(string? includeProps);
    public T Get(string id);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public void Delete(string id);
}

public class BaseDAO<T> : IBaseDAO<T> where T : class
{
    AppDbContext _context;
    DbSet<T> _dbSet;

    public BaseDAO(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void DetachEntity(T obj)
    {
        var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
        var keyValue = typeof(T).GetProperty(keyName).GetValue(obj);
        var checkExist = _context.Set<T>().Find(keyValue);
        if (checkExist != null)
        {
            _context.Entry(checkExist).State = EntityState.Detached;
        }
    }

    public IList<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public IList<T> GetAll(string? includeProps)
    {
        return _dbSet.Include(includeProps).ToList();
    }

    public T Get(string id)
    {
        return _dbSet.Find(id);
    }

    public void Create(T entity)
    {
        DetachEntity(entity);
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        DetachEntity(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        DetachEntity(entity);
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var entity = Get(id);
        DetachEntity(entity);
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
