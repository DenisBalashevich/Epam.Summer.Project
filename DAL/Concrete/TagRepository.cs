using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interfaces;
using DAL.Mappers;
using DAL.Interfaces.DTO;
using ORM;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace DAL.Concrete
{
    public class TagRepository : ITagRepository
    {
        private readonly UnitOfWork context;

        public TagRepository(UnitOfWork uow)
        {
            if (ReferenceEquals(uow, null))
                throw new ArgumentNullException(nameof(uow));
            this.context = uow;
        }

        public DalTag GetById(int key)
        {
            var tag = context.Context.Set<Tags>().FirstOrDefault(a => a.Id == key).ToDalTag();
            if (!ReferenceEquals(tag, null))
                return tag;
            return null;
        }

        public DalTag GetTagByName(string name)
        {
            var tag = context.Context.Set<Tags>().FirstOrDefault(t => t.Name == name);
            if (!ReferenceEquals(tag, null))
                return tag.ToDalTag();
            return null;
        }

        public void Create(DalTag tag)
        {
            context.Context.Set<Tags>().Add(tag.ToTag());
        }

        public void Delete(DalTag tag)
        {
            context.Context.Set<Tags>().Remove(context.Context.Set<Tags>().Single(u => u.Id == tag.Id));
        }

        public void Update(DalTag tag)
        {
            foreach (var t in tag.Photos)
            {
                context.Context.Set<Photos>().AddOrUpdate(t.ToPhoto());
            }
            context.Context.Set<Tags>().AddOrUpdate(tag.ToTag());
            context.Commit();
        }
        public IEnumerable<DalTag> GetAll() => context.Context.Set<Tags>().ToList().Select(a => a.ToDalTag());

        public IEnumerable<DalTag> GetByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            var visitor = new Visitor<DalTag, Tags>(Expression.Parameter(typeof(Tags), predicate.Parameters[0].Name));
            return context.Context.Set<Tags>().Where(Expression.Lambda<Func<Tags, bool>>(visitor
                .Visit(predicate.Body), visitor.ParameterExp)).ToList().Select(tag => tag.ToDalTag());
        }
    }
}
