namespace CoreWebAPIPract.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Budget { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
