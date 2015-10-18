using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExcelData;
using System.Data;
using System.IO;

namespace Test_ExcelData
{
    [TestClass]
    public class UnitTest1
    {
  

        [TestMethod]
        public void Test_ExcelDate()
        {
            //Arrange

            string fileName = @"C:\Users\Per\Downloads\Fakturadetaljer_MittEurocard.xls";
            string _outFile = @"C:\Temp\This.csv";

            // Act

            DataTable _data = ExcelHelper.GetData(fileName);
            
            _data.WriteToCsvFile(_outFile);

            //Assert

            Assert.IsTrue(File.Exists(_outFile));
            //File.Delete(_outFile);

        }
    }
}
