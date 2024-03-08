using BOs;
using Repos;

namespace Services;

public interface IAccountService : IBaseService<Account> { }

public class AccountService : BaseService<Account, IAccountRepo>, IAccountService
{
    public AccountService(IAccountRepo dao) : base(dao)
    {
    }
}
