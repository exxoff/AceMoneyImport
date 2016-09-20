using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData
{
    public static class DataTableExtensions
    {
        public static async void WriteToCsvFileAsync(this DataTable dataTable, string OuputFilePath)
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

            await Task.Run(() => System.IO.File.WriteAllText(OuputFilePath, fileContent.ToString(),Encoding.Unicode));

        }
    }
}

