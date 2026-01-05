using CoreWebAPIPract.DTO_s;
using CoreWebAPIPract.IdentityBasedAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIPract.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadingController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public LoadingController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetDepartment_Lazy()
        {
            var departments = _dbContext.Departments.ToList();

            var result = new List<DepartmentDTO>();

           foreach(var department in departments)
            {
                var employees = department.Employees;

                result.Add(new DepartmentDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Employees = employees.Select(e => new EmployeeDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                    }).ToList()
                });
            }

           return Ok(result);
        }


        [HttpGet]
        public IActionResult GetDepartment_Eager()
        {
            var departments = _dbContext.Departments
                .Include(e => e.Employees).ToList();

            var result = departments.Select(dept => new DepartmentDTO
            {
                Id = dept.Id,
                Name = dept.Name,
                Employees = dept.Employees.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList(),
            });

            ////Best Practice
            //var result = _dbContext.Departments.Select(dept => new DepartmentDTO
            //{
            //    Id =dept.Id,
            //    Name = dept.Name,
            //    Employees = dept.Employees.Select(e => new EmployeeDTO
            //    {
            //        Id = e.Id,
            //        Name= e.Name,

            //    }).ToList()
            //}).ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetDepartment_Explicit(int id, bool includeEmployees = false)
        {
            var departments = _dbContext.Departments.Find(id);

            if (includeEmployees)
            {
                _dbContext.Entry(departments)
                    .Collection(e => e.Employees)
                    .Load();
            }

            return Ok(departments);
        }
        
        
    }
}
