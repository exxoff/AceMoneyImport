using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceMoneyImport
{
    class ImportItem:INotifyPropertyChanged
    {
        private string _inputFile;
        private string _outFile;

        public event PropertyChangedEventHandler PropertyChanged;

        public string OutFile
        {
            get { return _outFile; }
            set
            {
                if(value != null)
                {
                    _outFile = value;
                    RaisePropertyChanged("OutFile");
                }

            }
        }

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                if(value != null)
                {
                    _inputFile = value;

                    if(OutFile==null)
                    {
                        
                        string _fileName = Path.GetFileNameWithoutExtension(_inputFile);
                        OutFile = string.Format("{0}\\{1}.csv",Path.GetDirectoryName(_inputFile).ToString(),_fileName);
                    }

                    

                    RaisePropertyChanged("InputFile");
                }
            }
                
        }



        private void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
                }

        }


    }
}
