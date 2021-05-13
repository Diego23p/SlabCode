using SlabCode.DataAccess.DBOperations.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlabCode.DataAccess.DBOperations.Implementation
{
    public class ProjectDbOperations : IDbOperations
    {
        private readonly SlabCodeTestContext DbContext;

        public ProjectDbOperations(SlabCodeTestContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public void create(Project project)
        {
            var entity = new Project()
            {
                Name = project.Name,
                Description = project.Description,
                Initdate = project.Initdate,
                Finishdate = project.Finishdate,
                State = "En Proceso"
            };

            DbContext.Projects.Add(entity);
            DbContext.SaveChanges();
        }

        public Project GetById(string id)
        {
            return DbContext.Projects.Select(
                    s => new Project
                    {
                        Name = s.Name,
                        Description = s.Description,
                        Initdate = s.Initdate,
                        Finishdate = s.Finishdate,
                        State = s.State
                    })
                .FirstOrDefault(s => s.Name == id);
        }

        public void Update(Project project)
        {
            var entity = DbContext.Projects.FirstOrDefault(s => s.Name == project.Name);

            entity.Name = project.Name;
            entity.Description = project.Description;
            entity.Initdate = project.Initdate;
            entity.Finishdate = project.Finishdate;
            entity.State = project.State;

            DbContext.SaveChanges();
        }
    }
}
