namespace MyFirstWebAPI.Models
{
    public class repositoryEmployee
    {
        private NorthwindContext _context;
        public repositoryEmployee(NorthwindContext context)
        {
            _context = context;
        }
        public List<Employee> AllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee FindEmpoyeeById(int id)
        {
            Employee employeeId = _context.Employees.Find(id);
            return employeeId;
        }
        public int AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            return _context.SaveChanges();
        }
        public int ModifyEmployee(int id)
        {
            Employee emp = _context.Employees.Find(id);
            _context.Employees.Update(emp);
            return _context.SaveChanges();
        }
        public int DeleteEmployee(int id)
        {
            Employee emp = _context.Employees.Find(id);
            _context.Employees.Remove(emp);
            return _context.SaveChanges();
        }
    }
}
