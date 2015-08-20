using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public class InventoryDbInitializer : MigrateDatabaseToLatestVersion<InventoryDbContext, InventoryDbConfiguration>
    {
        public InventoryDbInitializer()
        {

        }
    }
}
