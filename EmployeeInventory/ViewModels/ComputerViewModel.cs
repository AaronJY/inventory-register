using ES.InventoryRegister.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.ViewModels
{
    class ComputerViewModel
    {
        public string Processor { get; set; }

        public int? Memory { get; set; }

        public int? DiskSpace { get; set; }

        public DiskType? DiskType { get; set; }

        public string OperatingSystem { get; set; }
    }
}
