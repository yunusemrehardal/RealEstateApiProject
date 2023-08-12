using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult EmployeeList()
        {
            var values = _employeeService.TGetList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeService.TInsert(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            var value = _employeeService.TGetByID(id);
            _employeeService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(string id)
        {
            var value = _employeeService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _employeeService.TUpdate(employee);
            return Ok();
        }
    }
}
