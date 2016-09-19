using AceMoneyImport.Commands;
using ExcelData;
using System;
using System.ComponentModel;
using System.Data;
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
                    OnPropertyChanged();
                    
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
                OnPropertyChanged();
                
            }
        }

        public RelayCommand DoConvert { get; set; }
        public MainViewModel()
        {
            WindowTitle = string.Format("AceMoney Import v.{0}", GetVersion());
            DoConvert = new RelayCommand(ConvertToCsv,CreateCsvCanExecute);
        }

        public async void ConvertToCsv()
        {
            using (new WaitCursor())
            {

                try
                {
                    DataTable _dataFromExcel = ExcelHelper.GetData(InputFile);
                    await Task.Run(() => _dataFromExcel.WriteToCsvFile(OutFile));

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        public string GetVersion()
        {
            var _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            return string.Format("{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

        }


        // Dessa metoder "kan" inte implementeras än p.g.a. att det är svårt att skicka med EventArgs utan t.ex. MVVM Light
        //public void FileBox_Drop(object sender, DragEventArgs e)
        //{
        //    TextBox _box = sender as TextBox;

        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        string[] _files = (string[])e.Data.GetData(DataFormats.FileDrop);

        //        InputFile = _files[0].ToString();
        //    }
        //}

        //public void FileBox_PreviewDragOver(object sender, DragEventArgs e)
        //{
        //    e.Handled = true;
        //}

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
