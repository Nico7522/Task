using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModel = Task.Models.Entities.Task;


namespace Task.Models.Repositories
{
    public interface ITaskRepository
    {
        int Insert(TaskModel task);

        IEnumerable<TaskModel> GetAll();

        TaskModel GetById(int id);

        void Update(TaskModel task);

        void Assign(int taskId, int personId);

        void Finish(int taskId);

    }
}
