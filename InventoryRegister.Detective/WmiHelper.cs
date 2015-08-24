using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace InventoryRegister.Detective
{
    public static class WmiHelper
    {
        public static string RequestWmi(string query)
        {
            string propertyName = query.Split(' ')[1];
            WqlObjectQuery wqlQuery = new WqlObjectQuery(query);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wqlQuery);

            string val = "";
            foreach (ManagementObject entry in searcher.Get())
            {
                val = entry.Properties[propertyName].Value.ToString();
            }

            return val;
        }
    }
}
