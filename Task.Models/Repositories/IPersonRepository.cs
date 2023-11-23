using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Models.Entities;

namespace Task.Models.Repositories
{
    public interface IPersonRepository
    {
        int Insert(Person entity);

        IEnumerable<Person> GetAll();

        void Update(Person entity);

    }
}
