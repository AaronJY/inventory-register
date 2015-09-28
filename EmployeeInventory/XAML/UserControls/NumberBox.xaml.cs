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
    /// Interaction logic for NumberBox.xaml
    /// </summary>
    public partial class NumberBox : TextBox
    {
        public int Number
        {
            get
            {
                int num;
                if (Int32.TryParse(this.Text, out num))
                {
                    return num;
                }
                else return 0;
            }

            set
            {
                this.Text = value.ToString();
            }
        }
        public NumberBox()
        {
            InitializeComponent();

            this.PreviewTextInput += NumberBox_PreviewTextInput;
        }

        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}

