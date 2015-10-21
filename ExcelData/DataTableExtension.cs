﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExcelData
{
    public static class DataTableExtensions
    {
        public static void WriteToCsvFile(this DataTable dataTable, string OuputFilePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append("\"" + col.ToString() + "\";");
            }

            fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in dataTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\";");
                }

                fileContent.Replace(";", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(OuputFilePath, fileContent.ToString(),Encoding.Unicode);

        }
    }
}

