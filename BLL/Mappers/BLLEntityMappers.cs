using System.Linq;
using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class BLLEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                Password = userEntity.Password,
                Email = userEntity.Email,
                Roles = userEntity.Roles?.Select(a => a.ToDalRole()).ToList(),
                Photos = userEntity.Photos?.Select(a => a.ToDalPhoto()).ToList()
            };
        }

        public static UserEntity ToBLLUser(this DalUser userDal)
        {
            return new UserEntity()
            {
                Id = userDal.Id,
                Name = userDal.Name,
                Surname = userDal.Surname,
                Password = userDal.Password,
                Email = userDal.Email,
                Roles = userDal.Roles.Select(a => a.ToBLLRole()).ToList(),
                Photos = userDal.Photos.Select(a => a.ToBLLPhoto()).ToList()
            };
        }

        public static DalPhoto ToDalPhoto(this PhotoEntity photoEntity)
        {
            return new DalPhoto()
            {
                Name = photoEntity.Name,
                Date = photoEntity.Date,
                Id = photoEntity.Id,
                Picture = photoEntity.Picture,
                Tags = photoEntity.Tags?.Select(t => t.ToDalTag()).ToList(),
                Ratings = photoEntity.Ratings?.Select(a => a.ToDalRating()).ToList(),
                UserName = photoEntity.UserName,
                UserId = photoEntity.UserId
            };
        }

        public static PhotoEntity ToBLLPhoto(this DalPhoto photoDal)
        {
            return new PhotoEntity()
            {
                Name = photoDal.Name,
                Date = photoDal.Date,
                Id = photoDal.Id,
                Picture = photoDal.Picture,
                Tags = photoDal.Tags.Select(t => new TagEntity
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                Ratings = photoDal.Ratings?.Select(a => a.ToBLLRating()).ToList(),
                Description = photoDal.Description,
                UserName = photoDal.UserName,
                UserId = photoDal.UserId
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                RoleName = roleEntity.Name, 
                Description = roleEntity.Description
            };
        }

        public static RoleEntity ToBLLRole(this DalRole roleDal)
        {
            return new RoleEntity()
            {
                Id = roleDal.Id,
                Name = roleDal.RoleName,
                Description = roleDal.Description
            };
        }

        public static DalRating ToDalRating(this RatingEntity ratingEntity)
        {
            return new DalRating()
            {
                Id = ratingEntity.Id, 
                UserName = ratingEntity.UserName,
                PhotoId = ratingEntity.PhotoId
            };
        }

        public static RatingEntity ToBLLRating(this DalRating ratingDal)
        {
            return new RatingEntity()
            {
                Id = ratingDal.Id,
                UserName = ratingDal.UserName, 
                PhotoId = ratingDal.PhotoId
            };
        }

        public static DalTag ToDalTag(this TagEntity tagDal)
        {
            return new DalTag()
            {
                Id = tagDal.Id,
                Name = tagDal.Name
            };
        }

        public static TagEntity ToBLLTag(this DalTag tagDal)
        {
            return new TagEntity()
            {
                Id = tagDal.Id,
                Name = tagDal.Name,
                Photos = tagDal.Photos.Select(a => a.ToBLLPhoto()).ToList()
            };
        }
        public static DalInformationUsers ToDalInformationUsers(this UserInformationEntity information)
        {
            return new DalInformationUsers
            {
                Id = information.Id,
                Age = information.Age,
                Avatar = information.Avatar,
                FirstName = information.FirstName,
                LastName = information.LastName,
                UserId = information.UserId
            };
        }

        public static UserInformationEntity ToBLLInformationUsers(this DalInformationUsers information)
        {
            return new UserInformationEntity
            {
                Id = information.Id,
                Age = information.Age,
                Avatar = information.Avatar,
                FirstName = information.FirstName,
                LastName = information.LastName,
                UserId = information.UserId
            };
        }
    }
}
