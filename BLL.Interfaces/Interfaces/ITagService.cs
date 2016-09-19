using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface ITagService : IService<TagEntity>
    {
        TagEntity GetTagByName(string name);
        IEnumerable<TagEntity> FindTags(string filter);
    }
}
