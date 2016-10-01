using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NPOI.SS.UserModel;
using ExcelData;
using ExcelData.Helpers;
using ExcelData.Interfaces;
using ExcelData.Models;
using NPOI.HSSF.UserModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Test_ExcelData
{
    [TestFixture]
    public class ExcelDataTests
    {

        [Test]
        public void Get_string_value_from_cell()
        {
            //var mockCell = new Mock<ExcelHelper>();
            ////mockCell.Setup(x => x.SetCellValue("TestValue"));

            //ICell cell = mockCell.Object;
            //cell.SetCellValue(77);

            //Assert.AreEqual("TestValue",cell.GetValue());

        }

        [Test]
        public void ConvertToCsvReturnsOK()
        {
            
            
            var mockWriter = new Mock<ICsvFileWriter>();
            var table = new DataTable();
            IImportItem item = new ImportItem(table, mockWriter.Object);
            item.InputFile = string.Empty;
            item.OutputFile = string.Empty;

            ReturnObject ret = item.ConvertToCsv();

            mockWriter.Object.WriteAsync(table, "hhh");
            mockWriter.Verify(x => x.WriteAsync(table, "hhh"), Times.Exactly(1));
            NUnit.Framework.Assert.AreEqual(0, ret.ErrorNumber);
            NUnit.Framework.Assert.AreEqual("", ret.ErrorMessage);



        }

        [Test]
        public void ConvertToCsv_Returns_a_ReturnObject()
        {
            //Arrange


        }

        [Test]
        public void CreateAndReturnExcelSheetTest()
        {
            //Arrange: Create a workbook with headers + 2 rows
            ISheet sheet = SetupExcelSheet();

            //IImportItem item = new ImportItem();

            PrivateObject item = new PrivateObject(typeof(ImportItem));

            //Act

            DataTable table = (DataTable)item.Invoke("GetDataFromExcel", new object[] { sheet });

            //Assert

            NUnit.Framework.Assert.AreEqual(2, table.Rows.Count);

        }

        private static ISheet SetupExcelSheet()
        {
            IWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Test sheet");
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Date");
            row.CreateCell(1).SetCellValue("Date 2");
            row.CreateCell(2).SetCellValue("Specification");
            row.CreateCell(3).SetCellValue("City");
            row.CreateCell(4).SetCellValue("Currency");
            row.CreateCell(5).SetCellValue("Foreign amount");
            row.CreateCell(6).SetCellValue("Amount");
            row = sheet.CreateRow(1);
            row.CreateCell(0).SetCellValue("Text row");
            row = sheet.CreateRow(2);
            row.CreateCell(0).SetCellValue(new DateTime(2016, 5, 31));
            row.CreateCell(1).SetCellValue(new DateTime(2016, 5, 31));
            row.CreateCell(2).SetCellValue("ICA");
            row.CreateCell(3).SetCellValue("Gothenburg");
            row.CreateCell(4).SetCellValue("");
            row.CreateCell(5).SetCellValue("");
            row.CreateCell(6).SetCellValue(595);
            row = sheet.CreateRow(3);
            row.CreateCell(0).SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(1).SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(2).SetCellValue("Coop");
            row.CreateCell(3).SetCellValue("Gothenburg");
            row.CreateCell(4).SetCellValue("");
            row.CreateCell(5).SetCellValue("");
            row.CreateCell(6).SetCellValue(-30);
            row = sheet.CreateRow(4);
            row.CreateCell(0).SetCellValue("Text row");
            return sheet;
        }

        [Test]
        public void ConvertToCsv_InputFile_GuardClause_Returns_Error()
        {

            IImportItem item = new ImportItem() { OutputFile = "teststring" };

            ReturnObject ret = item.ConvertToCsv();

            NUnit.Framework.Assert.AreEqual("Input file cannot be null", ret.ErrorMessage);
        }

        [Test]
        public void ConvertToCsv_OuputFile_GuardClause_Returns_Error()
        {

            IImportItem item = new ImportItem() { InputFile = "teststring" };

            ReturnObject ret = item.ConvertToCsv();

            NUnit.Framework.Assert.AreEqual("Output file cannot be null", ret.ErrorMessage);
        }
    }
}
