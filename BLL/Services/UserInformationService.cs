using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Interfaces;
using BLL.Mappers;
using DAL.Interfaces.Interfaces;

namespace BLL.Services
{
    public class UserInformationService:IUserInformationService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserInformationRepository informationRepository;
        public UserInformationService(IUnitOfWork uow, IUserInformationRepository repository)
        {
            if (ReferenceEquals(uow, null) || ReferenceEquals(repository, null))
                throw new ArgumentNullException(uow == null ? nameof(uow) : nameof(repository));
            this.uow = uow;
            this.informationRepository = repository;
        }
        public UserInformationEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return informationRepository.GetById(id).ToBLLInformationUsers();
        }

        public UserInformationEntity GetByUserId(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return informationRepository.GetByUserId(id).ToBLLInformationUsers();
        }

        public IEnumerable<UserInformationEntity> GetAll() => informationRepository.GetAll().Select(model => model.ToBLLInformationUsers());

        public void Create(UserInformationEntity information)
        {
            if (ReferenceEquals(information, null))
                throw new ArgumentNullException(nameof(information));

            informationRepository.Create(information.ToDalInformationUsers());
            uow.Commit();
        }

        public void Delete(UserInformationEntity information)
        {
            if (ReferenceEquals(information, null))
                throw new ArgumentNullException(nameof(information));

            informationRepository.Delete(information.ToDalInformationUsers());
            uow.Commit();
        }

        public void Update(UserInformationEntity information)
        {
            if (ReferenceEquals(information, null))
                throw new ArgumentNullException(nameof(information));

            informationRepository.Update(information.ToDalInformationUsers());
            uow.Commit();
        }
    }
}
