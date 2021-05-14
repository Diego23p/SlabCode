using System.Collections.Generic;
using System.Linq;

namespace SlabCode.DataAccess.DBOperations.Implementation
{
    public class TaskDbOperations
    {
        private readonly SlabCodeTestContext DbContext;

        public TaskDbOperations(SlabCodeTestContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public void create(Task task)
        {
            var entity = new Task()
            {
                Name = task.Name,
                Description = task.Description,
                Executiondate = task.Executiondate,
                Project = task.Project,
                State = "En Proceso"
            };

            DbContext.Tasks.Add(entity);
            DbContext.SaveChanges();
        }

        public Task getById(string id)
        {
            return DbContext.Tasks.Select(
                    s => new Task
                    {
                        Name = s.Name,
                        Description = s.Description,
                        Executiondate = s.Executiondate,
                        Project = s.Project,
                        State = s.State
                    })
                .FirstOrDefault(s => s.Name == id);
        }

        public List<Task> getAllByProjectId(string projectId)
        {
            return DbContext.Tasks.Select(
                s => new Task
                {
                    Name = s.Name,
                    Description = s.Description,
                    Executiondate = s.Executiondate,
                    Project = s.Project,
                    State = s.State
                }).
                Where(s => s.Project == projectId).ToList();
        }

        public void update(Task task)
        {
            var entity = DbContext.Tasks.FirstOrDefault(s => s.Name == task.Name);

            entity.Name = task.Name;
            entity.Description = task.Description;
            entity.Executiondate = task.Executiondate;
            entity.Project = task.Project;
            entity.State = task.State;

            DbContext.SaveChanges();
        }

        public void delete(string taskId)
        {
            var entity = new Task()
            {
                Name = taskId
            };
            DbContext.Tasks.Attach(entity);
            DbContext.Tasks.Remove(entity);
            DbContext.SaveChanges();
        }
    }
}
