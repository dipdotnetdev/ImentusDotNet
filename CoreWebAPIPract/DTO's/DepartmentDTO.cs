namespace CoreWebAPIPract.DTO_s
{
    public class DepartmentDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
