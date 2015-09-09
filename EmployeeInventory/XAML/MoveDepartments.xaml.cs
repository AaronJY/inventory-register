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
using ES.InventoryRegister.Business;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
using AutoMapper;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for MoveDepartments.xaml
    /// </summary>
    public partial class MoveDepartments : Window
    {
        public string DepartmentName;
        private List<DepartmentMoveViewModel> _devicesInUse;

        public MoveDepartments(string departmentName)
        {
            InitializeComponent();

            #region Event listeners
            buttonChooseDepartment.Click += buttonChooseDepartment_Click;
            buttonMoveAll.Click += buttonMoveAll_Click;
            buttonContinue.Click += buttonContinue_Click;
            #endregion

            // Store the department name so it can be accessed later
            DepartmentName = departmentName;

            // Replace the placeholder in the message text block with the department name
            textBlockMessage.Text = String.Format(textBlockMessage.Text, departmentName);

            PopulateListWithDevicesInUse();
        }

        void buttonContinue_Click(object sender, RoutedEventArgs e)
        {
            var cantMove = _devicesInUse.Any(x => x.MoveTo == null);

            if (cantMove)
            {
                MessageBox.Show("New departments have not been applied to all listed devices.", "Error");
                return;
            }
            else
            {
                foreach (DepartmentMoveViewModel device in _devicesInUse)
                {
                    using (BusinessManager manager = new BusinessManager())
                    {
                        Device newDevice = manager.DeviceBusiness.GetDevice(device.DeviceId);
                        newDevice.Owner.Department = manager.DepartmentBusiness.GetDepartment(DepartmentName);

                        manager.DeviceBusiness.UpdateDevice(newDevice);
                    }
                }
            }
        }

        void buttonMoveAll_Click(object sender, RoutedEventArgs e)
        {
            // Get ViewModel for new department
            var value = (DepartmentViewModel)GetValueFromInput();

            if (value != null)
            {
                // Change the "new department" propertly for all selected view
                // models to the ViewModel for the department
                foreach (DepartmentMoveViewModel item in listViewDevices.Items)
                {
                    item.MoveTo = value.Name;
                }

                // Refresh the list
                listViewDevices.Items.Refresh();
            }
        }

        void buttonChooseDepartment_Click(object sender, RoutedEventArgs e)
        {
            // Don't continue if there isn't any item selected
            if (listViewDevices.SelectedItem == null)
            {
                MessageBox.Show("Please select a device to assign a new department.", "Error");
                return;
            }

            // Get input value from input box
            var value = (DepartmentViewModel)GetValueFromInput();

            if (value != null)
            {
                var newDepartmentName = value.Name;
                if (String.IsNullOrEmpty(newDepartmentName))
                {
                    MessageBox.Show("Please provide a valid department name!");
                }
                else
                {
                    // Change new department values of all selected entries in the list
                    foreach (DepartmentMoveViewModel item in listViewDevices.SelectedItems)
                    {
                        item.MoveTo = newDepartmentName;
                    }

                    // Refresh the list to show updates
                    listViewDevices.Items.Refresh();
                }
            }
        }

        public void PopulateListWithDevicesInUse()
        {
            List<Device> devices;
            List<DepartmentMoveViewModel> deviceViewModels;

            using (BusinessManager manager = new BusinessManager())
            {
                devices = manager.DeviceBusiness.GetDevicesInUseByDepartment(DepartmentName);
            }

            // Convert devices to DepartmentMoveViewModels
            _devicesInUse = Mapper.Map<List<Device>, List<DepartmentMoveViewModel>>(devices);

            listViewDevices.ItemsSource = _devicesInUse;
        }

        /// <summary>
        /// Opens the input box and returns the entered
        /// value
        /// </summary>
        /// <returns>Entered value</returns>
        private object GetValueFromInput()
        {
            // Get list of departments from the database
            List<DepartmentViewModel> departments;
            using (BusinessManager manager = new BusinessManager())
            {
                departments = manager.DepartmentBusiness.GetDepartmentsAsViewModels();
                
                // Remove the name of the department you're removing
                departments = departments.Where(x => x.Name != DepartmentName).ToList();
            }

            // Get the department name from an input box
            var input = InputComboBox.Show("New Department", departments);
            return input.Value;
        }
    }
}
