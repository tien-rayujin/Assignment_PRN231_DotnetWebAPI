using BOs;
using DAOs;

namespace Repos;

public interface IAccountRepo : IBaseRepo<Account> { }

public class AccountRepo : BaseRepo<Account, IAccountDAO>, IAccountRepo
{
    public AccountRepo(IAccountDAO dao) : base(dao)
    {
    }
}
