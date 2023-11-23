using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Database;
using Task.Models.Entities;
using Task.Models.Mappers;
using Task.Models.Repositories;

namespace Task.Models.Services
{
    public class PersonService : IPersonRepository
    {
        private readonly DbConnection _connection;
        public PersonService(DbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Person> GetAll()
        {
            _connection.Open();
            string query = "SELECT * FROM Person";
            return _connection.ExecuteReader(query, (c => c.ToPerson()), null);
        }

        public int Insert(Person entity)
        {
            _connection.Open();
            string query = "INSERT INTO Person (FirstName, LastName) OUTPUT inserted.PersonId VALUES (@FirstName, @LastName)";
            return (int)_connection.ExecuteScalar(query, entity);
        }

        public void Update(Person entity)
        {
            _connection.Open();
            string query = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName WHERE PersonId = @Id";
            _connection.ExecuteNonQuery(query, entity);
        }
    }
}
