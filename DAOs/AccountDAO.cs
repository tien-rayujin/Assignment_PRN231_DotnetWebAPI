using BOs;

namespace DAOs;

public interface IAccountDAO : IBaseDAO<Account> { }

public class AccountDAO : BaseDAO<Account>, IAccountDAO
{
    public AccountDAO(AppDbContext context) : base(context)
    {
    }
}
