using System;
using System.Collections.Generic;

#nullable disable

namespace SlabCode.DataAccess
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Initdate { get; set; }
        public DateTime? Finishdate { get; set; }
        public string State { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
