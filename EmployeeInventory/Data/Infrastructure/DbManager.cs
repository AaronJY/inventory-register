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
        /// <summary>
        /// Gets the connection string saved in ConnectionString.txt
        /// </summary>
        /// <returns>Connection string</returns>
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

        /// <summary>
        /// Saves the passed-in connection string to
        /// ConenectionString.txt
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public static void SaveConnectionString(string connectionString)
        {
            try
            {
                System.IO.File.WriteAllText("ConnectionString.txt", connectionString);
            }
            catch (Exception ex)
            {
                ErrorHandler.Show(ex, "There was an issue saving the connection string to 'ConnectionString.txt'. Please make sure the file is accessible.");
            }
        }
    }
}
