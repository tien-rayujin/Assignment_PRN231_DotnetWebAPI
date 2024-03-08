using BOs;
using DAOs;

namespace Repos;

public interface IProductRepo : IBaseRepo<Product> { }

public class ProductRepo : BaseRepo<Product, IProductDAO>, IProductRepo
{
    public ProductRepo(IProductDAO dao) : base(dao)
    {
    }
}
