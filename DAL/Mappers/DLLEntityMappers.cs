using System.Linq;
using ORM;
using DAL.Interfaces.DTO;
namespace DAL.Mappers
{
    public static class DLLEntityMappers
    {
        public static Users ToUser(this DalUser userDal)
        {
            return new Users()
            {
                Id = userDal.Id,
                Name = userDal.Name,
                Password = userDal.Password,
                Email = userDal.Email,
                Roles = userDal.Roles.Select(a => a.ToRole()).ToList(),
                Photos = userDal.Photos.Select(a => a.ToPhoto()).ToList()
            };
        }

        public static DalUser ToDalUser(this Users userDal)
        {
            return new DalUser()
            {
                Id = userDal.Id,
                Name = userDal.Name,
                Password = userDal.Password,
                Email = userDal.Email,
                Roles = userDal.Roles.Select(a => a.ToDalRole()).ToList(),
                Photos = userDal.Photos.Select(a => a.ToDalPhoto()).ToList()
            };
        }

        public static Photos ToPhoto(this DalPhoto photoDal)
        {
            return new Photos()
            {
                Name = photoDal.Name,
                Date = photoDal.Date,
                PhotoId = photoDal.Id,
                Picture = photoDal.Picture,
                Tags = photoDal.Tags.Select(a => a.ToTag()).ToList(),
                Ratings = photoDal.Ratings?.Select(a => a.ToRating()).ToList(),
                UserName = photoDal.UserName
            };
        }

        public static DalPhoto ToDalPhoto(this Photos photoDal)
        {
            return new DalPhoto()
            {
                Name = photoDal.Name,
                Date = photoDal.Date,
                Id = photoDal.PhotoId,
                Picture = photoDal.Picture,
                Tags = photoDal.Tags.Select(t => new DalTag
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                Ratings = photoDal.Ratings.Select(a => a.ToDalRating()).ToList(),
                UserName = photoDal.UserName
            };
        }

        public static Roles ToRole(this DalRole roleDal)
        {
            return new Roles()
            {
                RoleId = roleDal.Id,
                RoleName = roleDal.RoleName,
                Description = roleDal.Description
            };
        }

        public static DalRole ToDalRole(this Roles roleDal)
        {
            return new DalRole()
            {
                Id = roleDal.RoleId,
                RoleName = roleDal.RoleName,
                Description = roleDal.Description
            };
        }

        public static Ratings ToRating(this DalRating ratingDal)
        {
            return new Ratings
            {
                PhotoId = ratingDal.PhotoId,
                RatingId = ratingDal.Id,
                UserName = ratingDal.UserName

            };
        }

        public static DalRating ToDalRating(this Ratings ratingDal)
        {
            return new DalRating()
            {
                Id = ratingDal.RatingId,
                UserName = ratingDal.UserName,
                PhotoId = ratingDal.PhotoId
            };
        }

        public static Tags ToTag(this DalTag tagDal)
        {
            return new Tags()
            {
                Id = tagDal.Id,
                Name = tagDal.Name,
                Photos = tagDal.Photos?.Select(a => a.ToPhoto()).ToList()
            };
        }

        public static DalTag ToDalTag(this Tags tagDal)
        {
            return new DalTag()
            {
                Id = tagDal.Id,
                Name = tagDal.Name,
                Photos = tagDal.Photos?.Select(a => a.ToDalPhoto()).ToList()
            };

        }

        public static DalInformationUsers ToDalInformationUsers(this InformationUsers information )
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

        public static InformationUsers ToInformationUsers(this DalInformationUsers information)
        {
            return new InformationUsers
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
