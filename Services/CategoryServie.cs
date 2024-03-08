using BOs;
using Repos;

namespace Services;

public interface ICategoryService : IBaseService<Category> { }

public class CategoryService : BaseService<Category, ICategoryRepo>, ICategoryService
{
    public CategoryService(ICategoryRepo dao) : base(dao)
    {
    }
}
