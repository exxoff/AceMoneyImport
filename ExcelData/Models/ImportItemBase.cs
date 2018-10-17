using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelData.Helpers;
using ExcelData.Interfaces;
using NPOI.SS.UserModel;

namespace ExcelData.Models
{
    public abstract class ImportItemBase:IImportItem
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        
        public DataTable ExcelTable { get; set; }
        public ISheet ExcelSheet { get; set; }

        protected int DateColumn { get; set; }
        protected int SpecificationColumn { get; set; }
        protected int AmountColumn { get; set; }

        private ICsvFileWriter _writer;

        public ICsvFileWriter Writer
        {
            get
            {
                if (_writer == null)
                {
                    _writer = new CsvFileWriter();
                }
                return _writer;
            }
            set { _writer = value; }
        }

        //protected const int DateColumn = 0;
        //private const int SpecificationColumn = 2;
        //private const int AmountColumn = 6;

        public ImportItemBase()
        {
                
        }
        public ReturnObject ConvertToCsv()
        {
            if (InputFile == null)
            { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Input file cannot be null" }; };
            if (OutputFile == null)
            { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Output file cannot be null" }; };


            try
            {
                if (ExcelSheet == null)
                {
                    //ExcelSheet = GetExcelSheet(new FileStream(this.InputFile, FileMode.Open, FileAccess.Read));
                    ExcelSheet = GetExcelSheet(InputFile);

                }

                if (ExcelTable == null)
                {
                    ExcelTable = GetDataFromExcel(ExcelSheet);
                }


                Writer.WriteAsync(ExcelTable, this.OutputFile);
                //WriteToCsvFileAsync(excelTable);
                //GetDataFromExcel().WriteToCsvFileAsync(OutputFile);

                ExcelTable.Dispose();

                return new ReturnObject() { ErrorNumber = 0, ErrorMessage = string.Empty };

            }
            catch (Exception ex)
            {

                return new ReturnObject() { ErrorNumber = 1, ErrorMessage = ex.Message };
            }

        }

        private ISheet GetExcelSheet(FileStream source)
        {
            var book = WorkbookFactory.Create(source);


            return book.GetSheetAt(0);
        }

        private ISheet GetExcelSheet(string fileName)
        {
            ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = Encoding.Default.CodePage;

            var book = WorkbookFactory.Create(fileName);

            

            return book.GetSheetAt(0);
        }

        private DataTable GetDataFromExcel(ISheet excelSheet)
        {
            DataTable excelTable = new DataTable();

            excelTable.Columns.Add("Transaktion");
            excelTable.Columns.Add("Datum");
            excelTable.Columns.Add("Specifikation");
            excelTable.Columns.Add("Kategori");
            excelTable.Columns.Add("Status");
            excelTable.Columns.Add("Belopp");
            excelTable.Columns.Add("Insättning");
            excelTable.Columns.Add("Total");
            excelTable.Columns.Add("Kommentar");



            for (int i = 0; i < excelSheet.LastRowNum + 1; i++)
            {
                try
                {
                    DateTime dt;
                    //if (excelSheet.GetRow(i).GetCell(DateColumn).CellType == CellType.Numeric)
                    if(DateTime.TryParse(excelSheet.GetRow(i).GetCell(DateColumn).GetValue(),out dt))
                    {
                        //var cellDate = excelSheet.GetRow(i).GetCell(DateColumn).DateCellValue;
                        var cellDate = dt;

                        string spec = excelSheet.GetRow(i).GetCell(SpecificationColumn).StringCellValue;
                        Tuple<string, string> amounts =
                            ConvertAmounts(excelSheet.GetRow(i).GetCell(AmountColumn).GetValue());
                        string amount = amounts.Item1;
                        string deposit = amounts.Item2;
                        spec = spec.FormatText();

                        //if (amount.StartsWith("-"))
                        //{
                        //    deposit = amount;
                        //    amount = null;
                        //    deposit = deposit.Trim('-');
                        //}

                        excelTable.Rows.Add(null,
                            cellDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture),
                            spec,
                            null,
                            null,
                            amount,
                            deposit,
                            null,
                            null);
                    }
                }
                catch (InvalidDataException)
                {
                    // It's expected
                }
                catch (NullReferenceException)
                {

                }
            }

            return excelTable;

        }

        protected virtual Tuple<string, string> ConvertAmounts(string input)
        {
            Tuple<string, string> returnObject;
            if (input.StartsWith("-"))
            {
                returnObject = Tuple.Create("",input.Trim('-'));
            }
            else
            {
                returnObject = Tuple.Create(input,"");
            }

            return returnObject;
        }
    }
}
