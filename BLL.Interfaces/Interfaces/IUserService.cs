using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByName(string name);
    }
}
