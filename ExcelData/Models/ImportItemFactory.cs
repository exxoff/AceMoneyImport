using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelData.Interfaces;

namespace ExcelData.Models
{
    public static class ImportItemFactory
    {
        public static IImportItem GetImportItem(string itemType)
        {
            IImportItem item = null;

            switch (itemType.ToUpper())
            {
                case "EUROCARD":
                    item=new EuroCardImportItem();
                    break;

                case "SEB":
                    item=new SebImportItem();
                    break;

                default:
                    
                    break;
            }

            return item;
        }
    }
}
