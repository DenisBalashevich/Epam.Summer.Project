using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
namespace DAL.Interfaces.Interfaces
{
    public interface ITagRepository : IRepository<DalTag>
    {
        DalTag GetTagByName(string name);
    }
}
