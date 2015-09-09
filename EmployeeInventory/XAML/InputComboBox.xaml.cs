using System;
using System.Collections;
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
    /// Interaction logic for InputComboBox.xaml
    /// </summary>
    public partial class InputComboBox : Window
    {
        public IEnumerable Items;
        public object Value;

        public InputComboBox()
        {
            InitializeComponent();

            buttonOK.Click += buttonOK_Click;
            this.Closing += InputComboBox_Closing;
        }

        void InputComboBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }

        void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        public static InputComboBox Show(string value, IEnumerable items)
        {
            InputComboBox box = new InputComboBox();
            box.textBlockLabel.Text = value;
            box.comboBoxValue.ItemsSource = items;

            box.ShowDialog();

            return box;
        }

        public static InputComboBox Show(string value, IEnumerable items, string title)
        {
            var box = new InputComboBox();
            box.textBlockLabel.Text = value;
            box.comboBoxValue.ItemsSource = items;
            box.Title = title;

            box.ShowDialog();

            return box;
        }

        public static InputComboBox Show(string value, IEnumerable items, string title, string buttonText)
        {
            var box = new InputComboBox();
            box.textBlockLabel.Text = value;
            box.comboBoxValue.ItemsSource = items;
            box.Title = title;
            box.buttonOK.Content = buttonText;

            box.ShowDialog();

            return box;
        }

        /// <summary>
        /// Sends the data back and closes the input box
        /// </summary>
        private void Submit()
        {
            Value = comboBoxValue.SelectedItem;

            DialogResult = true;
        }
    }
}
