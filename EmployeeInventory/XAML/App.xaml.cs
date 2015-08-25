using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            AutoMapperConfiguration.Configure();
            InventoryDbContext ctx = new InventoryDbContext();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            base.OnStartup(e);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHandler.Show((Exception)e.ExceptionObject, "An unhandled exception has occured! The details have been logged.");
        }
    }
}
