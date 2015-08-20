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
using ES.InventoryRegister.Data.Infrastructure;
using ES.InventoryRegister.Data.Repositories;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
using AutoMapper;
using ES.InventoryRegister.Business;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageEmployees : Window
    {
        public ManageEmployees()
        {
            InitializeComponent();

            #region Define event Listeners
            buttonAdd.Click += buttonAdd_Click;
            buttonRefresh.Click += buttonRefresh_Click;
            buttonRemove.Click += buttonRemove_Click;
            listViewEmployees.SelectionChanged += listViewEmployees_SelectionChanged;
            this.Loaded += ManageEmployees_Loaded;
            #endregion
        }

        void listViewEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable the remove button when a selection is made
            buttonRemove.IsEnabled = true;
        }

        void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            EmployeeViewModel selectedItem = (EmployeeViewModel)listViewEmployees.SelectedItem;

            // Don't continue if there isn't a selected user
            if (listViewEmployees.SelectedItem == null)
                return;

            // Create a new dialog to double check deletion
            ConfirmEmployeeDelete dialog = new ConfirmEmployeeDelete(selectedItem.Name, selectedItem.Id);
            dialog.ShowDialog();

            // If the user has chosen "Yes"
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {
                using (BusinessManager manager = new BusinessManager())
                {
                    // Remove employee from the list
                    manager.EmployeeBusiness.RemoveEmployee(selectedItem.Id);
                }

                MessageBox.Show("Successfully deleted employee!", "Alert");

                // Repopulate list
                PopulateListWithEmployees();
            }
        }

        void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            PopulateListWithEmployees();
        }

        void ManageEmployees_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListWithEmployees();   
        }

        void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployee createEmployeeWindow = new CreateEmployee(this);
            createEmployeeWindow.ShowDialog();
        }

        /// <summary>
        /// Grabs employees from the database and populates the list
        /// </summary>
        public void PopulateListWithEmployees()
        {
            List<Employee> employees;
            List<EmployeeViewModel> viewModels;

            // Get employees from the database
            using (BusinessManager manager = new BusinessManager())
            {
                // Get employees from the database
                employees = manager.EmployeeBusiness.GetEmployees();

                // Create collection of view models to show in the list
                viewModels = Mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);
            }

            // Set item source to view models to display list entries
            listViewEmployees.ItemsSource = viewModels;
        }
    }
}
