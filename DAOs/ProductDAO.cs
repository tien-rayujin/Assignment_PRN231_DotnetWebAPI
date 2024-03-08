using BOs;

namespace DAOs;

public interface IProductDAO : IBaseDAO<Product> { }

public class ProductDAO : BaseDAO<Product>, IProductDAO
{
    public ProductDAO(AppDbContext context) : base(context)
    {
    }
}
