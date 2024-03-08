using BOs;
using Repos;

namespace Services;

public interface IProductService : IBaseService<Product> { }

public class ProductService : BaseService<Product, IProductRepo>, IProductService
{
    public ProductService(IProductRepo dao) : base(dao)
    {
    }
}
