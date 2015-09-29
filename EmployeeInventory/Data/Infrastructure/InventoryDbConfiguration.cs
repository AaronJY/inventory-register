using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ES.InventoryRegister.Entities;

namespace ES.InventoryRegister.Data.Infrastructure
{
    /// <summary>
    /// Used for defining database configuration
    /// </summary>
    public class InventoryDbConfiguration : DbMigrationsConfiguration<InventoryDbContext>
    {
        public InventoryDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Populates database with data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(InventoryDbContext context)
        {
            string[] departments = {
                "IT", 
                "Mitigation",
                "SubsNetUK",
                "Planning",
                "Arboriculture",
                "Ecology",
                "Management",
                "Tree Surgery",
                "Accounts",
                "Health & Safety"
            };

            if (!context.Set<Department>().Any())
            {
                foreach (string department in departments)
                {
                    context.Set<Department>().Add(new Department { Name = department, CreationDate = DateTime.Now });
                }
            }
            
            base.Seed(context);
        }
    }
}
