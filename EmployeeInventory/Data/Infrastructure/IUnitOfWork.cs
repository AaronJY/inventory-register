using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Data.Repositories;
using ES.InventoryRegister.Entities;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Desktop> Desktops { get;  }
        IRepository<Monitor> Monitors { get; }
        IRepository<Phone> Phones { get; }
        IRepository<Tablet> Tablets { get; }
        IRepository<ProductKey> ProductKeys { get; }
        IRepository<Laptop> Laptops { get; }
        DepartmentRepository Departments { get; }
        EmployeeRepository Employees { get; }
        DeviceRepository Devices { get; }

        void SaveChanges();
    }
}
