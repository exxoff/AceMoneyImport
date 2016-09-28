using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;

namespace ExcelData
{
    public static class ExcelExtensions
    {


        public static string GetValue(this ICell CurrentCell)
        {
            string _retVal = string.Empty;

            switch(CurrentCell.CellType)
            {
                case CellType.Numeric:
                    {
                        _retVal = CurrentCell.NumericCellValue.ToString();

                        break;
                    }
                case CellType.String:
                    {
                        _retVal = CurrentCell.StringCellValue;
                        break;
                    }
                    
            }

            return _retVal;
            
        }
    }
}
