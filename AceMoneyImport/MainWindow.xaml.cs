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
using ExcelData;
using System.Data;

namespace AceMoneyImport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ImportItem Item;
        public MainWindow()
        {
            InitializeComponent();

            ImportItem _item = new ImportItem();

            Item = _item;
            
            Item.PropertyChanged += Item_PropertyChanged;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.DataContext = Item;
        }

        private void FileBox_Drop(object sender, DragEventArgs e)
        {
            TextBox _box = sender as TextBox;

            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);

                Item.InputFile = _files[0].ToString();
            }
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            { 
                if (Item != null)
                {
                    try
                    {
                        DataTable _dataFromExcel = ExcelHelper.GetData(Item.InputFile);
                        _dataFromExcel.WriteToCsvFile(Item.OutFile);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
