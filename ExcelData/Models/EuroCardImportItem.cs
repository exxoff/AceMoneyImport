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
    public class EuroCardImportItem : ImportItemBase
    {


        

 


        public EuroCardImportItem()
        {
            SetColumns();
        }

        public EuroCardImportItem(ICsvFileWriter writer,ISheet excelSheet)
        {
            Writer = writer;
            ExcelSheet = excelSheet;

            SetColumns();

        }

        private void SetColumns()
        {
            DateColumn = 0;
            SpecificationColumn = 2;
            AmountColumn = 6;
        }







    }
}
