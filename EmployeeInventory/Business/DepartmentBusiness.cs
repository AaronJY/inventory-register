﻿using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return UnitOfWork.Departments.All().ToList();
        }

        /// <summary>
        /// Asks the DepartmentRepisitory if the department already exists
        /// in the database
        /// </summary>
        /// <param name="department">Department</param>
        /// <returns></returns>
        public bool DepartmentExists(string departmentName)
        {
            return UnitOfWork.Departments.DepartmentExists(departmentName);
        }

        /// <summary>
        /// Tells the DepartmentRepisitory to create a new department
        /// </summary>
        /// <param name="department">Department</param>
        public void CreateDepartment(string departmentName)
        {
            UnitOfWork.Departments.CreateDepartment(departmentName);
        }
    }
}
