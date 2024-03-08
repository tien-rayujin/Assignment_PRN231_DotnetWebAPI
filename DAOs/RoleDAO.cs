using BOs;

namespace DAOs;

public interface IRoleDAO : IBaseDAO<Role> { }

public class RoleDAO : BaseDAO<Role>, IRoleDAO
{
    public RoleDAO(AppDbContext context) : base(context)
    {
    }
}
