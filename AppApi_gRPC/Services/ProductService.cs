using Grpc.Core;
using Services;

namespace AppApi_gRPC.Services
{
    public class ProductService: ProductCRUD.ProductCRUDBase
    {
        private ILogger<ProductService> _logger;
        private IProductService _serv;

        public ProductService(ILogger<ProductService> _logger, IProductService _serv)
        {
            this._logger = _logger;
            this._serv = _serv;
        }

        public override Task<Products> GetAll(EmptyProduct requestData, ServerCallContext context)
        {
            Products products = new Products();

            var query = from e in _serv.GetAll()
                        select new Product()
                        {
                            ProductId = e.ProductId,
                            Name = e.Name,
                            CategoryId = e.CategoryId,
                            Description = e.Description,
                            IsActive = e.IsActive.HasValue ? (bool)e.IsActive : false,
                            Price = e.Price.HasValue ? (int)e.Price : 0
                        };
            products.Items.AddRange(query);
            return Task.FromResult(products);
        }

        public override Task<Product> Get(ProductFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.ProductId);
            Product product = new Product()
            {
                ProductId = data.ProductId,
                Name = data.Name,
                CategoryId = data.CategoryId,
                Description = data.Description,
                IsActive = data.IsActive.HasValue ? (bool)data.IsActive : false,
                Price = data.Price.HasValue ? (int)data.Price : 0
            };

            return Task.FromResult(product);
        }

        public override Task<EmptyProduct> Create(Product request, ServerCallContext context)
        {
            _serv.Create(new BOs.Product()
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Description = request.Description,
                IsActive = (bool)request.IsActive,
                Price = (int)request.Price
            });

            return Task.FromResult(new EmptyProduct());
        }

        public override Task<EmptyProduct> Update(Product request, ServerCallContext context)
        {
            _serv.Update(new BOs.Product()
            {
                ProductId = request.ProductId,
                Name = request.Name,
                CategoryId = request.CategoryId,
                Description = request.Description,
                IsActive = (bool)request.IsActive,
                Price = (int)request.Price
            });

            return Task.FromResult(new EmptyProduct());
        }

        public override Task<EmptyProduct> Delete(ProductFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.ProductId);
            _serv.Delete(data);

            return Task.FromResult(new EmptyProduct());
        }
    }
}
