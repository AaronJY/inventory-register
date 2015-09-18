using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
using AutoMapper;

namespace ES.InventoryRegister.Business
{
    /// <summary>
    /// Used to perform employee-based logic
    /// </summary>
    public class EmployeeBusiness : BusinessBase
    {
        private BusinessManager _businessManager;

        public EmployeeBusiness(BusinessManager businessManager)
            : base(businessManager)
        {
            _businessManager = businessManager;
        }

        /// <summary>
        /// Tells the EmployeeRepository to add a new employee
        /// to the database
        /// </summary>
        /// <param name="employee">Employee</param>
        public void AddEmployee(Employee employee)
        {
            UnitOfWork.Employees.Add(employee);
        }

        /// <summary>
        /// Gets a list of all employees from the database
        /// </summary>
        /// <returns>Employees</returns>
        public List<Employee> GetEmployees()
        {
            return UnitOfWork.Employees.All().OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Tells the EmployeeRepository to remove an
        /// employee from the database
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        public void RemoveEmployee(int employeeId)
        {
            UnitOfWork.Employees.Delete(employeeId);
        }

        /// <summary>
        /// Tells the EmployeeRepository to pass back
        /// an employee from the database with the
        /// given ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            return UnitOfWork.Employees.GetEmployee(employeeId);
        }

        /// <summary>
        /// Checks if an employee currently exists in the
        /// database with a given name
        /// </summary>
        /// <param name="employeeName">Employee's name</param>
        /// <returns>Result</returns>
        public bool IsEmployeeNameUsed(string employeeName)
        {
            // Get employees from the database
            List<Employee> employees;
            using (BusinessManager manager = new BusinessManager())
            {
                employees = manager.EmployeeBusiness.GetEmployees();
            }

            foreach (Employee employee in employees)
            {
                if (employee.Name == employeeName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Tells the EmployeeRepository to get a list of
        /// employees from the database which are then
        /// converted into their respective view models
        /// </summary>
        /// <returns>Empployee view models</returns>
        public List<EmployeeViewModel> GetEmployeesAsViewModels()
        {
            List<EmployeeViewModel> employeeModels;
            using (BusinessManager manager = new BusinessManager())
            {
                // Fetch a list of employees in the database
                List<Employee> employees = manager.EmployeeBusiness.GetEmployees();
                // Map the employees to their respective view models
                employeeModels = Mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);

                // Sort the list
                employeeModels.OrderBy(x => x.Name);
            }

            return employeeModels;
        }

        /// <summary>
        /// Asks the EmployeeRepository if the employee with
        /// the given ID is currently being used
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Result</returns>
        public bool IsEmployeeInUse(int employeeId)
        {
            return UnitOfWork.Employees.IsEmployeeInUse(employeeId);
        }
    }
}
