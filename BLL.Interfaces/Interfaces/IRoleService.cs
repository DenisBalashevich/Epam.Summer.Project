using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface IRoleService : IService<RoleEntity>
    {
        RoleEntity GetRoleByName(string name);
    }
}
