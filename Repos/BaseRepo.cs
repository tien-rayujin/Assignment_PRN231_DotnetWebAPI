using DAOs;

namespace Repos;

public interface IBaseRepo<T> where T : class
{
    public List<T> GetAll();
    public List<T> GetAll(string? includeProps);
    public T Get(string id);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}

public class BaseRepo<T, TDAO>: IBaseRepo<T> where T : class where TDAO : IBaseDAO<T>
{
    TDAO _dao;

    public BaseRepo(TDAO dao)
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
