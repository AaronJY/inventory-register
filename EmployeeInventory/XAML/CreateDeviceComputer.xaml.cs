using ES.InventoryRegister.Entities;
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
using System.Windows.Shapes;
using AutoMapper;
using System.Text.RegularExpressions;

namespace ES.InventoryRegister.XAML
{
    /// <summary>
    /// Interaction logic for CreateDeviceComputer.xaml
    /// </summary>
    public partial class CreateDeviceComputer : Window
    {
        public Computer Entity;
        public List<ProductKey> ProductKeys;

        public CreateDeviceComputer(Computer entity)
        {
            InitializeComponent();

            // Store the passed entity
            Entity = entity;

            // Get disk types from Computer entity
            comboBoxDiskTypes.ItemsSource = Enum.GetNames(typeof(DiskType));

            #region Event listeners
            buttonNext.Click += buttonNext_Click;
            buttonAddKey.Click += buttonAddKey_Click;
            buttonDeleteKey.Click += buttonDeleteKey_Click;
            textBoxMemory.PreviewTextInput += NumbericTextBoxPreviewTextInput;
            textBoxStorage.PreviewTextInput += NumbericTextBoxPreviewTextInput;
            #endregion
        }

        void NumbericTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        void buttonDeleteKey_Click(object sender, RoutedEventArgs e)
        {
            // Remove the currently selected item from the list
            if (listViewKeys.SelectedItem != null)
                listViewKeys.Items.Remove(listViewKeys.SelectedItem);
        }

        void buttonAddKey_Click(object sender, RoutedEventArgs e)
        {
            // Open the add key window
            AddKey addKeyWindow = new AddKey();
            addKeyWindow.ShowDialog();

            // Once a key has been given, add it to the list
            if (addKeyWindow.DialogResult.HasValue && addKeyWindow.DialogResult.Value)
            {
                string keyName = addKeyWindow.KeyName;
                string keyValue = addKeyWindow.KeyValue;

                listViewKeys.Items.Add(new KeyListViewModel { Name = keyName, Key = keyValue });
            }
        }

        void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Entity.Processor = textBoxProcessor.Text.Trim();
            Entity.Memory = Int32.Parse(textBoxMemory.Text);
            Entity.DiskSpace = Int32.Parse(textBoxStorage.Text);
            Entity.DiskType = (DiskType)Enum.Parse(typeof(DiskType), comboBoxDiskTypes.SelectedValue.ToString());
            Entity.OperatingSystem = textBoxOS.Text.Trim();

            List<KeyListViewModel> items = listViewKeys.Items.Cast<KeyListViewModel>()
                .Select(item => item)
                .ToList();

            ProductKeys = Mapper.Map(items, ProductKeys);
            
            DialogResult = true;
        }

    }
}
