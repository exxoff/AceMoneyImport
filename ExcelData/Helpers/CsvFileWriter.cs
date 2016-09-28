using ExcelData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExcelData.Helpers
{
    public class CsvFileWriter : ICsvFileWriter
    {
        public async void WriteAsync(DataTable data, string outputFile)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in data.Columns)
            {
                fileContent.Append("\"" + col.ToString() + "\";");
            }

            fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in data.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\";");
                }

                fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            await Task.Run(() => System.IO.File.WriteAllText(outputFile, fileContent.ToString(), Encoding.Unicode));

        }
    }
}
