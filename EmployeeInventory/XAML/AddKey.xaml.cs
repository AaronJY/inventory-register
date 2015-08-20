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
    /// Interaction logic for AddKey.xaml
    /// </summary>
    public partial class AddKey : Window
    {
        public string KeyName;
        public string KeyValue;

        public AddKey()
        {
            InitializeComponent();

            #region Event listeners
            buttonConfirm.Click += buttonConfirm_Click;
            #endregion

        }

        void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.KeyName = textBoxName.Text.Trim();
            this.KeyValue = textBoxValue.Text.Trim();

            if (KeyName == "" || KeyValue == "")
            {
                MessageBox.Show("Please provide both a key name and value!");
                return;
            }

            DialogResult = true;
        }
    }
}
