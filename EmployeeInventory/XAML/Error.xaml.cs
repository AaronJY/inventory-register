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
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class Error : Window
    {
        private Exception _exception;

        public Error(Exception ex, string message)
        {
            InitializeComponent();

            textBlockMessage.Text = message;

            _exception = ex;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonExpandExcpetion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_exception.ToString(), "Error Details");
        }
    }

    public static class ErrorHandler
    {
        /// <summary>
        /// Shows the error window and populates it with information
        /// with the given exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Error Show(Exception ex, string message)
        {
            Error error = new Error(ex, message);
            error.ShowDialog();

            return error;
        }
    }
}
