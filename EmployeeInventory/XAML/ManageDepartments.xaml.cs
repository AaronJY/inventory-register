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
        public ManageDepartments()
        {
            InitializeComponent();

            this.Loaded += ManageDepartments_Loaded;
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
            List<DepartmentViewModel> departmentViews = new List<DepartmentViewModel>();
            departmentViews = Mapper.Map(departments, departmentViews).OrderBy(department => department.Name).ToList();

            // Show the view models in the listView
            listViewDepartments.ItemsSource = departmentViews;
        }
    }
}
