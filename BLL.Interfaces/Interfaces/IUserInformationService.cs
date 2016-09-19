using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface IUserInformationService : IService<UserInformationEntity>
    {
        UserInformationEntity GetByUserId(int userId);
    }
}
