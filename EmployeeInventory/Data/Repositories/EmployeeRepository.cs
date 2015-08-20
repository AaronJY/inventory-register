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

        public void AddEmployee(Employee employee)
        {
            employee.CreationDate = DateTime.Now;
            _context.Set<Employee>().Add(employee);
        }

        public void RemoveEmployee(int employeeId)
        {
            List<Device> devices = _context.Set<Device>().ToList();
            foreach (Device device in devices)
            {
                Console.WriteLine(device);
            }

            base.Delete(employeeId);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Set<Employee>().Single(x => x.Id == employeeId);
        }
    }
}
