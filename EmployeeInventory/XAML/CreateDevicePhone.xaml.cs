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

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for CreateDevicePhone.xaml
    /// </summary>
    public partial class CreateDevicePhone : Window
    {
        public Phone Entity;

        public CreateDevicePhone(Phone entity)
        {
            InitializeComponent();

            // Store phone for access later
            Entity = entity;

            #region Event listeners
            buttonNext.Click += buttonNext_Click;
            #endregion
        }

        void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            // Set hascamera property
            Entity.HasCamera = checkBoxCamera.IsChecked ?? false;

            DialogResult = true;
        }
    }
}
