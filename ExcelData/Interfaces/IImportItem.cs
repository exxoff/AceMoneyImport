using ExcelData.Helpers;
using System.Data;

namespace ExcelData.Interfaces
{
    public interface IImportItem
    {
        string InputFile { get; set; }
        string OutputFile { get; set; }

        ICsvFileWriter Writer { get; set; }
        DataTable ExcelTable { get; set; }

        ReturnObject ConvertToCsv();

        

    }
}
