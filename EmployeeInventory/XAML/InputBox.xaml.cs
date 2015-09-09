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
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public string Value;

        public InputBox()
        {
            InitializeComponent();

            buttonOK.Click += buttonOK_Click;
            textBoxValue.KeyDown += textBoxValue_KeyDown;
            ShowDialog();
        }

        public static InputBox Show(string value)
        {
            InputBox box = new InputBox();
            box.textBlockLabel.Text = value;
            return box;
        }

        public static InputBox Show(string value, string title)
        {
            InputBox box = new InputBox();
            box.textBlockLabel.Text = value;
            box.Title = title;
            return box;
        }

        public static InputBox Show(string value, string title, string buttonText)
        {
            InputBox box = new InputBox();
            box.textBlockLabel.Text = value;
            box.Title = title;
            box.buttonOK.Content = buttonText;
            return box;
        }

        void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }

        /// <summary>
        /// Sends the data back and closes the input box
        /// </summary>
        private void Submit()
        {
            Value = textBoxValue.Text;

            DialogResult = true;
        }
    }
}
