using SlabCode.Core.ProjectServices.Contract;
using SlabCode.DataAccess;
using SlabCode.DataAccess.DBOperations.Implementation;
using System.Collections.Generic;

namespace SlabCode.Core.ProjectServices.Implementation
{
    public class ProjectServices : IServices
    {
        private readonly ProjectDbOperations projectDbOperations;
        private readonly TaskDbOperations taskDbOperations;

        public ProjectServices(ProjectDbOperations projectDbOperations, TaskDbOperations taskDbOperations)
        {
            this.taskDbOperations = taskDbOperations;
            this.projectDbOperations = projectDbOperations;
        }

        public string create(Project project)
        {
            if (project.Initdate < project.Finishdate)
            {
                projectDbOperations.create(project);
                return $"Project \"{project.Name}\" created";
            }
            return $"Project \"{project.Name}\" not created, finishDate must be later to initDate";
        }

        public string update(Project project)
        {
            if (!hasSubsequentTasks(project))
            {
                Project projectDb = projectDbOperations.getById(project.Name);

                projectDb.Name = project.Name;
                projectDb.Description = project.Description;
                projectDb.Finishdate = project.Finishdate;

                projectDbOperations.update(projectDb);
                return $"Project \"{project.Name}\" updated";
            }
            return $"Project \"{project.Name}\" cannot be updated, has subsequent task(s)";
        }

        private bool hasSubsequentTasks(Project project)
        {
            List<Task> taksList = taskDbOperations.getAllByProjectId(project.Name);
            foreach (Task task in taksList)
            {
                if (task.Executiondate > project.Finishdate) return true;
            }
            return false;
        }

        public string delete(string id)
        {
            List<Task> taksList = taskDbOperations.getAllByProjectId(id);
            int taskCount = 0;
            foreach (Task task in taksList)
            {
                taskDbOperations.delete(task.Name);
                taskCount++;
            }

            projectDbOperations.delete(id);
            return $"Project \"{id}\" and \"{taskCount}\" relationated tasks deleted";
        }

        public string complete(string id)
        {
            if (!hasOpenTasks(id))
            {
                Project projectDb = projectDbOperations.getById(id);

                projectDb.State = "Finalizado";

                projectDbOperations.update(projectDb);
                return $"Project \"{id}\" completed";
            }
            return $"Project \"{id}\" cannot be complete, has open task(s)";
        }

        private bool hasOpenTasks(string id)
        {
            List<Task> taksList = taskDbOperations.getAllByProjectId(id);
            foreach (Task task in taksList)
            {
                if (task.State.Equals("En Proceso")) return true;
            }
            return false;
        }
    }
}
