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

            KeyViewModels = new List<KeyListViewModel>();
        }

        public void AddKey(KeyListViewModel viewModel)
        {
            KeyViewModels.Add(viewModel);
            listViewKeys.ItemsSource = KeyViewModels;
        }
    }
}
