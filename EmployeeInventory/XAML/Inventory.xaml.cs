using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.Business;
using ES.InventoryRegister.ViewModels;
using AutoMapper;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        public Inventory()
        {
            InitializeComponent();

            #region Event listeners
            buttonEmployees.Click += buttonEmployees_Click;
            buttonDepartments.Click += buttonDepartments_Click;
            buttonAddDevice.Click += buttonAddDevice_Click;
            listViewInventory.MouseDoubleClick += listViewInventory_MouseDoubleClick;
            this.Closed += Inventory_Closed;
            buttonConnection.Click += buttonConnection_Click;
            #endregion

            // Populate inventory list with devices from DB
            PopulateDeviceList();
        }

        void buttonConnection_Click(object sender, RoutedEventArgs e)
        {
            OpenConnectionSettingsWindow();
        }

        void Inventory_Closed(object sender, EventArgs e)
        {
            // Close the application when the inventory window is closed
            Application.Current.Shutdown();
        }

        void listViewInventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            if (listViewInventory.SelectedItems.Count < 1) return;

            InventoryItemViewModel selectedDevice = (InventoryItemViewModel)listViewInventory.SelectedItem;

            ViewDevice viewDeviceWindow = new ViewDevice(this, selectedDevice.Id, selectedDevice.Type);
            viewDeviceWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the window to create a new device
        /// </summary>
        private void OpenCreateDeviceWindow()
        {
            CreateDevice createDeviceWindow = new CreateDevice(this);
            createDeviceWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the window to manage employees
        /// </summary>
        private void OpenManageEmployeesWindow()
        {
            ManageEmployees mangeEmployeesWindow = new ManageEmployees();
            mangeEmployeesWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the connection settings window
        /// </summary>
        private void OpenConnectionSettingsWindow()
        {
            ManageConnections manageConnectionsWindow = new ManageConnections();
            manageConnectionsWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the window to manage departments
        /// </summary>
        private void OpenManageDepartmentsWindow()
        {
            ManageDepartments manageDeparmentsWindow = new ManageDepartments();
            manageDeparmentsWindow.Show();
        }

        void buttonAddDevice_Click(object sender, RoutedEventArgs e)
        {
            OpenCreateDeviceWindow();
        }

        void buttonDepartments_Click(object sender, RoutedEventArgs e)
        {
            OpenManageDepartmentsWindow();
        }

        void buttonEmployees_Click(object sender, RoutedEventArgs e)
        {
            OpenManageEmployeesWindow();
        }

        /// <summary>
        /// Populates the inventory list with devices from the database
        /// </summary>
        public void PopulateDeviceList()
        {
            List<Device> devices;
            List<InventoryItemViewModel> deviceModels;
            using (BusinessManager business = new BusinessManager())
            {
                // Get devices
                devices = business.DeviceBusiness.GetDevices();
                // 'Convert' devices to view models
                deviceModels = Mapper.Map<List<Device>, List<InventoryItemViewModel>>(devices);
            }

            listViewInventory.ItemsSource = deviceModels;
        }
    }
}
