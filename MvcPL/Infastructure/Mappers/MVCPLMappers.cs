using System.Collections.Generic;
using System.Linq;
using MvcPL.ViewModels;
using BLL.Interfaces.Entities;
using BLL.Services;

namespace MvcPL.Infastructure.Mappers
{
    public static class MVCPLMappers
    {
        public static UserEntity ToBllUser(this UserViewModel user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Roles = user.Roles.Select(a => a.ToBLLRole()).ToList()
            };
        }

        public static UserViewModel ToMVCUser(this UserEntity userDal)
        {
            return new UserViewModel()
            {
                Id = userDal.Id,
                Name = userDal.Name,
                Password = userDal.Password,
                Email = userDal.Email,
                Roles = userDal.Roles.Select(a => a.ToMVCRole()).ToList()
            };
        }

        public static PhotoEntity ToBLLPhoto(this PhotoViewModel photoDal)
        {
            return new PhotoEntity()
            {
                Name = photoDal.Name,
                Date = photoDal.Date,
                Id = photoDal.Id,
                Picture = photoDal.Picture,
                Tags = photoDal.Tags?.Select(a => a.ToBllTag()).ToList(),
                Ratings = photoDal.Ratings?.Select(a => a.ToBLLRating()).ToList(),
                UserName = photoDal.UserName,
                UserId = photoDal.UserId

            };
        }

        public static PhotoViewModel ToMVCPhoto(this PhotoEntity photoDal)
        {
            return new PhotoViewModel()
            {
                Name = photoDal.Name,
                Date = photoDal.Date,
                Id = photoDal.Id,
                Picture = photoDal.Picture,
                Tags = photoDal.Tags.Select(t => new TagViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                Ratings = photoDal.Ratings?.Select(a => a.ToMVCRating()).ToList(),
                UserName = photoDal.UserName,
                UserId = photoDal.UserId
            };
        }

        public static RoleEntity ToBLLRole(this RoleViewModel roleDal)
        {
            return new RoleEntity()
            {
                Id = roleDal.Id,
                Name = roleDal.RoleName, 
                //Description
            };
        }

        public static RoleViewModel ToMVCRole(this RoleEntity roleDal)
        {
            return new RoleViewModel()
            {
                Id = roleDal.Id,
                RoleName = roleDal.Name,
                //Description
            };
        }

        public static RatingEntity ToBLLRating(this RatingViewModel ratingDal)
        {
            return new RatingEntity()
            {

                UserName = ratingDal.UserName,
                PhotoId = ratingDal.PhotoID.Value,
                Id = ratingDal.ID
            };
        }

        public static RatingViewModel ToMVCRating(this RatingEntity ratingDal)
        {
            return new RatingViewModel()
            {

                UserName = ratingDal.UserName,
                PhotoID = ratingDal.PhotoId,
                ID = ratingDal.Id
            };
        }
        
        public static TagEntity ToBllTag(this TagViewModel tagDal)
        {
            return new TagEntity()
            {
                Id = tagDal.Id,
                Name = tagDal.Name
            };
        }

        public static TagViewModel ToMVCTag(this TagEntity tagDal)
        {
            return new TagViewModel()
            {
                Id = tagDal.Id,
                Name = tagDal.Name,
                Photos = tagDal.Photos.Select(a=>a.ToMVCPhoto()).ToList()
            };
        }

        public static ICollection<TagEntity> ToTagEntitys(this ICollection<TagViewModel> tags)
        {
            var result = new List<TagEntity>();
            foreach (var tag in tags)
            {
                result.Add(new TagEntity
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        public static ICollection<TagViewModel> ToMVCTags(this ICollection<TagEntity> tags)
        {
            var result = new List<TagViewModel>();
            foreach (var tag in tags)
            {
                result.Add(new TagViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        public static UserInformationViewModel ToMVCInformationUsers(this UserInformationEntity information)
        {
            return new UserInformationViewModel
            {
                Id = information.Id,
                Age = information.Age,
                Avatar = information.Avatar,
                FirstName = information.FirstName,
                LastName = information.LastName,
                UserId = information.UserId
            };
        }

        public static UserInformationEntity ToBLLInformationUsers(this UserInformationViewModel information)
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