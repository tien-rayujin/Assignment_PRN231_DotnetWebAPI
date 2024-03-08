using BOs;
using Repos;

namespace Services;

public interface IRoleService : IBaseService<Role> { }

public class RoleService : BaseService<Role, IRoleRepo>, IRoleService
{
    public RoleService(IRoleRepo dao) : base(dao)
    {
    }
}
