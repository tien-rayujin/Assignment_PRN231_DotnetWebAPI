using Grpc.Core;
using Services;

namespace AppApi_gRPC.Services
{
    public class RoleService: RoleCRUD.RoleCRUDBase
    {
        private ILogger<RoleService> _logger;
        private IRoleService _serv;

        public RoleService(ILogger<RoleService> _logger, IRoleService _serv)
        {
            this._logger = _logger;
            this._serv = _serv;
        }

        public override Task<Roles> GetAll(EmptyRole requestData, ServerCallContext context)
        {
            Roles products = new Roles();

            var query = from e in _serv.GetAll()
                        select new Role()
                        {
                            RoleId = e.RoleId,
                            Name = e.Name,
                        };
            products.Items.AddRange(query);
            return Task.FromResult(products);
        }

        public override Task<Role> Get(RoleFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.RoleId);
            Role product = new Role()
            {
                RoleId = data.RoleId,
                Name = data.Name,
            };

            return Task.FromResult(product);
        }

        public override Task<EmptyRole> Create(Role request, ServerCallContext context)
        {
            _serv.Create(new BOs.Role()
            {
                RoleId = request.RoleId,
                Name = request.Name,
            });

            return Task.FromResult(new EmptyRole());
        }

        public override Task<EmptyRole> Update(Role request, ServerCallContext context)
        {
            _serv.Update(new BOs.Role()
            {
                RoleId = request.RoleId,
                Name = request.Name,
            });

            return Task.FromResult(new EmptyRole());
        }

        public override Task<EmptyRole> Delete(RoleFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.RoleId);
            _serv.Delete(data);

            return Task.FromResult(new EmptyRole());
        }
    }
}
