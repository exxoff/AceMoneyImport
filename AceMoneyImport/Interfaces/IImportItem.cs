using AceMoneyImport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceMoneyImport.Interfaces
{
    public interface IImportItem
    {
        string InputFile { get; set; }
        string OutputFile { get; set; }

        ReturnObject ConvertToCsv();
    }
}
