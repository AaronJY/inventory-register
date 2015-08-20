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
    /// Interaction logic for ConfirmEmployeeDelete.xaml
    /// </summary>
    public partial class ConfirmEmployeeDelete : Window
    {
        public ConfirmEmployeeDelete(string employeeName, int EmployeeId)
        {
            InitializeComponent();

            // Replace placholder with employee name
            textBlockMessage.Text = String.Format("Are you sure you wish to delete\n'{0}'\nas an employee?", employeeName);

            #region Event listeners
            buttonYes.Click += buttonYes_Click;
            buttonNo.Click += buttonNo_Click;
            #endregion
        }

        void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
