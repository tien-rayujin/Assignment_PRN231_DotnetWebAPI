using Repos;

namespace Services;

public interface IBaseService<T> where T : class
{
    public List<T> GetAll();
    public List<T> GetAll(string? includeProps);
    public T Get(string id);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}

public class BaseService<T, TRepo> : IBaseService<T> where T : class where TRepo : IBaseRepo<T>
{
    TRepo _dao;

    public BaseService(TRepo dao)
    {
        _dao = dao;
    }
    public List<T> GetAll() => _dao.GetAll().ToList();
    public List<T> GetAll(string? includeProps) => _dao.GetAll(includeProps).ToList();
    public T Get(string id) => _dao.Get(id);
    public void Create(T entity) => _dao.Create(entity);
    public void Update(T entity) => _dao.Update(entity);
    public void Delete(T entity) => _dao.Delete(entity);
}
