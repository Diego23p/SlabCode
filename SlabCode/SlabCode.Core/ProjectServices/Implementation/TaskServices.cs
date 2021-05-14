using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;
using SlabCode.DataAccess.DBOperations.Implementation;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class TaskServices : IServices
    {
        private readonly TaskDbOperations taskDbOperations;
        private readonly ProjectDbOperations projectDbOperations;

        public TaskServices(TaskDbOperations taskDbOperations, ProjectDbOperations projectDbOperations)
        {
            this.taskDbOperations = taskDbOperations;
            this.projectDbOperations = projectDbOperations;
        }

        public string create(Task task)
        {
            Project projectDb = projectDbOperations.getById(task.Project);
            if (projectDb.Initdate < task.Executiondate && projectDb.Finishdate > task.Executiondate)
            {
                taskDbOperations.create(task);
                return $"Task \"{task.Name}\" created";
            }
            return $"Task \"{task.Name}\" cannot be created, has an out of execution date range";

        }

        public string update(Task task)
        {
            Task taskDb = taskDbOperations.getById(task.Name);
            Project projectDb = projectDbOperations.getById(taskDb.Project);

            if (projectDb.Initdate < task.Executiondate && projectDb.Finishdate > task.Executiondate)
            {
                taskDb.Name = task.Name;
                taskDb.Description = task.Description;
                taskDb.Executiondate = task.Executiondate;

                taskDbOperations.update(taskDb);
                return $"Task \"{task.Name}\" updated";
            }
            return $"Task \"{task.Name}\" cannot be updated, has an out of execution date range";
        }

        public string delete(string id)
        {
            taskDbOperations.delete(id);
            return $"Task \"{id}\" deleted";
        }

        public string complete(string id)
        {
            Task taskDb = taskDbOperations.getById(id);

            taskDb.State = "Finalizado";

            taskDbOperations.update(taskDb);
            return $"Task \"{id}\" completed";
        }
    }
}
