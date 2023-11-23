using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModel = Task.Models.Entities.Task;
using Task.Models.Repositories;
using System.Data.Common;
using Task.Database;
using Task.Models.Mappers;

namespace Task.Models.Services
{
    public class TaskService : ITaskRepository
    {
        private readonly DbConnection _connection;
        public TaskService(DbConnection connection) {
            _connection = connection;
        }

        public void Assign(int taskId, int personId)
        {
            _connection.Open();
            string query = "UPDATE Task SET PersonId = @PersonId WHERE TaskId = @TaskId";
            _connection.ExecuteNonQuery(query, new { TaskId = taskId, PersonId = personId });
        }

        public void Finish(int taskId)
        {
            _connection.Open();
            string query = "UPDATE Task SET IsFinished = 1 WHERE TaskId = @TaskId";
            _connection.ExecuteNonQuery(query, new { TaskId = taskId });
        }

        public IEnumerable<TaskModel> GetAll()
        {
            _connection.Open();
            string query = "SELECT * FROM Task";
            return _connection.ExecuteReader(query, (t => t.ToTask()), null);
        }

        public TaskModel GetById(int id)
        {
            _connection.Open();
            string query = "SELECT * FROM Task WHERE TaskId = @TaskId";
            return _connection.ExecuteReader(query, (t => t.ToTask()), new { TaskId = id }).First();
        }

        public int Insert(TaskModel task)
        {
            _connection.Open();
            string query = "INSERT INTO Task (Title, Description, IsFinished, PersonId) OUTPUT inserted.TaskId VALUES (@Title, @Description, @IsFinished, @PersonId)";
            return (int)_connection.ExecuteScalar(query, task);
            
        }

        public void Update(TaskModel task)
        {
            _connection.Open();
            string query = "UPDATE Task SET Title = @Title, Description = @Description WHERE TaskId = @Id";
            _connection.ExecuteNonQuery(query, task);
        }
    }
}
