using CoreWebAPIPract.DTO_s;
using CoreWebAPIPract.IdentityBasedAuth;
using CoreWebAPIPract.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIPract.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateEmployee(EmployeeDTO employees)
        {
            _context.Employees.Add(new Employees
            {
                Name = employees.Name,
                Salary = employees.Salary,
                Email = employees.Email,
                JoinDate = DateTime.Now,
                DepartmentId = employees.DepartmentId
            });
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task CreateDepartment(DepartmentDTO department)
        {
            _context.Departments.Add(new Department
            {
                Name = department.Name,
                Location = department.Location,
            });
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task CreateProject(ProjectDTO project)
        {
            _context.Projects.Add(new Project
            {
                ProjectName = project.ProjectName,
                Budget = project.Budget,
            });
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task CreateEmployeeProject(EmployeeProjectDTO employeeProject)
        {
            _context.EmployeeProjects.Add(new EmployeeProject
            {
                EmployeeId = employeeProject.EmployeeId,
                ProjectId = employeeProject.ProjectId,
            });

            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public List<EmployeeDTO> GetEmployee(int id)
        {
            var result = _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDTO
                {
                    Name = e.Name,
                    DepartmentId = e.DepartmentId
                })
                .ToList();



            ////Inner join
            //         _context.Employees
            //.Join(_context.Departments,
            //    e => e.DepartmentId,
            //    d => d.Id,
            //    (e, d) => new
            //    {
            //        e.Name,
            //        d.Name
            //    })
            //.ToList();

            ////GroupBy
            //_context.Employees
            //    .GroupBy(e => e.Department.Name)
            //    .Select(e=> new
            //    {
            //        Department = e.
            //    })

            return result;
        }
    }
}
