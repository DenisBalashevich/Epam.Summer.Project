using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interfaces;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Entities;
using BLL.Mappers;
using DAL;
using DAL.Interfaces.DTO;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IUnitOfWork uow;

        public PhotoService(IUnitOfWork uow, IPhotoRepository photoRepository)
        {
            if (ReferenceEquals(uow, null) || ReferenceEquals(photoRepository, null))
                throw new ArgumentNullException(uow == null ? nameof(uow) : nameof(photoRepository));
            this.uow = uow;
            this.photoRepository = photoRepository;
        }

        public PhotoEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            DalPhoto photo = null;

            photo = photoRepository.GetById(id);

            if (ReferenceEquals(photo, null))
                return null;
            return photo.ToBLLPhoto();
        }

        public IEnumerable<PhotoEntity> GetAll() => photoRepository.GetAll().Select(p => p.ToBLLPhoto());


        public void Create(PhotoEntity photo)
        {
            if (ReferenceEquals(photo, null))
                throw new ArgumentNullException(nameof(photo));

            photoRepository.Create(photo.ToDalPhoto());
            uow.Commit();
        }

        public void Delete(PhotoEntity photo)
        {
            if (ReferenceEquals(photo, null))
                throw new ArgumentNullException(nameof(photo));

            photoRepository.Delete(photo.ToDalPhoto());
            uow.Commit();
        }

        public void Update(PhotoEntity photo)
        {
            if (ReferenceEquals(photo, null))
                throw new ArgumentNullException(nameof(photo));

            photoRepository.Update(photo.ToDalPhoto());
            uow.Commit();
        }

        public void DeleteRating(RatingEntity rating)
        {
            if (ReferenceEquals(rating, null))
                throw new ArgumentNullException(nameof(rating));

            photoRepository.DeleteRating(rating.ToDalRating());
            uow.Commit();
        }

        public void AddRating(RatingEntity rating)
        {
            if (ReferenceEquals(rating, null))
                throw new ArgumentNullException(nameof(rating));

            photoRepository.AddRating(rating.ToDalRating());
            uow.Commit();
        }

        public IEnumerable<PhotoEntity> AllUserPhotos(string userName)
        {
            return photoRepository.GetByPredicate(p => p.UserName.CompareTo(userName) == 0).Select(p => p.ToBLLPhoto());
        }

        public IEnumerable<PhotoEntity> AllUserPhotos(int userId)
        {
            throw new NotImplementedException();
        }


    }
}

