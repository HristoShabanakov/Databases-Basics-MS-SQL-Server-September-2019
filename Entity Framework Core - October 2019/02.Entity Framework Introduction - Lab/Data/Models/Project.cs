using System;
using System.Collections.Generic;

namespace DatabaseDemoEFCore.Data.Models
{
    public  class Project
    {
        public Project()
        {
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<EmployeeProjects> EmployeesProjects { get; set; } = new HashSet<EmployeeProjects>();
    }
}
