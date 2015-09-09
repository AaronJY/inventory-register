using AutoMapper;
using ES.InventoryRegister.Business;
using ES.InventoryRegister.Entities;
using ES.InventoryRegister.ViewModels;
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

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for ManageDepartments.xaml
    /// </summary>
    public partial class ManageDepartments : Window
    {
        List<DepartmentViewModel> departmentsSource;

        public ManageDepartments()
        {
            InitializeComponent();

            this.Loaded += ManageDepartments_Loaded;
            buttonAdd.Click += buttonAdd_Click;
            buttonRemove.Click += buttonRemove_Click;
            buttonRefresh.Click += buttonRefresh_Click;
            listViewDepartments.SelectionChanged += listViewDepartments_SelectionChanged;
        }

        void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            PopulateListWithDepartments();
        }

        void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            var selected = (DepartmentViewModel)listViewDepartments.SelectedItem;
            var selectedDepartmentName = selected.Name;

            if (selected != null)
            {
                var msgBoxResult = MessageBox.Show(String.Format("Are you sure you want to delete '{0}'", selectedDepartmentName), "Delete", MessageBoxButton.YesNo);
                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    // Remove the department from the database
                    using (BusinessManager manager = new BusinessManager())
                    {
                        if (manager.DepartmentBusiness.IsDepartmentInUse(selectedDepartmentName))
                        {
                            MessageBox.Show("Department is in use");
                        }

                        //manager.DepartmentBusiness.RemoveDepartment(selectedDepartmentName);
                    }

                    // Refresh the department list
                    PopulateListWithDepartments();
                }
            }
        }

        void listViewDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonRemove.IsEnabled = true;
        }

        void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenCreateDepartmentWindow();
        }

        void ManageDepartments_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListWithDepartments();
        }

        /// <summary>
        /// Populates the main list view with departments
        /// from the server
        /// </summary>
        private void PopulateListWithDepartments()
        {
            // Get departments from the server and store them in
            // a variable
            List<Department> departments;
            using (BusinessManager manager = new BusinessManager())
            {
                departments = manager.DepartmentBusiness.GetDepartments();
            }

            // Covnert the departments into their respective view models
            departmentsSource = new List<DepartmentViewModel>();
            departmentsSource = Mapper.Map(departments, departmentsSource).OrderBy(department => department.Name).ToList();

            // Show the view models in the listView
            listViewDepartments.ItemsSource = departmentsSource;
        }

        /// <summary>
        /// Opens a new window to create a department
        /// </summary>
        private void OpenCreateDepartmentWindow()
        {
            CreateDepartment createDepartmentWindow = new CreateDepartment();
            createDepartmentWindow.ShowDialog();

            if (createDepartmentWindow.DialogResult.HasValue && createDepartmentWindow.DialogResult.Value)
            {
                string departmentName = createDepartmentWindow.DepartmentName;

                using (BusinessManager manager = new BusinessManager())
                {
                    // Only create the department if it doesn't already exist
                    // in the database
                    if (!manager.DepartmentBusiness.DepartmentExists(departmentName))
                    {
                        // Create the department
                        manager.DepartmentBusiness.CreateDepartment(departmentName);

                        // Show a message
                        MessageBox.Show(String.Format("Successfully created department '{0}'", departmentName));

                        // Reload the department list from the server
                        PopulateListWithDepartments();
                    }
                    else
                    {
                        MessageBox.Show("This department already exists in the database!", "Error");
                    }
                }
            }
        }
    }
}
