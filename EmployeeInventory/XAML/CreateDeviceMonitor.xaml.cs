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

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for CreateDeviceMonitor.xaml
    /// </summary>
    public partial class CreateDeviceMonitor : Window
    {
        public Monitor Entity;

        public CreateDeviceMonitor(Monitor monitor)
        {
            InitializeComponent();

            Entity = monitor;

            #region Event listeners
            buttonNext.Click += buttonNext_Click;
            #endregion
        }

        void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Next();
        }

        /// <summary>
        /// Advances the creation procedure by passing back
        /// the entity with populated properties
        /// </summary>
        private void Next()
        {
            // Assign dispaly interface values
            Entity.DisplayInterfaces = new DisplayInterfaces();
            Entity.DisplayInterfaces.VGA = checkBoxVGA.IsChecked ?? true;
            Entity.DisplayInterfaces.DVI = checkBoxDVI.IsChecked ?? true;
            Entity.DisplayInterfaces.HDMI = checkBoxHDMI.IsChecked ?? true;
            Entity.DisplayInterfaces.DisplayPort = checkBoxDisplayPort.IsChecked ?? true;

            // Set screen size
            Entity.ScreenSize = Int32.Parse(textBoxScreenSize.Text);

            DialogResult = true;
        }
    }
}
