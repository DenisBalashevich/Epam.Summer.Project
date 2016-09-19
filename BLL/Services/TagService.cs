using System;
using System.Collections;
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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork uow;
        private readonly ITagRepository repository;
        public TagService(IUnitOfWork uow, ITagRepository repository)
        {
            if (ReferenceEquals(uow, null) || ReferenceEquals(repository, null))
                throw new ArgumentNullException(uow == null ? nameof(uow) : nameof(repository));
            this.uow = uow;
            this.repository = repository;
        }
        public TagEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return repository.GetById(id).ToBLLTag();
        }

        public IEnumerable<TagEntity> GetAll() => repository.GetAll().Select(r => r.ToBLLTag());

        public void Create(TagEntity tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException(nameof(tag));

            repository.Create(tag.ToDalTag());
            uow.Commit();
        }

        public void Delete(TagEntity tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException(nameof(tag));

            repository.Delete(tag.ToDalTag());
            uow.Commit();
        }

        public void Update(TagEntity tag)
        {
            if (ReferenceEquals(tag, null))
                throw new ArgumentNullException(nameof(tag));

            repository.Update(tag.ToDalTag());
            uow.Commit();
        }

        public TagEntity GetTagByName(string name)
        {
            if (ReferenceEquals(name, null))
                throw new ArgumentNullException(nameof(name));

            return repository.GetTagByName(name)?.ToBLLTag();
        }

        public IEnumerable<TagEntity> FindTags(string filter)
        {
      
            return repository.GetAll().Where(a => a.Name.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase))
                            .Select(a => a.ToBLLTag())
                            .Distinct();
        }
    }
}
