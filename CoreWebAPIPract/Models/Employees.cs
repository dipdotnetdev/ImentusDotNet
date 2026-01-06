namespace CoreWebAPIPract.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
