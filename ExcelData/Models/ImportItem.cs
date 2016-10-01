using ExcelData.Helpers;
using ExcelData.Interfaces;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData.Models
{
    public class ImportItem : IImportItem
    {

        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        //public ICsvFileWriter Writer { get; set; }

        private ICsvFileWriter writer;

        public ICsvFileWriter Writer
        {
            get
            {
                if(writer == null)
                {
                    writer = new CsvFileWriter();
                }
                return writer;
            }
            set { writer = value; }
        }

        public DataTable ExcelTable { get; set; }


        public ImportItem()
        {
            
        }

        public ImportItem(DataTable excelTable,ICsvFileWriter writer)
        {
            this.Writer = writer;
            this.ExcelTable = excelTable;
        }
        public ReturnObject ConvertToCsv()
        {
            if (InputFile == null) { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Input file cannot be null" }; };
            if (OutputFile == null) { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Output file cannot be null" }; };


            try
            {
                if (ExcelTable == null)
                {
                    ExcelTable = GetDataFromExcel(GetExcelSheet(new FileStream(this.InputFile, FileMode.Open, FileAccess.Read)));
                }


                Writer.WriteAsync(ExcelTable, this.OutputFile);
                //WriteToCsvFileAsync(excelTable);
                //GetDataFromExcel().WriteToCsvFileAsync(OutputFile);

                return new ReturnObject() { ErrorNumber = 0, ErrorMessage = string.Empty };

            }
            catch (Exception ex)
            {

                return new ReturnObject() { ErrorNumber = 1, ErrorMessage = ex.Message };
            }


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

                    DateTime _cellDate = DateTime.Now;
                    if (excelSheet.GetRow(i).GetCell(0).CellType == CellType.Numeric)
                    {
                        _cellDate = excelSheet.GetRow(i).GetCell(0).DateCellValue;


                        string _spec = excelSheet.GetRow(i).GetCell(2).StringCellValue;
                        string _amount = excelSheet.GetRow(i).GetCell(6).GetValue();
                        string _deposit = null;
                        _spec = _spec.FormatText();

                        if (_amount.StartsWith("-"))
                        {
                            _deposit = _amount;
                            _amount = null;
                            _deposit = _deposit.Trim('-');
                        }

                        excelTable.Rows.Add(null,
                            _cellDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture),
                            _spec,
                            null,
                            null,
                            _amount,
                            _deposit,
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

        private ISheet GetExcelSheet(FileStream source)
        {
            IWorkbook book;


            book = WorkbookFactory.Create(source);


            ISheet sheet = book.GetSheetAt(0);
            return sheet;
        }

        //private async void WriteToCsvFileAsync(DataTable excelTable)
        //{
        //    StringBuilder fileContent = new StringBuilder();

        //    foreach (var col in excelTable.Columns)
        //    {
        //        fileContent.Append("\"" + col.ToString() + "\";");
        //    }

        //    fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);



        //    foreach (DataRow dr in excelTable.Rows)
        //    {

        //        foreach (var column in dr.ItemArray)
        //        {
        //            fileContent.Append("\"" + column.ToString() + "\";");
        //        }

        //        fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
        //    }

        //    await Task.Run(() => System.IO.File.WriteAllText(OutputFile, fileContent.ToString(), Encoding.Unicode));

        //}

    }
}
