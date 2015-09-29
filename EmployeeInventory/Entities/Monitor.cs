using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES.InventoryRegister.Entities
{
    public class Monitor : Device
    {
        public DisplayInterfaces DisplayInterfaces { get; set; }

        public int? ScreenSize { get; set; }
    }

    /// <summary>
    /// Used by Montior entity to describe which interfaces
    /// from the list are currently being used
    /// </summary>
    public class DisplayInterfaces
    {
        public bool VGA { get; set; }

        public bool DVI { get; set; }

        public bool DisplayPort { get; set; }

        public bool HDMI { get; set; }
    }
}
