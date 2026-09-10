using Microsoft.AspNetCore.Mvc; // this give use WEBAPI features // MVC = is a tool for creating API controller
using Microsoft.EntityFrameworkCore; // talking to the database

namespace WebApiProject.Controllers
{
    [ApiController] // it say's this class is an API controller
    [Route("api/[controller]")] // Controller will receive the request and decides what to do // our class name is "EmployeeController" so "controller" become "Employee", so the address is "api/Employee"
    public class EmployeesController : ControllerBase // this "EmployeeController" class handles API request
    {
        private readonly AppDbContext _context; // Whenever a controller needs "AppDbContext", give it an "AppDbContext" is called Dependency injection // this controller needs "AppDbContext" and "_context" is your way of accessing the database directly through the EF core
        public EmployeesController(AppDbContext context) //constructor injection
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet] // this say's --> When someone sends a GET request, run the method below
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList(); // returning all the employee
        }

        // GET: api/Employees/1 --> putting their id in the URL
        [HttpGet("{id}")] 
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id); // find the employee whose id is "1001"

            if (employee == null)
            { 
                return NotFound(); 
            }
            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee) //why "Action Result" --> so, The method can return an Employee or an HTTP response
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        // PUT: api/Employees/1
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            { 
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified; // "_context.Entry(employee)" this say's --> look at this employee the employee and ".State" = that employees state has got modified

            _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Employees/1
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) 
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}