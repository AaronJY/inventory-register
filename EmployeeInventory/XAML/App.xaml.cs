using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using ES.InventoryRegister.Data.Infrastructure;
using System.Windows.Threading;
using ES.InventoryRegister.XAML;

namespace ES.InventoryRegister
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        DatabaseConnection connectionWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            AutoMapperConfiguration.Configure();

            this.Startup += App_Startup;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ShowDbConnectingWindow();
            CreateInitialDbContext();

            base.OnStartup(e);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            connectionWindow.Hide();
        }

        /// <summary>
        /// Shows the connection status window
        /// </summary>
        private void ShowDbConnectingWindow()
        {
            connectionWindow = new DatabaseConnection();
            connectionWindow.Show();
        }

        /// <summary>
        /// Creates the initial connection to the database.
        /// </summary>
        private void CreateInitialDbContext()
        {
            try
            {
                InventoryDbContext context = new InventoryDbContext();
            }
            catch (Exception ex)
            {
                ErrorHandler.Show(ex, "Couldn't create a connection to the database!");
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHandler.Show((Exception)e.ExceptionObject, "An unhandled exception has occured! The details have been logged.");
        }
    }
}
