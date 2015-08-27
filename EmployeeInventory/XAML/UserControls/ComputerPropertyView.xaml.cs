using ES.InventoryRegister.ViewModels;
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
    /// Interaction logic for ComputerPropertyView.xaml
    /// </summary>
    public partial class ComputerPropertyView : UserControl
    {
        public List<KeyListViewModel> KeyViewModels;

        public ComputerPropertyView()
        {
            InitializeComponent();

            listViewKeys.ItemsSource = KeyViewModels;
            KeyViewModels = new List<KeyListViewModel>();

            buttonAddKey.Click += ButtonAddKey_Click;
        }

        private void ButtonAddKey_Click(object sender, RoutedEventArgs e)
        {
            // Create and show a new AddKey window
            AddKey addKeyWindow = new AddKey();
            addKeyWindow.ShowDialog();

            // Check to see if 
            if (addKeyWindow.DialogResult.HasValue && addKeyWindow.DialogResult.Value)
            {
                KeyListViewModel keyViewModel = new KeyListViewModel();
                keyViewModel.Key = addKeyWindow.KeyValue;
                keyViewModel.Name = addKeyWindow.KeyName;

                KeyViewModels.Add(keyViewModel);

                listViewKeys.ItemsSource = KeyViewModels;
                listViewKeys.Items.Refresh();
            }
        }
    }
}
