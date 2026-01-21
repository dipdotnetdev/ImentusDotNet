using CoreWebAPIPract.DTO_s;
using CoreWebAPIPract.IdentityBasedAuth;
using CoreWebAPIPract.Models;
using CoreWebAPIPract.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIPract.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController : ControllerBase
    {
        public readonly ICompanyRepository _repo;
        private readonly ApplicationDbContext _context;
        public LinqController(ICompanyRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        [HttpPost("employee")]
        public IActionResult CreateEmployee(EmployeeDTO employees)
        {
            _repo.CreateEmployee(employees);

            return Ok();
        }

        [HttpPost("department")]
        public IActionResult CreateDepartment(DepartmentDTO department)
        {
            _repo.CreateDepartment(department);

            return Ok();
        }

        [HttpPost("project")]
        public IActionResult CreateProject(ProjectDTO project)
        {
            _repo.CreateProject(project);

            return Ok();
        }

        [HttpPost("employeeProject")]
        public IActionResult CreateEmployeeProject(EmployeeProjectDTO employeeProject)
        {
            _repo.CreateEmployeeProject(employeeProject);

            return Ok();
        }

        [HttpPost("get-employee")]
        public Employees GetEmployeeBySproc(int id)
        {
            return _context.Employees.FromSqlRaw("EXEC Employee_Get @EmployeeId", new SqlParameter("@EmployeeId", id))
            .AsEnumerable()
            .FirstOrDefault();
        }

        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            var result = _context.Employees.Where(e => e.Id == 2).Select(e => new
            {
                e.Name,
                e.DepartmentId
            });

            //OrderBy
            _context.Employees.OrderBy(e => e.Salary).ToList();
            _context.Employees.OrderByDescending(e => e.Salary).ToList();

            //Paging
            _context.Employees.Skip(10).Take(10).ToList();

            //Inner Join
            var res = from e in _context.Employees
            join d in _context.Departments
            on e.DepartmentId equals d.Id
            select new
            {
                e.Name,
                d.Location
            };

            res = _context.Employees
                .Join(_context.Departments,
                e => e.DepartmentId,
                d => d.Id,
                (e, d) => new
                {
                    e.Name,
                    d.Location
                });

            //Left Join
            res = from e in _context.Employees
                  join d in _context.Departments
                  on e.DepartmentId equals d.Id into g
                  from d in g.DefaultIfEmpty()
                  select new { d.Name, d.Location };

            var left = _context.Employees
                .GroupJoin(_context.Departments,
                e=>e.DepartmentId,
                d=>d.Id,
                (e, d) => new
                {
                    e,d
                }
                )
                .SelectMany(
                x=>x.d.DefaultIfEmpty(),
                (x, d) => new
                {
                    x.e.Name,
                    d.Location
                }
                );

            //Right join
            res = from d in _context.Departments
                  join e in _context.Employees
                  on d.Id equals e.DepartmentId into g
                  from e in g.DefaultIfEmpty()
                  select new {d.Name, d.Location};

            var right = _context.Departments
                .GroupJoin(_context.Employees,
                d => d.Id,
                e => e.DepartmentId,
                (d, e) => new
                {
                    d,
                    e
                })
                .SelectMany(
                x => x.e.DefaultIfEmpty(),
                (x, e) => new
                {

                    e.Name,
                    x.d.Location,
                });

            //Full Outer Join
            res = left.Union(right);

            //Cross Join
            var cross = _context.Employees
                .SelectMany(e => _context.Departments,
                (e, d) => new
                {
                    e.Name,
                    d.Location
                });

            //Join with Where Condition
            var mj = _context.Employees
                .Join(_context.Departments,
                e => e.DepartmentId,
                d => d.Id,
                (e, d) => new
                {
                    e,
                    d
                })
                .Where(x => x.d.Location == "Indore")
                .Select(x => new
                {
                    x.e.Name,
                    x.d.Location,
                });

            //

            return Ok(result);
        }

        [HttpGet("employees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _context.Employees
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Salary = e.Salary,
                    DepartmentId = e.DepartmentId,
                    JoinDate = e.JoinDate
                })
                .ToList();

            return Ok(employees);
        }
    }
}
