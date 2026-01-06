namespace CoreWebAPIPract.DTO_s
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
