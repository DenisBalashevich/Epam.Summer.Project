using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
   public  class UserInformationRepository:IUserInformationRepository
    {
        private readonly UnitOfWork context;

        public UserInformationRepository(UnitOfWork uow)
        {
            if (ReferenceEquals(uow, null))
                throw new ArgumentNullException(nameof(uow));
            this.context = uow;
        }

        public DalInformationUsers GetById(int key)
        {
            var information =
                context.Context.Set<InformationUsers>().FirstOrDefault(a => a.Id == key).ToDalInformationUsers();
            if (!ReferenceEquals(information, null))
                return information;
            return null;
        }

        public DalInformationUsers GetByUserId(int key)
        {
            var information =
                context.Context.Set<InformationUsers>().FirstOrDefault(a => a.UserId == key).ToDalInformationUsers();
            if (!ReferenceEquals(information, null))
                return information;
            return null;
        }

        public void Create(DalInformationUsers information)
        {
            if (ReferenceEquals(null, information))
                throw new ArgumentNullException(nameof(information));
            context.Context.Set<InformationUsers>().Add(information.ToInformationUsers());
        }

        public void Delete(DalInformationUsers information)
        {
            if (ReferenceEquals(null, information))
                throw new ArgumentNullException(nameof(information));
            context.Context.Set<InformationUsers>().Remove(context.Context.Set<InformationUsers>().Single(u => u.Id == information.Id));
        }

        public void Update(DalInformationUsers information)
        {
            var profile = context.Context.Set<InformationUsers>().FirstOrDefault(p => p.UserId == information.UserId);

            if (!ReferenceEquals(information.FirstName, null))
                profile.FirstName = information.FirstName;
            if (!ReferenceEquals(information.LastName,null))
                profile.LastName = information.LastName;
            if (information.Age != 0)
                profile.Age = information.Age;
            if (!ReferenceEquals(information.Avatar,null))
                profile.Avatar = information.Avatar;
        }
        public IEnumerable<DalInformationUsers> GetAll() => context.Context.Set<InformationUsers>().ToList().Select(a => a.ToDalInformationUsers());

        public IEnumerable<DalInformationUsers> GetByPredicate(Expression<Func<DalInformationUsers, bool>> predicate)
        {
            var visitor = new Visitor<DalInformationUsers, InformationUsers>(Expression.Parameter(typeof(InformationUsers), predicate.Parameters[0].Name));
            return context.Context.Set<InformationUsers>().Where(Expression.Lambda<Func<InformationUsers, bool>>(visitor
                .Visit(predicate.Body), visitor.ParameterExp)).ToList().Select(information => information.ToDalInformationUsers());
        }
    }
}
