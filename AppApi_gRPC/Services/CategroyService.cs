using Grpc.Core;
using Services;

namespace AppApi_gRPC.Services
{
    public class CategoryService: CategoryCRUD.CategoryCRUDBase
    {
        private ILogger<CategoryService> _logger;
        private ICategoryService _serv;

        public CategoryService(ILogger<CategoryService> _logger, ICategoryService _serv)
        {
            this._logger = _logger;
            this._serv = _serv;
        }

        public override Task<Categorys> GetAll(EmptyCategory requestData, ServerCallContext context)
        {
            Categorys products = new Categorys();

            var query = from e in _serv.GetAll()
                        select new Category()
                        {
                            CategoryId = e.CategoryId,
                            Name = e.Name,
                        };
            products.Items.AddRange(query);
            return Task.FromResult(products);
        }

        public override Task<Category> Get(CategoryFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.CategoryId);
            Category product = new Category()
            {
                CategoryId = data.CategoryId,
                Name = data.Name,
            };

            return Task.FromResult(product);
        }

        public override Task<EmptyCategory> Create(Category request, ServerCallContext context)
        {
            _serv.Create(new BOs.Category()
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
            });

            return Task.FromResult(new EmptyCategory());
        }

        public override Task<EmptyCategory> Update(Category request, ServerCallContext context)
        {
            _serv.Update(new BOs.Category()
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
            });

            return Task.FromResult(new EmptyCategory());
        }

        public override Task<EmptyCategory> Delete(CategoryFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.CategoryId);
            _serv.Delete(data);

            return Task.FromResult(new EmptyCategory());
        }
    }
}
