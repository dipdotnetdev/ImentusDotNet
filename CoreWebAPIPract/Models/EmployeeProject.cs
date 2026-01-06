namespace CoreWebAPIPract.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public Employees Employee { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
