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
using System.Windows.Shapes;
using ES.InventoryRegister.XAML.UserControls;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.Business;
using ES.InventoryRegister.ViewModels;
using System.Data.Entity.Core.Objects;
using AutoMapper;
using System.Security.Cryptography;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for ViewDevice.xaml
    /// </summary>
    public partial class ViewDevice : Window
    {
        private UserControl _propertyView;
        private UserControl _propertyView2;
        private Inventory _inventoryInstance;
        private Device _device;

        public ViewDevice(Inventory inventory, int deviceId, Type type)
        {
            InitializeComponent();

            #region Event listeners
            buttonSave.Click += buttonSave_Click;
            buttonDelete.Click += buttonDelete_Click;
            #endregion

            _inventoryInstance = inventory;

            // Figure out which user control to display
            if (type.IsSubclassOf(typeof(Computer)))
            {
                _propertyView = new ComputerPropertyView();

                if (type == typeof(Phone))
                {
                    _propertyView2 = new PhonePropertyView();
                }
            }
            else if (type == typeof(Monitor))
            {
                _propertyView = new MonitorPropertyView();
            }

            // Add the views to the grids
            if (_propertyView != null)  gridPropertyHolder.Children.Add(_propertyView);
            if (_propertyView2 != null) gridPropertyHolder2.Children.Add(_propertyView2);

            // Get the device from the database and store it
            using (BusinessManager manager = new BusinessManager())
            {
                _device = manager.DeviceBusiness.GetDevice(deviceId);
            }

            PopulateFields();
            PopulateOwners();
        }

        void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDevice();
        }

        void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            UpdateDevice();
        }

        /// <summary>
        /// Deletes the currently loaded device
        /// </summary>
        private void DeleteDevice()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this device?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (BusinessManager manager = new BusinessManager())
                {
                    manager.DeviceBusiness.DeleteDevice(_device.Id);
                }

                // Show message box to user confirming the delete
                MessageBox.Show("Successfully deleted the device.", "Success");

                // Refresh device list
                _inventoryInstance.GetDevices();

                // Close the current window
                this.Close();
            }
        }

        /// <summary>
        /// Fetches employee list from the database and populates the
        /// "Owner" combo box with the names
        /// </summary>
        private void PopulateOwners()
        {
            // Get all employees in a view model format from
            // the database
            List<EmployeeViewModel> employeeViewModels;
            using (BusinessManager manager = new BusinessManager())
            {
                employeeViewModels = manager.EmployeeBusiness.GetEmployeesAsViewModels();
            }

            // Set the combo box item source to employeeViewModels
            comboBoxOwner.ItemsSource = employeeViewModels;

            // Find the combo box entry for the device owner
            EmployeeViewModel model = ((List<EmployeeViewModel>)comboBoxOwner.ItemsSource).FirstOrDefault(x => x.Id == _device.Owner.Id);
            // Set the comboBox's selected item to the owner's employee view model
            comboBoxOwner.SelectedItem = model;
        }

        /// <summary>
        /// Saves changes made
        /// </summary>
        private void UpdateDevice()
        {
            // Get the type of the stored device
            Type deviceType = ObjectContext.GetObjectType(_device.GetType());

            // Create a new entity depending on the type of the device
            Device device;
            if (deviceType == typeof(Monitor))
                device = new Monitor();
            else if (deviceType == typeof(Desktop))
                device = new Desktop();
            else if (deviceType == typeof(Laptop))
                device = new Laptop();
            else if (deviceType == typeof(Phone))
                device = new Phone();
            else if (deviceType == typeof(Entities.Tablet))
                device = new Entities.Tablet();
            else device = null;

            // Get base values for device
            device.Id = Int32.Parse(textBoxID.Text);
            device.Name = textBoxName.Text;
            device.Make = textBoxMake.Text;
            device.Model = textBoxModel.Text;
            device.SerialNumber = textBoxSerialNumber.Text;
            device.Notes = textBoxNotes.Text;
            device.PurchaseDate = datePickerPurchaseDate.SelectedDate;
            device.ExpiryDate = datePickerExpiryDate.SelectedDate;
            
            // Get ID of currently selected owner
            int id = ((EmployeeViewModel)comboBoxOwner.SelectedItem).Id;
            // Get the owner entity from the database
            using (BusinessManager manager = new BusinessManager())
            {
                device.Owner = manager.EmployeeBusiness.GetEmployee(id);
            }

            // If the entity is a computer...
            if (deviceType.IsSubclassOf(typeof(Computer)))
            {
                ComputerPropertyView view = (ComputerPropertyView)_propertyView;
                Computer computer = (Computer)device;
                computer.Processor = view.textBoxProcessor.Text;
                computer.DiskSpace = Int32.Parse(view.textBoxStorage.Text);
                computer.Memory = Int32.Parse(view.textBoxMemory.Text);
                computer.OperatingSystem = view.textBoxOS.Text;
                computer.DiskType = (DiskType)view.comboBoxDiskTypes.SelectedValue;

                // Turn product key view models into product keys
                computer.ProductKeys = Mapper.Map<List<KeyListViewModel>, List<ProductKey>>((List<KeyListViewModel>)view.listViewKeys.ItemsSource);

                // If the entity is a Computer -> Phone
                if (deviceType == typeof(Phone))
                {
                    PhonePropertyView view2 = (PhonePropertyView)_propertyView2;
                    Phone phone = (Phone)computer;

                    phone.HasCamera = view2.checkBoxHasCamera.IsChecked ?? false;

                    // Update the device
                    using (BusinessManager manager = new BusinessManager())
                    {
                        manager.DeviceBusiness.UpdateDevice(phone);
                    }
                }
                else
                {
                    // Update the device
                    using (BusinessManager manager = new BusinessManager())
                    {
                        manager.DeviceBusiness.UpdateDevice(computer);
                    }
                }
            }

            // Alert the user that the device has been updated successfully
            MessageBox.Show("Successfully updated the device.", "Success");

            // Close the window
            this.Close();
        }

        /// <summary>
        /// Populates fields with device data
        /// </summary>
        private void PopulateFields()
        {
            textBoxID.Text = _device.Id.ToString();
            textBoxName.Text = _device.Name;
            textBoxMake.Text = _device.Make;
            textBoxModel.Text = _device.Model;
            textBoxSerialNumber.Text = _device.SerialNumber;
            textBoxNotes.Text = _device.Notes;
            datePickerPurchaseDate.SelectedDate = (DateTime)_device.PurchaseDate;
            datePickerExpiryDate.SelectedDate = (DateTime)_device.ExpiryDate;

            Type viewType = _propertyView.GetType();
            Type view2Type = null;
            if (_propertyView2 != null)
                view2Type = _propertyView2.GetType();

            if (viewType == typeof(MonitorPropertyView))
            {
                Monitor device = _device as Monitor;
                MonitorPropertyView view = _propertyView as MonitorPropertyView;

                view.checkBoxDisplayPort.IsChecked = device.DisplayInterfaces.DisplayPort;
                view.checkBoxDVI.IsChecked = device.DisplayInterfaces.DVI;
                view.checkBoxHDMI.IsChecked = device.DisplayInterfaces.HDMI;
                view.checkBoxVGA.IsChecked = device.DisplayInterfaces.VGA;

                view.textBoxScreenSize.Text = device.ScreenSize.ToString();
            }
            else if (viewType == typeof(ComputerPropertyView))
            {
                Computer computer = _device as Computer;
                ComputerPropertyView view = _propertyView as ComputerPropertyView;

                // Create ViewModel for each disk type so it can be
                // added to the combo box
                // todo: change to auto mapper at some point?
                List<DiskTypeViewModel> diskTypeViews = new List<DiskTypeViewModel>();
                foreach (DiskType type in Enum.GetValues(typeof(DiskType)))
                {
                    diskTypeViews.Add(new DiskTypeViewModel { Name = type.ToString(), Type = type });
                }

                // Set combo box to dispaly disk types
                view.comboBoxDiskTypes.ItemsSource = diskTypeViews;

                view.textBoxProcessor.Text = computer.Processor;
                view.textBoxStorage.Text = computer.DiskSpace.ToString();
                view.textBoxMemory.Text = computer.Memory.ToString();
                view.textBoxOS.Text = computer.OperatingSystem;
                view.comboBoxDiskTypes.SelectedItem = diskTypeViews.FirstOrDefault(x => x.Name == computer.DiskType.ToString());

                // Create ViewModel for each product key so it
                // can be added to the list view
                List<KeyListViewModel> keyViews = new List<KeyListViewModel>();
                foreach (ProductKey key in computer.ProductKeys)
                {
                    keyViews.Add(new KeyListViewModel { Name = key.Name, Key = key.Key });
                }

                // Set the item source so the list view shows product keys
                view.listViewKeys.ItemsSource = keyViews;
            }

            if (view2Type == typeof(PhonePropertyView))
            {
                Phone phone = _device as Phone;
                PhonePropertyView view = _propertyView2 as PhonePropertyView;

                view.checkBoxHasCamera.IsChecked = phone.HasCamera;
            }
        }
    }
}
