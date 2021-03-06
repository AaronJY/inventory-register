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

            comboBoxOwner.SelectionChanged += ComboBoxOwner_SelectionChanged;
            buttonClearOwner.Click += ButtonClearOwner_Click;
        }

        private void ComboBoxOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonClearOwner.IsEnabled = true;
        }

        private void ButtonClearOwner_Click(object sender, RoutedEventArgs e)
        {
            comboBoxOwner.SelectedItem = null;
            buttonClearOwner.IsEnabled = false;
        }

        /// <summary>
        /// Loads a list of employees from the database into the owners combo box
        /// </summary>
        void LoadOwners()
        {
            using (var manager = new BusinessManager())
            {
                comboBoxOwner.ItemsSource = manager.EmployeeBusiness.GetEmployeesAsViewModels();
            }
        }
    }
}
