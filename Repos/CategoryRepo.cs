using BOs;
using DAOs;

namespace Repos;

public interface ICategoryRepo : IBaseRepo<Category> { }

public class CategoryRepo : BaseRepo<Category, ICategoryDAO>, ICategoryRepo
{
    public CategoryRepo(ICategoryDAO dao) : base(dao)
    {
    }
}
