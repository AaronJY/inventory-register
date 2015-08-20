using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Entities;
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
using ES.InventoryRegister.ViewModels;
using AutoMapper;
using ES.InventoryRegister.Business;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        private ManageEmployees _parentWindow;

        public CreateEmployee(ManageEmployees parentWindow)
        {
            InitializeComponent();
            FetchDepartments();
            #region Event listeners
            buttonCreate.Click += buttonCreate_Click;
            #endregion

            _parentWindow = parentWindow;

            textBoxName.Focus();
        }

        /// <summary>
        /// Fetches a list of departments from the database
        /// and stores them for use
        /// </summary>
        private void FetchDepartments()
        {
            List<Department> departments;
            List<DepartmentViewModel> departmentViews;
            
            using (BusinessManager manager = new BusinessManager())
            {
                // Get departments from the database
                departments = manager.DepartmentBusiness.GetDepartments();

                // Map departments to department view objects
                departmentViews = Mapper.Map<List<Department>, List<DepartmentViewModel>>(departments);
            }

            // Update combo box item source to display members
            comboBoxDepartment.ItemsSource = departmentViews;
        }

        void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string department = comboBoxDepartment.Text;

            // Data validation
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(department))
            {
                MessageBox.Show("A name and a department must be given!", "Error");
                return;
            }

            // Check to see if this employee's name has been used for another employee
            bool nameUsed;
            using (BusinessManager manager = new BusinessManager())
                nameUsed = manager.EmployeeBusiness.IsEmployeeNameUsed(name);

            if (nameUsed)
            {
                MessageBoxResult continueResult = MessageBox.Show(
                    "This employee's name is already in use. Are you sure you wish to create another employee with the same name?",
                    "Alert",
                    MessageBoxButton.YesNo
                    );

                // If no, don't continue
                if (continueResult == MessageBoxResult.No)
                    return;
            }
            
            using (BusinessManager manager = new BusinessManager())
            {
                // Create a new employee
                Employee employee = new Employee();
                employee.Name = name;
                employee.Department = manager.UnitOfWork.Departments.Get(department);

                // Add it to the unit of work
                manager.EmployeeBusiness.AddEmployee(employee);
            }

            // Repopulate the employee list
            _parentWindow.PopulateListWithEmployees();

            // Close the window
            this.Close();
        }
    }
}
