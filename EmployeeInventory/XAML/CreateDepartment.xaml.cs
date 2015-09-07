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
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        public string DepartmentName;

        public CreateDepartment()
        {
            InitializeComponent();

            buttonCreate.Click += buttonCreate_Click;
        }

        void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            DepartmentName = textBoxDepartment.Text;

            DialogResult = true;
        }
    }
}
