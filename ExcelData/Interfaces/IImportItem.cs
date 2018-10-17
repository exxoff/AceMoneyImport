using ExcelData.Helpers;
using System.Data;
using NPOI.SS.UserModel;

namespace ExcelData.Interfaces
{
    public interface IImportItem
    {
        string InputFile { get; set; }
        string OutputFile { get; set; }

        ICsvFileWriter Writer { get; set; }
        DataTable ExcelTable { get; set; }
        ISheet ExcelSheet { get; }

        ReturnObject ConvertToCsv();

        

    }
}
