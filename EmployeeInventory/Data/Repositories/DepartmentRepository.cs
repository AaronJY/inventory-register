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
        /// <returns></returns>
        public Department Get(string name)
        {
            return _context.Set<Department>().FirstOrDefault(x => x.Name == name);
        }
            
    }
}
