using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.IO;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() : base(Properties.Settings.Default.InventoryDbConnectionString)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Database.SetInitializer(new InventoryDbInitializer());
            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateTables(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Creates database tables if they don't already exist
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        private void CreateTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ProductKey>()
                .Property(m => m.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>()
                .Property(m => m.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Department>()
                .Property(m => m.Id).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
