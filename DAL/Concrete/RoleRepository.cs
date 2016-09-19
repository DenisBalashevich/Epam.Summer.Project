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
    public class RoleRepository : IRoleRepository
    {
        private readonly UnitOfWork context;

        public RoleRepository(UnitOfWork uow)
        {
            if (ReferenceEquals(uow, null))
                throw new ArgumentNullException(nameof(uow));
            this.context = uow;
        }

        public DalRole GetById(int key)
        {
            var role = context.Context.Set<Roles>().FirstOrDefault(a => a.RoleId == key).ToDalRole();
            if (!ReferenceEquals(role, null))
                return role;
            return null;
        }

        public void Create(DalRole role)
        {
            if (ReferenceEquals(null, role))
                throw new ArgumentNullException(nameof(role));
            context.Context.Set<Roles>().Add(role.ToRole());
        }

        public void Delete(DalRole role)
        {
            if (ReferenceEquals(null, role))
                throw new ArgumentNullException(nameof(role));
            context.Context.Set<Roles>().Remove(context.Context.Set<Roles>().Single(u => u.RoleId == role.Id));
        }

        public void Update(DalRole role)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DalRole> GetAll() => context.Context.Set<Roles>().ToList().Select(a => a.ToDalRole());

        public IEnumerable<DalRole> GetByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new Visitor<DalRole, Roles>(Expression.Parameter(typeof(Roles), predicate.Parameters[0].Name));
            return context.Context.Set<Roles>().Where(Expression.Lambda<Func<Roles, bool>>(visitor
                .Visit(predicate.Body), visitor.ParameterExp)).ToList().Select(role => role.ToDalRole());
        }
    }
}
