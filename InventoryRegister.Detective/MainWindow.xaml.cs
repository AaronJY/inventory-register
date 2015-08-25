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
using System.Xml.Serialization;
using System.IO;
using System.Xml;

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

            int diskSize =
                Int32.Parse(WmiHelper.RequestWmi("SELECT Size FROM Win32_LogicalDisk WHERE DeviceID = 'C:'"));
            AddDetail("Disk Size", diskSize.ToString());

            string processor =
                WmiHelper.RequestWmi("SELECT Name FROM Win32_Processor");
            AddDetail("Processor", processor);

            string memory =
                WmiHelper.RequestWmi("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            // Convert from bytes to GB
            double memoryGb = Math.Round(long.Parse(memory) / 1024d / 1024d / 1024d);
            AddDetail("Memory (GB)", memoryGb.ToString());

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

    public class Device
    {
        public string Owner { get; set; }
        public string Name { get; set; }
    }

    public class Computer : Device
    {
        public int DiskSize { get; set; }
        public int Memory { get; set; }
        public string OperatingSystem { get; set; }
    }

    public class Desktop : Computer
    {

    }

    public class Laptop : Computer
    {

    }

    public class Tablet : Computer
    {

    }
}
