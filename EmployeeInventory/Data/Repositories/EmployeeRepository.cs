using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        private InventoryDbContext _context;

        public EmployeeRepository(InventoryDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new employee to the database
        /// </summary>
        /// <param name="employee">Employee object</param>
        public void AddEmployee(Employee employee)
        {
            employee.CreationDate = DateTime.Now;
            _context.Set<Employee>().Add(employee);
        }

        /// <summary>
        /// Removes an employee from the database with a specific ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        public void RemoveEmployee(int employeeId)
        {
            Employee employee = GetEmployee(employeeId);
            employee.Deleted = true;

            _context.SaveChanges();
        }

        /// <summary>
        /// Get an employee from the database with a specific ID
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Employee</returns>
        public Employee GetEmployee(int employeeId)
        {
            return _context.Set<Employee>().Single(x => x.Id == employeeId && !x.Deleted);
        }

        /// <summary>
        /// Gets a list of all employees that are currently
        /// using a given department
        /// </summary>
        /// <param name="departmentName">Department name</param>
        /// <returns>List of employees</returns>
        public List<Employee> GetEmployeesUsingDepartment(string departmentName)
        {
            return _context.Set<Employee>().Where(x => x.Department.Name == departmentName && !x.Deleted).ToList();
        }

        /// <summary>
        /// Checks if a device is currently using an employee
        /// as its owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result</returns>
        public bool IsEmployeeInUse(int employeeId)
        {
            return _context.Set<Device>().Any(x => x.Owner.Id == employeeId && !x.Owner.Deleted);
        }
    }
}
