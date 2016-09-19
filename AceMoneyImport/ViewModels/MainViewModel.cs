using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AceMoneyImport.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Version { get; set; }

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
            set { outFile = value; OnPropertyChanged(); }
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

        public void CreateCsvCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {


            e.CanExecute = (isValidPath(InputFile) && isValidPath(OutFile));



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
