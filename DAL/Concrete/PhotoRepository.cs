using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interfaces;
using System.Data.Entity;
using DAL.Interfaces.Interfaces;
using DAL.Interfaces.DTO;
using ORM;
using DAL.Mappers;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly UnitOfWork context;

        public PhotoRepository(UnitOfWork uow)
        {
            if (ReferenceEquals(uow, null))
                throw new ArgumentNullException(nameof(uow));
            this.context = uow;
        }

        public DalPhoto GetById(int key)
        {
            var photo = context.Context.Set<Photos>().FirstOrDefault(a => a.PhotoId == key).ToDalPhoto();
            if (!ReferenceEquals(photo, null))
                return photo;
            return null;
        }

        public void Create(DalPhoto photo)
        {
            if (ReferenceEquals(photo, null))
                throw new ArgumentNullException(nameof(photo));

            var tags = photo.Tags.ToList();
            photo.Tags.Clear();
            var ormPhoto = photo.ToPhoto();

            foreach (var tag in tags)
            {
                var dbtag = context.Context.Set<Tags>().FirstOrDefault(t => t.Name == tag.Name);
                if (ReferenceEquals(dbtag, null))
                {
                    context.Context.Set<Tags>().Add(tag.ToTag());
                    context.Commit();
                    dbtag = context.Context.Set<Tags>().FirstOrDefault(t => t.Name == tag.Name);
                }
                context.Context.Set<Tags>().Attach(dbtag);
                dbtag.Photos = new List<Photos>();
                dbtag.Photos.Add(ormPhoto);
            }

        }

        public void Delete(DalPhoto photo)
        {
            context.Context.Set<Photos>().Remove(context.Context.Set<Photos>().Single(u => u.PhotoId == photo.Id));
        }

        public void Update(DalPhoto photo)
        {
            foreach (var r in photo.Ratings)
            {
                context.Context.Set<Ratings>().AddOrUpdate(r.ToRating());
            }

            context.Context.Set<Photos>().AddOrUpdate(photo.ToPhoto());
            context.Commit();
        }

        public void DeleteRating(DalRating rating)
        {
            context.Context.Set<Photos>().First(p => p.PhotoId == rating.PhotoId).Ratings.Remove(rating.ToRating());
            var ratingDB = context.Context.Set<Ratings>().First(r => r.UserName == rating.UserName && r.PhotoId == rating.PhotoId);
            context.Context.Set<Ratings>().Remove(ratingDB);
        }

        public void AddRating(DalRating rating)
        {
            var photo = context.Context.Set<Photos>().FirstOrDefault(p => p.PhotoId == rating.PhotoId);
            context.Context.Set<Photos>().Attach(photo);
            photo.Ratings.Add(rating.ToRating());
        }
        public IEnumerable<DalPhoto> GetAll() => context.Context.Set<Photos>().ToList().Select(a => a.ToDalPhoto());
        public IEnumerable<DalPhoto> GetByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var visitor = new Visitor<DalPhoto, Photos>(Expression.Parameter(typeof(Photos), predicate.Parameters[0].Name));
            return context.Context.Set<Photos>().Where(Expression.Lambda<Func<Photos, bool>>(visitor
                .Visit(predicate.Body), visitor.ParameterExp)).ToList().Select(photo => photo.ToDalPhoto());
        }
    }
}
