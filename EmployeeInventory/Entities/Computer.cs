using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Entities
{
    // Only allow setting to SSD or HDD disk types
    public enum DiskType
    {
        SSD, HDD
    }

    public abstract class Computer : Device
    {
        public string Processor { get; set; }

        public int Memory { get; set; }

        public int DiskSpace { get; set; }

        public DiskType? DiskType { get; set; }

        public string OperatingSystem { get; set; }

        public virtual List<ProductKey> ProductKeys { get; set; }
    }
}
