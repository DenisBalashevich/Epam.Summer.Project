using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
namespace DAL.Interfaces.Interfaces
{
    public interface IPhotoRepository : IRepository<DalPhoto>
    {
        void AddRating(DalRating rating);

        void DeleteRating(DalRating rating);

    }
}
