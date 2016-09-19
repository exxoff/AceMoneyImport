using AceMoneyImport.ViewModels;
using ExcelData;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AceMoneyImport
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //ImportItem Item;
        MainViewModel viewModel;
        

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            //TODO: Fixa detta via DI istället
            //viewModel = new MainViewModel();
            this.viewModel = viewModel;

            //ImportItem _item = new ImportItem();

            //Item = _item;
            this.DataContext = viewModel;
            
            //Item.PropertyChanged += Item_PropertyChanged;

            this.Title = string.Format("AceMoney Import v.{0}", viewModel.GetVersion());
        }

        //private string GetVersion()
        //{
        //    var _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        //    return string.Format("{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);


        //}

        //private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    //this.DataContext = Item;
        //}

        private void FileBox_Drop(object sender, DragEventArgs e)
        {
            TextBox _box = sender as TextBox;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);

                viewModel.InputFile = _files[0].ToString();
            }
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    using (new WaitCursor())
        //    { 
        //        if (viewModel != null)
        //        {
        //            try
        //            {
        //                DataTable _dataFromExcel = ExcelHelper.GetData(viewModel.InputFile);
        //                _dataFromExcel.WriteToCsvFile(viewModel.OutFile);

        //            }
        //            catch (Exception ex)
        //            {

        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //}

        private void CreateCsvExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                if (viewModel != null)
                {
                    try
                    {
                        DataTable _dataFromExcel = ExcelHelper.GetData(viewModel.InputFile);
                        _dataFromExcel.WriteToCsvFile(viewModel.OutFile);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void CreateCsvCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {


            e.CanExecute = (isValidPath(txtInput.Text) && isValidPath(txtOutput.Text));



        }

        private bool isValidPath(string FilePath)
        {

            try
            {
                FileInfo _file = new FileInfo(FilePath);

                if (Directory.Exists(_file.Directory.ToString()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }


        }


    }
}
