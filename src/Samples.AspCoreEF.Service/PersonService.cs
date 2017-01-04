using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.DAL.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Service
{
    public interface IPersonService
    {
        void Add(Person Person);
        void Update(Person Person);
        void Delete(Person Person);
        IEnumerable<Person> GetAll();
        IEnumerable<Person> GetByKeyword(string keyword);
        Person GetById(long id);
    }
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
        }

        public void Add(Person Person)
        {
            _personRepository.Insert(Person);
        }
        public void Update(Person Person)
        {
            _personRepository.Update(Person);
        }
        public void Delete(Person Person)
        {
            _personRepository.Delete(Person);
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

        public Person GetById(long id)
        {
            return _personRepository.Get(id);
        }

        public IEnumerable<Person> GetByKeyword(string keyword)
        {
            return _personRepository.GetMulti(p => p.Name.Contains(keyword));
        }

        
    }
}
