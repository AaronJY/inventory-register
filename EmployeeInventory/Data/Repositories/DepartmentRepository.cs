using ES.InventoryRegister.Entities;
using ES.InventoryRegister.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
    {
        private InventoryDbContext _context;

        public DepartmentRepository(InventoryDbContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a department with a given name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public Department Get(string name)
        {
            return _context.Set<Department>().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Creates a new department in the database
        /// </summary>
        /// <param name="departmentName">Department name</param>
        public void CreateDepartment(string departmentName)
        {
            Department department = new Department();
            department.CreationDate = DateTime.Now;
            department.Name = departmentName;

            _context.Set<Department>().Add(department);
            _context.SaveChanges();
        }

        /// <summary>
        /// Checks to see if a department exists in the database
        /// </summary>
        /// <param name="departmentName">Department name</param>
        /// <returns>Result</returns>
        public bool DepartmentExists(string departmentName)
        {
            return _context.Set<Department>().Any(x => x.Name == departmentName);
        }
    }
}
