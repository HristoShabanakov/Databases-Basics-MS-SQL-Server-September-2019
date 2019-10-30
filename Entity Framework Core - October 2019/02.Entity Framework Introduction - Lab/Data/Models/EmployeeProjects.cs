namespace DatabaseDemoEFCore.Data.Models
{
    public  class EmployeeProjects
    {
        public int EmployeeId { get; set; }
        public  Employee Employee { get; set; }
        public int ProjectId { get; set; }
        public  Project Project { get; set; }
    }
}
