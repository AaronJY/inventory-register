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
            return _context.Set<Department>().FirstOrDefault(x => (x.Name == name && x.Deleted == false));
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
            return _context.Set<Department>().Any(x => (x.Name == departmentName && x.Deleted == false));
        }

        /// <summary>
        /// Marks a department as deleted in the database
        /// </summary>
        /// <param name="departmentName">Department name</param>
        public void RemoveDepartment(string departmentName)
        {
            Department department = _context.Set<Department>().FirstOrDefault(x => (x.Name == departmentName && x.Deleted == false));
            department.Deleted = true;
            department.UpdateDate = DateTime.Now;

            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all departments in the database
        /// </summary>
        /// <returns>List of departments</returns>
        public List<Department> GetDepartments()
        {
            return _context.Set<Department>().Where(x => x.Deleted == false).ToList();
        }
    }
}
