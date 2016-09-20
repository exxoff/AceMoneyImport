using AceMoneyImport.Helpers;
using AceMoneyImport.Interfaces;
using ExcelData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceMoneyImport.Models
{
    public class ImportItem:IImportItem
    {

        public string InputFile { get; set; }
        public string OutputFile { get; set; }



        public  ReturnObject ConvertToCsv()
        {
            if (InputFile == null) { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Input file cannot be null" }; };
            if (OutputFile == null) { return new ReturnObject() { ErrorNumber = 1, ErrorMessage = "Output file cannot be null" }; };

            try
                {
                    DataTable _dataFromExcel = ExcelHelper.GetData(InputFile);
                    _dataFromExcel.WriteToCsvFileAsync(OutputFile);
                    return new ReturnObject() { ErrorNumber = 0, ErrorMessage = string.Empty };

                }
                catch (Exception ex)
                {

                    return new ReturnObject() { ErrorNumber = 1, ErrorMessage = ex.Message };
                }
            

        }
    }
}
