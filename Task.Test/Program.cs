// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using Microsoft.Data.SqlClient;

using System;
using Task.Models.Services;
using Task.Models.Repositories;
using Task.Models.Entities;
using TaskModel = Task.Models.Entities.Task;

Console.WriteLine("Hello, World!");
string connectionString = "Data Source=DESKTOP-IFNFMI9;Initial Catalog=TaskDB;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;";

DbConnection connection = new SqlConnection(connectionString);

// Person
IPersonRepository personRepository = new PersonService(connection);
Person person1 = new Person()
{
    Id = 1,
    FirstName = "Jean",
    LastName = "Dujardin"
};
#region Insert 

//try
//{
//    int newPersonId = personRepository.Insert(person1);
//    Console.WriteLine($"La nouvelle personne a été ajoutée, son id est {newPersonId}");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}

#endregion

#region GetAll 
//List<Person> personList = personRepository.GetAll().ToList();

//foreach (var person in personList)
//{
//    Console.Write($"{person.Id} {person.FirstName} {person.LastName}");
//}

#endregion

#region Update

//try
//{
//    personRepository.Update(person1);
//    Console.WriteLine("Les changements ont été effectués");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}

#endregion


// Task
ITaskRepository taskRepository = new TaskService(connection);

TaskModel task = new TaskModel()
{
    Id = 3,
    Title = "Faire les courses",
    Description = "Je dois acheter du pain ce soir",
};
#region Insert

//try
//{
//    int newTaskId = taskRepository.Insert(task);
//    Console.WriteLine($"L'id de la nouvelle tache est {newTaskId}");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}


#endregion

#region GetAll()

//List<TaskModel> taskList = taskRepository.GetAll().ToList();

//foreach (var t in taskList)
//{
//    Console.Write($"{t.Id} {t.Title} {t.Description} {t.IsFinished} {t.PersonId}");
//}

#endregion

#region Update

//try
//{
//    taskRepository.Update(task);
//    Console.WriteLine("La modification a été effectuée");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}

#endregion

#region GetById

//try
//{
//    TaskModel myTask = taskRepository.GetById(3);
//    Console.Write($"{myTask.Id} {myTask.Title} {myTask.Description} {myTask.Description} {myTask.PersonId}");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}


#endregion

#region Assign

//try
//{
//    taskRepository.Assign(3, 2);
//    Console.WriteLine("La personne a été indiquée avec succès");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}


#endregion

#region Finish

//try
//{
//    taskRepository.Finish(6);
//    Console.WriteLine("La tache a été cloturée avec succès");
//}
//catch (Exception ex)
//{

//    Console.WriteLine(ex.Message);
//}

#endregion

try
{
    List<Person> people = personRepository.GetAll().ToList();
    List<TaskModel> tasks = taskRepository.GetAll().ToList();
    var tasksWithPerson = tasks.Where(t => t.IsFinished == false).Join(people, t => t.Id, p => p.Id, (t, p) => new { FullName = p.FirstName + p.LastName, TaskTitle = t.Title, TaskDescription = t.Description });

    foreach (var t in tasksWithPerson)
    {
        Console.WriteLine($"Nom: {t.FullName} doit {t.TaskTitle} qui est {t.TaskDescription}");
    }
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
