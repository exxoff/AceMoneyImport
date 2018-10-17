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
    public class SebImportItem : ImportItemBase
    {

        //private const int COLUMN_DATE = 0;
        //private const int COLUMN_SPECIFICATION = 3;
        //private const int COLUMN_AMOUNT = 4;




        public SebImportItem()
        {
            SetColumns();
        }



        public SebImportItem(ICsvFileWriter writer, ISheet excelSheet)
        {
            Writer = writer;
            this.ExcelSheet = excelSheet;
            SetColumns();
        }

        private void SetColumns()
        {
            DateColumn = 0;
            SpecificationColumn = 3;
            AmountColumn = 4;
        }

        protected override Tuple<string, string> ConvertAmounts(string input)
        {
            Tuple<string, string> returnObject;
            if (input.StartsWith("-"))
            {
                returnObject = Tuple.Create(input.Trim('-'), "");
            }
            else
            {
                returnObject = Tuple.Create("", input);
            }

            return returnObject;
        }
    }
}
