using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Data.Repositories;
using ES.InventoryRegister.Entities;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private InventoryDbContext _context;
        
        private IRepository<Laptop> _laptops;
        private EmployeeRepository _employees;
        private IRepository<Desktop> _desktops;
        private IRepository<Monitor> _monitors;
        private IRepository<Phone> _phones;
        private IRepository<ProductKey> _productKeys;
        private DepartmentRepository _departments;
        private IRepository<Tablet> _tablets;
        private DeviceRepository _devices;

        public UnitOfWork()
        {
            _context = new InventoryDbContext();
        }

        public DeviceRepository Devices
        {
            get
            {
                if (_devices == null)
                    _devices = new DeviceRepository(_context);

                return _devices;
            }
        }

        public IRepository<Laptop> Laptops
        {
            get
            {
                if (_laptops == null)
                    _laptops = new RepositoryBase<Laptop>(_context);

                return _laptops;
            }
        }

        public EmployeeRepository Employees
        {
            get
            {
                if (_employees == null)
                    _employees = new EmployeeRepository(_context);

                return _employees;
            }
        }

        public IRepository<Desktop> Desktops
        {
            get
            {
                if (_desktops == null)
                    _desktops = new RepositoryBase<Desktop>(_context);

                return _desktops;
            }
        }

        public IRepository<Tablet> Tablets
        {
            get
            {
                if (_tablets == null)
                    _tablets = new RepositoryBase<Tablet>(_context);

                return _tablets;
            }
        }

        public IRepository<ProductKey> ProductKeys
        {
            get
            {
                if (_productKeys == null)
                    _productKeys = new RepositoryBase<ProductKey>(_context);

                return _productKeys;
            }
        }

        public DepartmentRepository Departments
        {
            get
            {
                if (_departments == null)
                    _departments = new DepartmentRepository(_context);

                return _departments;
            }
        }

        public IRepository<Phone> Phones
        {
            get
            {
                if (_phones == null)
                    _phones = new RepositoryBase<Phone>(_context);

                return _phones;
            }
        }

        public IRepository<Monitor> Monitors
        {
            get
            {
                if (_monitors == null)
                    _monitors = new RepositoryBase<Monitor>(_context);

                return _monitors;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        #endregion
    }
}
