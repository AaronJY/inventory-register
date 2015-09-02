using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Business
{
    public class BusinessManager : IDisposable
    {
        private EmployeeBusiness _employeeBusiness { get; set; }
        private DepartmentBusiness _departmentBusiness { get; set; }
        private DeviceBusiness _deviceBusiness { get; set; }
        
        internal IUnitOfWork UnitOfWork { get; private set; }

        public BusinessManager()
        {
            // Create a new instance of the UnitOfWork
            UnitOfWork = new UnitOfWork();
        }

        public EmployeeBusiness EmployeeBusiness
        {
            get
            {
                // Create a new instance if it hasn't been accessed yet
                if (_employeeBusiness == null)
                    _employeeBusiness = new EmployeeBusiness(this);

                return _employeeBusiness;
            }
        }

        public DepartmentBusiness DepartmentBusiness
        {
            get
            {
                if (_departmentBusiness == null)
                    _departmentBusiness = new DepartmentBusiness(this);

                return _departmentBusiness;
            }
        }

        public DeviceBusiness DeviceBusiness
        {
            get
            {
                if (_deviceBusiness == null)
                    _deviceBusiness = new DeviceBusiness(this);

                return _deviceBusiness;
            }
        }

        /// <summary>
        /// Gets an entity type from a passed in string
        /// </summary>
        /// <param name="str">Type string</param>
        /// <returns>Entity type</returns>
        public Type GetEntityTypeFromStr(string str)
        {
            switch (str)
            {
                case "Computer": return typeof(Computer);
                case "Tablet": return typeof(Entities.Tablet);
                case "Laptop": return typeof(Laptop);
                case "Monitor": return typeof(Monitor);
                case "Phone": return typeof(Phone);
                case "Desktop": return typeof(Desktop);
                default: return null;
            }
        }

        #region Handle disposing
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (UnitOfWork != null)
                {
                    // Save changes made to UnitOfWork
                    UnitOfWork.SaveChanges();

                    // Dispose
                    UnitOfWork.Dispose();
                }
            }
        }
        #endregion
        
    }
}
