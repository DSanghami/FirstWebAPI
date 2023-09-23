using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Model;
using MyFirstWebAPI.Models;

namespace MyFirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private repositoryEmployee _repositoryEmployee;
        public EmployeeController(repositoryEmployee repository)
        {
            _repositoryEmployee = repository;
        }
        [HttpGet("/GetAllEmployees")]
        public IEnumerable<EmpViewModel> GetAllEmployees()
        {
            List<Employee> employees = _repositoryEmployee.AllEmployees();
            var empList = (
                from emp in employees
                select new EmpViewModel()
                {
                    EmpID = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    BirthDate = emp.BirthDate,
                    HireDate = emp.HireDate,
                    Title = emp.Title,
                    City = emp.City,
                    ReportsTo = emp.ReportsTo
                }).ToList();
            return empList;
        }





        [HttpGet("/FindEmployee")]
        public Employee FindEmployee(int id)
        {
            Employee employeeById = _repositoryEmployee.FindEmpoyeeById(id);
            return employeeById;
        }

        [HttpPost("/AddEmployee")]
        public string AddEmployee(Employee newEmployee)
        {
            int employeestatus = _repositoryEmployee.AddEmployee(newEmployee);
            if (employeestatus == 0)
            {
                return "Employee Not Added To Database Since it already exist";
            }
            else
            {
                return "Employee Added To Database";
            }
        }
        [HttpPost]
        public int Post(EmpViewModel emp)
        {
            Employee employee = new Employee();
            {
                employee.FirstName = emp.FirstName;
                employee.LastName = emp.LastName;
                employee.BirthDate = emp.BirthDate;
                employee.HireDate = emp.HireDate;
                employee.Title = emp.Title;
                employee.City = emp.City;
                employee.ReportsTo = emp.ReportsTo > 0 ? emp.ReportsTo : null;
                // employee.EmployeeId



            };
            _repositoryEmployee.AddEmployee(employee);
            return 1;



        }


        [HttpPut("/EditEmployee")]
        public int EditEmployee(int id, [FromBody] EmpViewModel emp)
        {
            Employee employee = new Employee();
            employee.EmployeeId = emp.EmpID;
            employee.FirstName = emp.FirstName;
            employee.LastName = emp.LastName;
            _repositoryEmployee.UpdateEmployee(employee);
            return 1;
        }



        [HttpDelete("/DeleteEmployee")]
        public string DeleteEmployee(int id)
        {
            int employeestatus = _repositoryEmployee.DeleteEmployee(id);
            if (employeestatus == 0)
            {
                return "Employee does not exist in the Database";
            }
            else
            {
                return "Employee Successfully Deleted";
            }
        }

       
    }
}
