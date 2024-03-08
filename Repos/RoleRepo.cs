using BOs;
using DAOs;

namespace Repos;

public interface IRoleRepo : IBaseRepo<Role> { }

public class RoleRepo : BaseRepo<Role, IRoleDAO>, IRoleRepo
{
    public RoleRepo(IRoleDAO dao) : base(dao)
    {
    }
}
