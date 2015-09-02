using ES.InventoryRegister.Entities;
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
    }
}
