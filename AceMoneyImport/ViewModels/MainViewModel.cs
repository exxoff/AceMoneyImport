using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using ExcelData.Interfaces;
using ExcelData.Models;
using GalaSoft.MvvmLight.Command;

namespace AceMoneyImport.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputFile;

        private IImportItem _item;

        private string _outFile;
        private string _itemType;

        public MainViewModel(IImportItem item)
        {
            this._item = item;
            WindowTitle = $"AceMoney Import v.{GetVersion()}";
            DoConvert = new RelayCommand(() => ConvertToCsv(), () => CreateCsvCanExecute());
            SetItemType = new RelayCommand<string>((type) => DoSetItemType(type));

            //InputFile = item.InputFile;
            //OutFile = item.OutputFile;
        }

        public string WindowTitle { get; set; }

        public string InputFile
        {
            get => _inputFile;
            set
            {
                //if (value != null)
                //{
                    _inputFile = value;

                    if (OutFile == null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(_inputFile);
                        OutFile = $"{Path.GetDirectoryName(_inputFile)}\\{fileName}.csv";
                    }

                    _item.InputFile = _inputFile;
                    OnPropertyChanged();
                    DoConvert.RaiseCanExecuteChanged();
                //}
            }
        }

        public string ItemType
        {
            get => _itemType;
            set
            {
                _itemType = value;
                OnPropertyChanged();
            }
            
        }

        public string OutFile
        {
            get => _outFile;
            set
            {
                _outFile = value;
                _item.OutputFile = _outFile;
                OnPropertyChanged();
                DoConvert.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<string> SetItemType { get; set; }

        public RelayCommand DoConvert { get; }

        public RelayCommand<DragEventArgs> DropCommand { get; set; }

        private void DoSetItemType(string type)
        {
            InputFile = null;
            OutFile = null;

            _item = ImportItemFactory.GetImportItem(type);
        }

        public async void ConvertToCsv()
        {
            using (new WaitCursor())
            {
                var ret = await Task.Run(() => _item.ConvertToCsv());
                if (ret.ErrorNumber != 0) MessageBox.Show(ret.ErrorMessage,"AceMoneyImport",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            InputFile = null;
            OutFile = null;
        }

        public string GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }


        public bool CreateCsvCanExecute()
        {
            return IsValidPath(InputFile) && IsValidPath(OutFile);
        }

        private bool IsValidPath(string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);

                if (Directory.Exists(file.Directory.ToString())) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void FileBox_Drop(object sender, DragEventArgs e)
        {
            //TextBox _box = sender as TextBox;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                InputFile = files[0];
            }
        }

        private void FileBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }


        #region PropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}