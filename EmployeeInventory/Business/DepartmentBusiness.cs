using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ES.InventoryRegister.Business
{
    /// <summary>
    /// Used to perform department-based logic
    /// </summary>
    public class DepartmentBusiness : BusinessBase
    {
        private BusinessManager _manager { get; set; }

        public DepartmentBusiness(BusinessManager manager)
            : base(manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets a list of departments from the database
        /// </summary>
        /// <returns>Departments</returns>
        public List<Department> GetDepartments()
        {
            return UnitOfWork.Departments.GetDepartments().OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Asks the DepartmentRepisitory if the department already exists
        /// in the database
        /// </summary>
        /// <param name="department">Department name</param>
        /// <returns></returns>
        public bool DepartmentExists(string departmentName)
        {
            return UnitOfWork.Departments.DepartmentExists(departmentName);
        }

        /// <summary>
        /// Tells the DepartmentRepisitory to create a new department
        /// </summary>
        /// <param name="department">Department name</param>
        public void CreateDepartment(string departmentName)
        {
            UnitOfWork.Departments.CreateDepartment(departmentName);
        }

        /// <summary>
        /// Tells the DepartmentRepository to remove an existing department
        /// </summary>
        /// <param name="selectedDepartmentName">Department name</param>
        public void RemoveDepartment(string departmentName)
        {
            UnitOfWork.Departments.RemoveDepartment(departmentName);
        }

        /// <summary>
        /// Asks the DepartmentRepository if the department is in use
        /// by other entities
        /// </summary>
        /// <param name="selectedDepartmentName">Department name</param>
        /// <returns>Result</returns>
        public bool IsDepartmentInUse(string departmentName)
        {
            return UnitOfWork.Departments.IsDepartmentIsUse(departmentName);
        }

        public List<DepartmentViewModel> GetDepartmentsAsViewModels()
        {
            List<DepartmentViewModel> departmentModels;
            using (BusinessManager manager = new BusinessManager())
            {
                // Fetch a list of departments from the database
                List<Department> departments = GetDepartments();

                // Map the departments to their respective view models
                departmentModels = Mapper.Map<List<Department>, List<DepartmentViewModel>>(departments);

                // Sort the view models
                departmentModels = departmentModels.OrderBy(x => x.Name).ToList();
            }

            return departmentModels;
        }

        /// <summary>
        /// Asks the DepartmentRepository for a department with
        /// a specific name
        /// </summary>
        /// <param name="departmentName">Department name</param>
        /// <returns>Department</returns>
        public Department GetDepartment(string departmentName)
        {
            return UnitOfWork.Departments.GetDepartment(departmentName);
        }
    }
}
