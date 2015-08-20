using ES.InventoryRegister.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Business
{
    public abstract class BusinessBase
    {
        protected BusinessManager BusinessManager { get; set; }

        public BusinessBase()
        {
        }

        protected BusinessBase(BusinessManager businessManager)
        {
            BusinessManager = businessManager;
        }

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return BusinessManager.UnitOfWork;
            }
        }
    }
}
