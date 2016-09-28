using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData.Interfaces
{
    public interface ICsvFileWriter
    {
        void WriteAsync(DataTable data, string outputFile);
    }
}
