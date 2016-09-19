using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
namespace DAL.Interfaces.Interfaces
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByName(string name);
        DalUser GetByEmail(string email);
    }
}
