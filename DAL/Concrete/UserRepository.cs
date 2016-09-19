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
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork context;

        public UserRepository(UnitOfWork uow)
        {
            if (ReferenceEquals(uow, null))
                throw new ArgumentNullException(nameof(uow));
            this.context = uow;
        }

        public DalUser GetById(int key)
        {
            var user = context.Context.Set<Users>().FirstOrDefault(a => a.Id == key).ToDalUser();
            if (!ReferenceEquals(user, null))
                return user;
            return null;
        }

        public DalUser GetByEmail(string name)
        {
            var user = context.Context.Set<Users>().FirstOrDefault(t => t.Email == name);
            if (!ReferenceEquals(user, null))
                return user.ToDalUser();
            return null;
        }

        public DalUser GetByName(string name)
        {
            var user = context.Context.Set<Users>().FirstOrDefault(t => t.Name == name);
            if (!ReferenceEquals(user, null))
                return user.ToDalUser();
            return null;
        }

        public void Create(DalUser user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException(nameof(user));

            var userRoles = user.Roles.ToList();
            user.Roles.Clear();
            var ormUser = user.ToUser();

            foreach (var role in userRoles)
            {
                var dbRole = context.Context.Set<Roles>().FirstOrDefault(t => t.RoleName == role.RoleName);
                if (ReferenceEquals(dbRole, null))
                {
                    context.Context.Set<Roles>().Add(role.ToRole());
                    context.Commit();
                    dbRole = context.Context.Set<Roles>().FirstOrDefault(t => t.RoleName == role.RoleName);
                }
                if (ReferenceEquals(dbRole.Users, null))
                    dbRole.Users = new List<Users>();
                dbRole.Users.Add(ormUser);
            }
        }

        public void Delete(DalUser user)
        {
            context.Context.Set<Users>().Remove(context.Context.Set<Users>().Single(u => u.Id == user.Id));
        }

        public void Update(DalUser user)
        {
            foreach (var t in user.Photos)
            {
                context.Context.Set<Photos>().AddOrUpdate(t.ToPhoto());
            }

            foreach (var t in user.Roles)
            {
                context.Context.Set<Roles>().AddOrUpdate(t.ToRole());
            }
            context.Context.Set<Users>().AddOrUpdate(user.ToUser());
        }
        public IEnumerable<DalUser> GetAll() => context.Context.Set<Users>().ToList().Select(a => a.ToDalUser());

        public IEnumerable<DalUser> GetByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new Visitor<DalUser, Users>(Expression.Parameter(typeof(Users), predicate.Parameters[0].Name));
            return context.Context.Set<Users>().Where(Expression.Lambda<Func<Users, bool>>(visitor
                .Visit(predicate.Body), visitor.ParameterExp)).ToList().Select(user => user.ToDalUser());
        }
    }
}
