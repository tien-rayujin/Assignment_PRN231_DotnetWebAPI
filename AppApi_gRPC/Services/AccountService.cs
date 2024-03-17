using Grpc.Core;
using Services;

namespace AppApi_gRPC.Services
{
    public class AccountService: AccountCRUD.AccountCRUDBase
    {
        private ILogger<AccountService> _logger;
        private IAccountService _serv;

        public AccountService(ILogger<AccountService> _logger, IAccountService _serv)
        {
            this._logger = _logger;
            this._serv = _serv;
        }

        public override Task<Accounts> GetAll(EmptyAccount requestData, ServerCallContext context)
        {
            Accounts products = new Accounts();

            var query = from e in _serv.GetAll()
                        select new Account()
                        {
                            AccountId = e.AccountId,
                            Name = e.Name,
                            Email = e.Email,
                            Password = e.Password,
                            RoleId = e.RoleId,
                            Phone = e.Phone,
                            IsActive = e.IsActive.HasValue ? (bool)e.IsActive : false,
                        };
            products.Items.AddRange(query);
            return Task.FromResult(products);
        }

        public override Task<Account> Get(AccountFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.AccountId);
            Account product = new Account()
            {
                AccountId = data.AccountId,
                Name = data.Name,
                Email = data.Email,
                Password = data.Password,
                IsActive = data.IsActive.HasValue ? (bool)data.IsActive : false,
                RoleId = data.RoleId,
                Phone = data.Phone,
            };

            return Task.FromResult(product);
        }

        public override Task<EmptyAccount> Create(Account request, ServerCallContext context)
        {
            _serv.Create(new BOs.Account()
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                IsActive = (bool)request.IsActive,
                RoleId = request.RoleId,
                Phone = request.Phone,
            });

            return Task.FromResult(new EmptyAccount());
        }

        public override Task<EmptyAccount> Update(Account request, ServerCallContext context)
        {
            _serv.Update(new BOs.Account()
            {
                AccountId = request.AccountId,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                IsActive = (bool)request.IsActive,
                RoleId = request.RoleId,
                Phone = request.Phone,
            });

            return Task.FromResult(new EmptyAccount());
        }

        public override Task<EmptyAccount> Delete(AccountFilter request, ServerCallContext context)
        {
            var data = _serv.Get(request.AccountId);
            _serv.Delete(data);

            return Task.FromResult(new EmptyAccount());
        }
    }
}
