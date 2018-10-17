using AceMoneyImport.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AceMoneyImport
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        //EuroCardImportItem Item;
        MainViewModel viewModel;
        

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            
            //this.Title = string.Format("AceMoney Import v.{0}", viewModel.GetVersion());

        }

        private void FileBox_Drop(object sender, DragEventArgs e)
        {
            

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null)
                {
                    viewModel.InputFile = files[0];

                }
            }
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }




    }
}
