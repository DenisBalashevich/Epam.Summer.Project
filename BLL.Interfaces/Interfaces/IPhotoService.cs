using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Interfaces
{
    public interface IPhotoService : IService<PhotoEntity>
    {
        void DeleteRating(RatingEntity rating);
        void AddRating(RatingEntity rating);
        IEnumerable<PhotoEntity> AllUserPhotos(string userName);
        IEnumerable<PhotoEntity> AllUserPhotos(int userId);
    }
}
