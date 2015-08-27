using ES.InventoryRegister.Data.Infrastructure;
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
    /// Interaction logic for ManageConnections.xaml
    /// </summary>
    public partial class ManageConnections : Window
    {
        public ManageConnections()
        {
            InitializeComponent();

            #region Event listeners
            this.Loaded += ManageConnections_Loaded;
            buttonSave.Click += buttonSave_Click;
            #endregion
        }

        void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // Save the connection string to the text file
            string connectionString = textBoxConnectionString.Text.Trim();
            DbManager.SaveConnectionString(connectionString);

            // Show a message box
            MessageBox.Show("Successfully saved connection string.", "Success");

            // Restart the application
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        void ManageConnections_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxConnectionString.Text = DbManager.GetConnectionString();
        }
    }
}
