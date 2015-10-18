using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel;
using System.IO;
using System.Data;
using CSharpJExcel;
using NPOI;
using NPOI.SS.UserModel;

namespace ExcelData
{
    public class ExcelHelper
    {


        public static DataTable GetData(string FileName)
        {
            DataTable _excelTable = new DataTable();

            _excelTable.Columns.Add("Transaktion");
            _excelTable.Columns.Add("Datum");
            _excelTable.Columns.Add("Specifikation");
            _excelTable.Columns.Add("Kategori");
            _excelTable.Columns.Add("Status");
            _excelTable.Columns.Add("Belopp");
            _excelTable.Columns.Add("Insättning");
            _excelTable.Columns.Add("Total");
            _excelTable.Columns.Add("Kommentar");



            IWorkbook book;
            using (var stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                book = WorkbookFactory.Create(stream);
            }

            ISheet sheet = book.GetSheetAt(0);

            for(int i=0;i< sheet.LastRowNum;i++)
            {
                try
                { 

                    DateTime _cellDate = sheet.GetRow(i).GetCell(0).DateCellValue;
                    string _spec = sheet.GetRow(i).GetCell(2).StringCellValue;
                    string _amount = sheet.GetRow(i).GetCell(6).NumericCellValue.ToString();
                    string _deposit = null;
                    _spec = _spec.FormatText();

                    if(_amount.StartsWith("-"))
                    {
                        _deposit = _amount;
                        _amount = null;
                        _deposit = _deposit.Trim('-');
                    }

                    _excelTable.Rows.Add(null,
                        _cellDate.ToString("yyyy/MM/dd",System.Globalization.CultureInfo.InvariantCulture),
                        _spec,
                        null,
                        null,
                        _amount,
                        _deposit,
                        null,
                        null);
                }
                catch (InvalidDataException)
                {
                    // It's expected
                }
                catch (NullReferenceException)
                {

                }
            }
            

            

            
            return _excelTable;
            
        }
    }
}
