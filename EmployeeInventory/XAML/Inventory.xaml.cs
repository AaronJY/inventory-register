using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            filterBox.buttonApply.Click += FilterBoxButtonApply_Click;
            buttonFilter.Click += ButtonFilter_Click;
            buttonRefresh.Click += ButtonRefresh_Click;
            #endregion

            // Populate inventory list with devices from DB
            PopulateDeviceList();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            PopulateDeviceList();
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            if (filterBox.Visibility == Visibility.Visible)
                CancelFilter();
            else OpenFilter();
        }

        private void FilterBoxButtonApply_Click(object sender, RoutedEventArgs e)
        {
            string make = filterBox.textBoxMake.Text;
            string model = filterBox.textBoxModel.Text;
            string serialNum = filterBox.textBoxSerialNumber.Text;
            string name = filterBox.textBoxName.Text;
            int assetNumber = filterBox.numberBoxAssetNumber.Number;

            string ownerName = "";
            if (filterBox.comboBoxOwner.SelectedValue != null)
                ownerName = ((EmployeeViewModel)filterBox.comboBoxOwner.SelectedValue).Name;

            bool searchPurchaseDate = filterBox.datePickerPurchaseDate.SelectedDate.HasValue;
            string purchaseDateKeyword = filterBox.comboBoxPurchaseDateKeyword.Text;

            bool searchExpiryDate = filterBox.datePickerExpiryDate.SelectedDate.HasValue;
            string expiryDateKeyword = filterBox.comboBoxExpiryDateKeyword.Text;

            bool typeLaptop = filterBox.checkBoxLaptop.IsChecked ?? false;
            bool typeDesktop = filterBox.checkBoxDesktop.IsChecked ?? false;
            bool typeMonitor = filterBox.checkBoxMonitor.IsChecked ?? false;
            bool typeTablet = filterBox.checkBoxTablet.IsChecked ?? false;
            bool typePhone = filterBox.checkBoxPhone.IsChecked ?? false;

            var items = listViewInventory.Items;

            foreach (var item in _items)
            {
                item.Hidden = false;

                if (make != "" && !item.Make.Contains(make))
                    item.Hidden = true;
                if (model != "" && !item.Model.Contains(model))
                    item.Hidden = true;
                if (serialNum != "" && !item.SerialNumber.Contains(serialNum))
                    item.Hidden = true;
                if (name != "" && !item.Name.Contains(name))
                    item.Hidden = true;
                if (ownerName != "" && item.OwnerName != ownerName)
                    item.Hidden = true;
                if (filterBox.numberBoxAssetNumber.Text != "" && item.AssetNumber != assetNumber)
                    item.Hidden = true;

                if (!typeLaptop && item.Type == typeof(Laptop) ||
                    !typeDesktop && item.Type == typeof(Desktop) ||
                    !typeMonitor && item.Type == typeof(Monitor) ||
                    !typeTablet && item.Type == typeof(Entities.Tablet) ||
                    !typePhone && item.Type == typeof(Phone))
                {
                    item.Hidden = true;
                }

                if (item.Type.IsSubclassOf(typeof(Computer)))
                {
                    string processor;
                    using (var manager = new BusinessManager())
                    {
                        manager.DeviceBusiness.GetComputerAsViewModel(item.Id);
                    }
                } 

            }

            listViewInventory.ItemsSource = _items.Where(x => x.Hidden == false).ToList();
            listViewInventory.Items.Refresh();
        }

        private void ButtonCancelFilter_Click(object sender, RoutedEventArgs e)
        {
            CancelFilter();
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

        private void CancelFilter()
        {
            listViewInventory.ItemsSource = _items;

            filterBox.Visibility = Visibility.Collapsed;
            buttonFilterLabel.Text = "Filter";
        }

        private void OpenFilter()
        {
            filterBox.Visibility = Visibility.Visible;
            buttonFilterLabel.Text = "Close Filter";
        }
    }
}
