﻿using ExcelData;
using NUnit.Framework;


namespace Test_ExcelData
{
    [TestFixture]
    public class MiscTests  
    {
  

        //[TestMethod]
        //public void Test_ExcelDate()
        //{
        //    //Arrange

        //    string fileName = @"C:\Users\Per\Downloads\Fakturadetaljer_MittEurocard.xls";
        //    string _outFile = @"C:\Temp\This.csv";

        //    // Act

        //    DataTable _data = ExcelHelper.GetData(fileName,new DataTable());
            
        //    _data.WriteToCsvFileAsync(_outFile);

        //    //Assert

        //    Assert.IsTrue(File.Exists(_outFile));
        //    //File.Delete(_outFile);

        //}

        [Test]
        public void StringExtensionFormatTextReturnsString()
        {
            //Arrange
            string input = "$";
            
            //Act



            //Assert

            Assert.AreEqual("Å", StringExtensions.FormatText(input));
        }
    }
}
