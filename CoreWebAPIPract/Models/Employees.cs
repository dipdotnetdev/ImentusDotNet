namespace CoreWebAPIPract.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
