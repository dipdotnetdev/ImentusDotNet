using CoreWebAPIPract.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIPract.Repository
{
    public interface ICompanyRepository
    {
        Task CreateEmployee(EmployeeDTO employeeDTO);
        Task CreateDepartment(DepartmentDTO departmentDTO);
        Task CreateProject(ProjectDTO projectDTO);
        Task CreateEmployeeProject(EmployeeProjectDTO employeeProjectDTO);
        List<EmployeeDTO> GetAllEmployees();
    }
}
