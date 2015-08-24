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
    public class EmployeeBusiness : BusinessBase
    {
        private BusinessManager _businessManager;

        public EmployeeBusiness(BusinessManager businessManager)
            : base(businessManager)
        {
            _businessManager = businessManager;
        }

        public void AddEmployee(Employee employee)
        {
            UnitOfWork.Employees.Add(employee);
        }

        public List<Employee> GetEmployees()
        {
            return UnitOfWork.Employees.All().ToList();
        }

        public void RemoveEmployee(int employeeId)
        {
            UnitOfWork.Employees.Delete(employeeId);
        }

        public Employee GetEmployee(int employeeId)
        {
            return UnitOfWork.Employees.GetEmployee(employeeId);
        }

        public bool IsEmployeeNameUsed(string employeeName)
        {
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

        public List<EmployeeViewModel> GetEmployeesAsViewModels()
        {
            List<EmployeeViewModel> employeeModels;
            using (BusinessManager manager = new BusinessManager())
            {
                // Fetch a list of employees in the database
                List<Employee> employees = manager.EmployeeBusiness.GetEmployees();
                // Map the employees to their respective view models
                employeeModels = Mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);
            }

            return employeeModels;
        }
    }
}
