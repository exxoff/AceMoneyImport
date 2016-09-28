
using ExcelData.Helpers;
using ExcelData.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace AceMoneyImport.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string WindowTitle { get; set; }

        private string inputFile;

        public string InputFile
        {
            get { return inputFile; }
            set
            {
                if (value != null)
                {
                    inputFile = value;

                    if (OutFile == null)
                    {

                        string _fileName = Path.GetFileNameWithoutExtension(inputFile);
                        OutFile = string.Format("{0}\\{1}.csv", Path.GetDirectoryName(inputFile).ToString(), _fileName);
                        
                    }
                    item.InputFile = inputFile;
                    OnPropertyChanged();
                    DoConvert.RaiseCanExecuteChanged();
                    
                }
            }
        }

        private string outFile;
        public string OutFile
        {
            get { return outFile; }
            set
            {
                outFile = value;
                item.OutputFile = outFile;
                OnPropertyChanged();
                DoConvert.RaiseCanExecuteChanged();

            }
        }

        private IImportItem item;
        public RelayCommand DoConvert { get; set; }
        public MainViewModel(IImportItem item)
        {
            this.item = item;
            WindowTitle = string.Format("AceMoney Import v.{0}", GetVersion());
            DoConvert = new RelayCommand(() => ConvertToCsv(),() => CreateCsvCanExecute());

            InputFile = item.InputFile;
            OutFile = item.OutputFile;

        }

        public async void ConvertToCsv()
        {
            using (new WaitCursor())
            {


                ReturnObject ret = await Task<ReturnObject>.Run(() => this.item.ConvertToCsv());
                if (ret.ErrorNumber != 0)
                {
                    MessageBox.Show(ret.ErrorMessage);
                }
                
            }
            
        }

        public string GetVersion()
        {
            var _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format("{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

        }


        public bool CreateCsvCanExecute()
        {

            return (isValidPath(InputFile) && isValidPath(OutFile));
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

        private RelayCommand<DragEventArgs> dropCommand;

        public RelayCommand<DragEventArgs> DropCommand
        {
            get { return dropCommand; }
            set { dropCommand = value; }
        }

        private void FileBox_Drop(object sender, DragEventArgs e)
        {
            //TextBox _box = sender as TextBox;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);

                InputFile = _files[0].ToString();
            }
        }

        private void FileBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }


        #region PropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
}
