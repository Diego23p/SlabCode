using System;

#nullable disable

namespace SlabCode.DataAccess
{
    public partial class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Executiondate { get; set; }
        public string Project { get; set; }
        public string State { get; set; }

        public virtual Project ProjectNavigation { get; set; }
    }
}
