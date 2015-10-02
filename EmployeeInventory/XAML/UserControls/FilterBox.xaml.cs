﻿using ES.InventoryRegister.Business;
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

namespace ES.InventoryRegister.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for FilterBox.xaml
    /// </summary>
    public partial class FilterBox : UserControl
    {
        public FilterBox()
        {
            InitializeComponent();

            LoadOwners();
        }

        void LoadOwners()
        {
            using (var manager = new BusinessManager())
            {
                comboBoxOwner.ItemsSource = manager.EmployeeBusiness.GetEmployeesAsViewModels();
            }
        }
    }
}