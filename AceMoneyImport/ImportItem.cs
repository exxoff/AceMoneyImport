using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceMoneyImport
{
    public class ImportItem
    {

        public string InputFile { get; set; }
        public string OutFile { get; set; }

        //private string _inputFile;
        //private string _outFile;



        //public string OutFile
        //{
        //    get { return _outFile; }
        //    set
        //    {
        //        if(value != null)
        //        {
        //            _outFile = value;

        //        }

        //    }
        //}

        //public string InputFile
        //{
        //    get { return _inputFile; }
        //    set
        //    {
        //        if(value != null)
        //        {
        //            _inputFile = value;

        //            if(OutFile==null)
        //            {

        //                string _fileName = Path.GetFileNameWithoutExtension(_inputFile);
        //                OutFile = string.Format("{0}\\{1}.csv",Path.GetDirectoryName(_inputFile).ToString(),_fileName);
        //            }




        //        }
        //    }

        //}





    }
}
