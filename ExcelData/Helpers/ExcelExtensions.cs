using NPOI.SS.UserModel;

namespace ExcelData.Helpers
{
    public static class ExcelExtensions
    {


        public static string GetValue(this ICell currentCell)
        {
            string retVal = string.Empty;

            switch(currentCell.CellType)
            {
                case CellType.Numeric:
                {

                    retVal =  DateUtil.IsCellDateFormatted(currentCell)
                        ? currentCell.DateCellValue.ToString()
                        : currentCell.NumericCellValue.ToString();

                        break;
                    }
                case CellType.String:
                    {
                        retVal = currentCell.StringCellValue;
                        break;
                    }
                    
            }

            return retVal;
            
        }
    }
}
