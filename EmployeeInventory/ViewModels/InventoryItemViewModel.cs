using ES.InventoryRegister.Entities;
using System;

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

        public string Notes { get; set; }

        public int AssetNumber { get; set; }

        public Device.DeviceStatus Status { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
