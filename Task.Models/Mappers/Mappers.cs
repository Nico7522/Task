using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Models.Entities;
using TaskModel = Task.Models.Entities.Task;


namespace Task.Models.Mappers
{
    internal static class Mappers
    {
        internal static Person ToPerson(this DbDataReader person)
        {
            return new Person()
            {
                Id = (int)person["PersonId"],
                FirstName = (string)person["FirstName"],
                LastName = (string)person["LastName"]
            };
        }

        internal static TaskModel ToTask(this DbDataReader task)
        {
            return new TaskModel()
            {
                Id = (int)task["TaskId"],
                Title = (string)task["Title"],
                Description = (string)task["Description"],
                IsFinished = (bool)task["IsFinished"],
                PersonId = (task["PersonId"] != DBNull.Value) ? (int)task["PersonId"] : null,
        };
        }
    }
}
