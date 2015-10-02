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
using System.Diagnostics;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        private List<InventoryItemViewModel> _items;

        public Inventory()
        {
            InitializeComponent();

            #region Event listeners
            buttonEmployees.Click += buttonEmployees_Click;
            buttonDepartments.Click += buttonDepartments_Click;
            buttonAddDevice.Click += buttonAddDevice_Click;
            listViewInventory.MouseDoubleClick += listViewInventory_MouseDoubleClick;
            this.Closed += Inventory_Closed;
            buttonExportXML.Click += buttonExportXML_Click;
            textBoxSearch.PreviewKeyDown += TextBoxSearch_PreviewKeyDown;
            buttonCancelFilter.Click += ButtonCancelFilter_Click;
            #endregion

            // Populate inventory list with devices from DB
            PopulateDeviceList();
        }

        private void ButtonCancelFilter_Click(object sender, RoutedEventArgs e)
        {
            CancelFilter();
        }

        private void TextBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            ApplyFilter(textBoxSearch.Text);
        }

        void buttonExportXML_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "XML File (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FilterIndex = 0;

            // Check if the file browser dialog has been OK'd
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Tell the business manager to run the export method
                using (BusinessManager manager = new BusinessManager())
                {
                    manager.ExportAsXml(saveFileDialog.FileName);
                }

                var msgBoxResult = MessageBox.Show(
                    String.Format("Successfully exported to '{0}'. Do you you wish to view the exported file?", saveFileDialog.FileName),
                    "Success",
                    MessageBoxButton.YesNo);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    Process.Start(saveFileDialog.FileName);
                }
            }
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
                _items = Mapper.Map<List<Device>, List<InventoryItemViewModel>>(devices);
            }

            listViewInventory.ItemsSource = _items;
        }

        /// <summary>
        /// Opens the window to manage departments
        /// </summary>
        private void OpenManageDepartmentsWindow()
        {
            ManageDepartments manageDeparmentsWindow = new ManageDepartments();
            manageDeparmentsWindow.ShowDialog();
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
        /// REMOVED BUTTON AS NO LONGER USED
        /// </summary>
        private void OpenConnectionSettingsWindow()
        {
            ManageConnections manageConnectionsWindow = new ManageConnections();
            manageConnectionsWindow.ShowDialog();
        }

        /// <summary>
        /// Filters devices based on passed in text
        /// </summary>
        /// <param name="filterText">Filter text</param>
        private void ApplyFilter(string filterText)
        {
            var newText = filterText.ToLower();
            var items = listViewInventory.Items;

            if (newText == "")
            {
                CancelFilter();
                return;
            }

            stackPanelFilter.Visibility = Visibility.Visible;

            foreach (var viewModel in _items)
            {
                if (viewModel.Name.ToLower().Contains(newText) ||
                    viewModel.OwnerName.ToLower().Contains(newText) ||
                    viewModel.SerialNumber.ToLower().Contains(newText) ||
                    viewModel.Make.ToLower().Contains(newText) ||
                    viewModel.Model.ToLower().Contains(newText))
                {
                    viewModel.Hidden = false;
                }
                else
                {
                    viewModel.Hidden = true;
                }
            }

            listViewInventory.ItemsSource = _items.Where(x => x.Hidden == false).ToList();
            listViewInventory.Items.Refresh();
        }

        private void CancelFilter()
        {
            textBoxSearch.Text = "";
            listViewInventory.ItemsSource = _items;
            stackPanelFilter.Visibility = Visibility.Collapsed;
        }
    }
}
