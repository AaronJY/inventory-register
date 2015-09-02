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
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.Business;
using ES.InventoryRegister.ViewModels;
using AutoMapper;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for CreateDevice.xaml
    /// </summary>
    public partial class CreateDevice : Window
    {
        private Inventory _inventoryInstance;

        public CreateDevice(Inventory inventoryInstance)
        {
            InitializeComponent();

            #region Event listeners
            buttonNext.Click += buttonNext_Click;
            #endregion

            // Store the inventory instance so it can be accessed later
            _inventoryInstance = inventoryInstance;

            PopulateOwnersDropdown();
        }

        /// <summary>
        /// Gets a list of employee view models and populates
        /// the owner drop-down with them
        /// </summary>
        private void PopulateOwnersDropdown()
        {
            // Get view models from the business
            List<EmployeeViewModel> employeeModels;
            using (BusinessManager manager = new BusinessManager())
            {
                employeeModels = manager.EmployeeBusiness.GetEmployeesAsViewModels();
            }

            // Set the item source as the view models
            comboBoxOwner.ItemsSource = employeeModels;
        }

        void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            bool created = false;

            string name = textBoxName.Text.Trim();
            string typeStr = comboBoxType.Text;
            string make = textBoxMake.Text.Trim();
            string model = textBoxModel.Text.Trim();
            string serialNumber = textBoxSerialNumber.Text.Trim();
            string ownerName = comboBoxOwner.Text;

            // Figure out which fields still need to be filled in
            List<string> toProvide = new List<string>();
            if (name == "")
                toProvide.Add("Name");
            if (typeStr == "")
                toProvide.Add("Type");
            if (make == "")
                toProvide.Add("Make");
            if (model == "")
                toProvide.Add("Model");
            if (serialNumber == "")
                toProvide.Add("Serial number");

            // If there are still fields to be filled in
            if (toProvide.Count > 0)
            {
                // Create a string listing the fields that beed to be filled in
                string fieldStr = "";
                foreach (string field in toProvide)
                {
                    fieldStr += field + ", ";
                }

                // Trim the command and space off of the last entry
                fieldStr = fieldStr.Substring(0, fieldStr.Length - 2);

                // Alert the user
                MessageBox.Show("You still need to provide the following details: " + fieldStr, "Error");
                return;
            }

            DateTime? purchaseDate = datePickerPurchaseDate.SelectedDate.HasValue ? datePickerPurchaseDate.SelectedDate.Value : default(DateTime);
            DateTime? expiryDate = datePickerExpiryeDate.SelectedDate.HasValue ? datePickerExpiryeDate.SelectedDate.Value : default(DateTime);

            // Get entity Type from typeStr
            Type type;
            using (BusinessManager manager = new BusinessManager())
                type = manager.GetEntityTypeFromStr(typeStr);

            // Create a new instance of the selected entity
            Device entity;
            if (type == typeof(Monitor))
                entity = new Monitor();
            else if (type == typeof(Desktop))
                entity = new Desktop();
            else if (type == typeof(Laptop))
                entity = new Laptop();
            else if (type == typeof(Phone))
                entity = new Phone();
            else if (type == typeof(Entities.Tablet))
                entity = new Entities.Tablet();
            else entity = null;

            // Populate properties with entered values
            entity.Name = name;
            entity.Make = make;
            entity.Model = model;
            entity.SerialNumber = serialNumber;
            entity.PurchaseDate = purchaseDate;
            entity.ExpiryDate = expiryDate;

            // If no owner is selected, set them to null
            if (ownerName == "(owner)")
            {
                entity.Owner = null;
            }
            else
            {
                // Get the employee's ID
                int employeeId = ((EmployeeViewModel)comboBoxOwner.SelectedItem).Id;

                // Set the owner to the employee found in the database with the same ID
                using (BusinessManager businessManager = new BusinessManager())
                {
                    entity.Owner = businessManager.EmployeeBusiness.GetEmployee(employeeId);
                }
            }

            // Hide the current window
            this.Hide();

            // Check if the entity inherits Computer
            if (type.IsSubclassOf(typeof(Computer)))
            {
                // Create new instance of computer details window and pass entity in
                CreateDeviceComputer cdc = new CreateDeviceComputer(entity as Computer);
                // Show the computer details window
                cdc.ShowDialog();

                if (cdc.DialogResult.HasValue && cdc.DialogResult.Value == true)
                {
                    if (type == typeof(Phone))
                    {
                        // Create new instance of phone details window and pass entity in
                        CreateDevicePhone cdp = new CreateDevicePhone(entity as Phone);
                        // Show the phone details window
                        cdp.ShowDialog();

                        // When the "Next..." button has been clicked in the phone details window
                        if (cdp.DialogResult.HasValue && cdp.DialogResult.Value)
                        {
                            // Get the entity from the phone details window
                            Phone phone = cdp.Entity;

                            // Add the device to the database
                            using (BusinessManager manager = new BusinessManager())
                            {
                                manager.DeviceBusiness.AddDevice(phone);
                            }

                            created = true;
                        }
                    }
                    else
                    {
                        // Get the entity from the other window
                        Computer computer = cdc.Entity;

                        // Add the device to the database
                        using (BusinessManager manager = new BusinessManager())
                        {
                            manager.DeviceBusiness.AddDevice(computer);
                        }

                        created = true;
                    }
                }
            }
            else if (type == typeof(Monitor))
            {
                // Create a new instance of the monitor details window and pass entity in
                CreateDeviceMonitor cdm = new CreateDeviceMonitor(entity as Monitor);
                // Show the monitor details window
                cdm.ShowDialog();

                // When the "Next..." button has been clicked in the monitor details window
                if (cdm.DialogResult.HasValue && cdm.DialogResult.Value)
                {
                    // Get the entity from the other window
                    Monitor monitor = cdm.Entity;

                    // Add the monitor to the database
                    using (BusinessManager manager = new BusinessManager())
                    {
                        manager.DeviceBusiness.AddDevice(monitor);
                    }

                    created = true;
                }
            }

            // Check to see if the device has been created successfully
            if (created)
            {
                MessageBox.Show("Your device has successfully been added to the database", "Success");
            }

            // Refresh the device list
            _inventoryInstance.GetDevices();

            // Close the create device window
            this.Close();
            
        }
    }
}
