using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces.Interfaces;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Entities;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;
        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            if (ReferenceEquals(uow, null) || ReferenceEquals(repository, null))
                throw new ArgumentNullException(uow == null ? nameof(uow) : nameof(repository));
            this.uow = uow;
            this.roleRepository = repository;
        }
        public RoleEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return roleRepository.GetById(id).ToBLLRole();
        }

        public IEnumerable<RoleEntity> GetAll() => roleRepository.GetAll().Select(role => role.ToBLLRole());

        public void Create(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException(nameof(role));

            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }

        public void Delete(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException(nameof(role));

            roleRepository.Delete(role.ToDalRole());
            uow.Commit();
        }

        public void Update(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException(nameof(role));

            roleRepository.Update(role.ToDalRole());
            uow.Commit();
        }

        public RoleEntity GetRoleByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
