using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interfaces;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Entities;
using BLL.Mappers;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository repository;
        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            if (ReferenceEquals(uow, null) || ReferenceEquals(repository, null))
                throw new ArgumentNullException(uow == null ? nameof(uow) : nameof(repository));
            this.uow = uow;
            this.repository = repository;
        }
        public UserEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return repository.GetById(id).ToBLLUser();
        }

        public IEnumerable<UserEntity> GetAll() => repository.GetAll().Select(r => r.ToBLLUser());

        public void Create(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException(nameof(user));

            repository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException(nameof(user));

            repository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Update(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException(nameof(user));

            repository.Update(user.ToDalUser());
            uow.Commit();
        }

        public UserEntity GetUserByName(string name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException(nameof(name));

            return repository.GetByName(name).ToBLLUser();
        }

        public UserEntity GetUserByEmail(string name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException(nameof(name));

            return repository.GetByEmail(name)?.ToBLLUser();
        }
    }
}
