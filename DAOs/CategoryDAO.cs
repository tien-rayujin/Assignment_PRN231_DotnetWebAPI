using BOs;

namespace DAOs;

public interface ICategoryDAO : IBaseDAO<Category> { }

public class CategoryDAO : BaseDAO<Category>, ICategoryDAO
{
    public CategoryDAO(AppDbContext context) : base(context)
    {
    }
}
