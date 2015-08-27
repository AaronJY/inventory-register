using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ES.InventoryRegister.XAML;

namespace ES.InventoryRegister.Data.Infrastructure
{
    public static class DbManager
    {
        public static string GetConnectionString()
        {
            try
            {
                string connectionString = System.IO.File.ReadAllText("ConnectionString.txt").Trim();
                return connectionString;
            }
            catch (Exception ex)
            {
                ErrorHandler.Show(ex, "There was an issue reading the connection string from 'ConnectionString.txt'. Please make sure the file exists and is accessible.");
                return null;
            }
        }
    }
}
