using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.ViewModels
{
    public class InventoryItemViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string OwnerName { get; set; }

        public string PurchaseDate { get; set; }

        public string ExpiryDate { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public Type Type { get; set; }

        public string TypeName { get; set; }

        public bool Hidden { get; set; }
    }
}
