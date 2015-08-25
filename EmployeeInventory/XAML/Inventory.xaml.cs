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
            #endregion

            // Populate inventory list with devices from DB
            GetDevices();
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
            // Create a new instance of the CreateDevice window and pass the Inventory object in
            // so it can be referenced later
            CreateDevice createDeviceWindow = new CreateDevice(this);
            // Show the window
            createDeviceWindow.Show();
        }

        /// <summary>
        /// Opens the window to manage employees
        /// </summary>
        private void OpenManageEmployeesWindow()
        {
            // Create a new instance of the ManageEmployee window
            ManageEmployees mangeEmployeesWindow = new ManageEmployees();
            // Show the window
            mangeEmployeesWindow.ShowDialog();
        }

        void buttonAddDevice_Click(object sender, RoutedEventArgs e)
        {
            OpenCreateDeviceWindow();
        }

        void buttonDepartments_Click(object sender, RoutedEventArgs e)
        {
        }

        void buttonEmployees_Click(object sender, RoutedEventArgs e)
        {
            OpenManageEmployeesWindow();
        }

        /// <summary>
        /// Populates the inventory list with devices from the database
        /// </summary>
        public void GetDevices()
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
