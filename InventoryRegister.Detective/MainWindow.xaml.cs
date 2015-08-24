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
using System.Management;
using Microsoft.VisualBasic.Devices;

namespace InventoryRegister.Detective
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            buttonSave.Click += ButtonSave_Click;

            CollectDetails();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Collects details for the detective
        /// </summary>
        private void CollectDetails()
        {

            string diskSize =
                WmiHelper.RequestWmi("SELECT Size FROM Win32_LogicalDisk WHERE DeviceID = 'C:'");
            AddDetail("Disk Size", diskSize);

            string processor =
                WmiHelper.RequestWmi("SELECT Name FROM Win32_Processor");
            AddDetail("Processor", processor);

            string memory =
                WmiHelper.RequestWmi("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            // Convert from bytes to GB
            string memoryGb = Math.Round(long.Parse(memory) / 1024d / 1024d / 1024d).ToString();
            AddDetail("Memory (GB)", memoryGb);

            string os =
                WmiHelper.RequestWmi("SELECT Caption FROM Win32_OperatingSystem");
            AddDetail("Operating System", os);


            MessageBox.Show(Environment.UserName);
        }

        private void AddDetail(string name, string text)
        {
            string newline = textBlockDetails.Text == "" ? "" : "\n";
            textBlockDetails.Text += String.Format("{0}{1}: {2}", newline, name, text);
        }
    }
}
