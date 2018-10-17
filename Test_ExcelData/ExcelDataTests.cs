using ExcelData.Helpers;
using ExcelData.Interfaces;
using ExcelData.Models;
using Moq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NUnit.Framework;
using System;
using System.Data;
using ExcelData;
using NPOI.XSSF.Streaming;

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
        public void GetValueFromCellTest()
        {
            ISheet sheet = SetupExcelSheet();

            ICell cell = sheet.GetRow(3).GetCell(0);

            var value = cell.GetValue();

            Assert.AreEqual("2016-12-31 00:00:00",value);

            var amountCell = sheet.GetRow(3).GetCell(6);

            Assert.AreEqual("-30,5",amountCell.GetValue());

        }

        [Test]
        public void ConvertToCsvReturnsOK()
        {
            
            
            var mockWriter = new Mock<ICsvFileWriter>();
            var table = new DataTable();
            IImportItem item = new EuroCardImportItem(mockWriter.Object,SetupExcelSheet());
            item.InputFile = string.Empty;
            item.OutputFile = string.Empty;

            ReturnObject ret = item.ConvertToCsv();

            Assert.AreEqual(0, ret.ErrorNumber);
            Assert.AreEqual("", ret.ErrorMessage);



        }

        [Test]
        public void ConvertSEBToCsvReturnsOK()
        {


            var mockWriter = new Mock<ICsvFileWriter>();
            var table = new DataTable();
            IImportItem item = new SebImportItem(mockWriter.Object,setupSebExcelSheet());
            item.InputFile = string.Empty;
            item.OutputFile = string.Empty;

            ReturnObject ret = item.ConvertToCsv();

            mockWriter.Object.WriteAsync(table, "hhh");
            mockWriter.Verify(x => x.WriteAsync(table, "hhh"), Times.Exactly(1));
            Assert.AreEqual(0, ret.ErrorNumber);
            Assert.AreEqual("", ret.ErrorMessage);



        }

        [Test]
        public void ConvertToCsv_Returns_a_ReturnObject()
        {
            //Arrange


        }

        [Test]
        public void CreateAndReturnSebExcelSheetTest()
        {
            //Arrange: Create a workbook with headers + 2 rows
            ISheet sheet = setupSebExcelSheet();
            var mockWriter = new Mock<ICsvFileWriter>();
            
            IImportItem item = new SebImportItem(mockWriter.Object,sheet);

            item.InputFile = string.Empty;
            item.OutputFile = string.Empty;

            //Act

            item.ConvertToCsv();
            

            //Assert

            Assert.AreEqual(2, item.ExcelTable.Rows.Count);

        }

        [Test]
        public void CreateAndReturnEurocardExcelSheetTest()
        {
            //Arrange: Create a workbook with headers + 2 rows
            ISheet sheet = SetupExcelSheet();
            var mockWriter = new Mock<ICsvFileWriter>();
            var table = new DataTable();
            IImportItem item = new EuroCardImportItem(mockWriter.Object, sheet);

            item.InputFile = string.Empty;
            item.OutputFile = string.Empty;

            //Act

            item.ConvertToCsv();


            //Assert

            Assert.AreEqual(2, item.ExcelTable.Rows.Count);

        }

        private static ISheet SetupExcelSheet()
        {
            IWorkbook book = new HSSFWorkbook();

            IDataFormat format = book.CreateDataFormat();

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
            ICell cell = row.CreateCell(0);
            cell.CellStyle.DataFormat = format.GetFormat("dd-MM");
            cell.SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(1).SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(2).SetCellValue("Coop");
            row.CreateCell(3).SetCellValue("Gothenburg");
            row.CreateCell(4).SetCellValue("");
            row.CreateCell(5).SetCellValue("");
            row.CreateCell(6).SetCellValue(-30.50);
            row = sheet.CreateRow(4);
            row.CreateCell(0).SetCellValue("Text row");
            return sheet;
        }

        private ISheet setupSebExcelSheet()
        {
            IWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Test sheet");
            IRow row = sheet.CreateRow(0);

            row.CreateCell(0).SetCellValue("Date");
            row.CreateCell(1).SetCellValue("Date 2");
            row.CreateCell(2).SetCellValue("Verification number");
            row.CreateCell(3).SetCellValue("Specification");
            row.CreateCell(4).SetCellValue("Amount");
            row.CreateCell(5).SetCellValue("Saldo");
            row = sheet.CreateRow(1);
            row.CreateCell(0).SetCellValue("Text row");
            row = sheet.CreateRow(2);
            row.CreateCell(0).SetCellValue(new DateTime(2016, 5, 31));
            row.CreateCell(1).SetCellValue(new DateTime(2016, 5, 31));
            row.CreateCell(2).SetCellValue("76779838");
            row.CreateCell(3).SetCellValue("Dressmann");
            row.CreateCell(4).SetCellValue(595);
            row.CreateCell(5).SetCellValue(11111);
            row = sheet.CreateRow(3);
            row.CreateCell(0).SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(1).SetCellValue(new DateTime(2016, 12, 31));
            row.CreateCell(2).SetCellValue("934876");
            row.CreateCell(3).SetCellValue("ICA");
            row.CreateCell(4).SetCellValue(-30);
            row.CreateCell(5).SetCellValue(11111);
            row = sheet.CreateRow(4);
            row.CreateCell(0).SetCellValue("Text row");
            return sheet;

        }


        [Test]
        public void ConvertToCsv_InputFile_GuardClause_Returns_Error()
        {
            var mockWriter = new Mock<ICsvFileWriter>();

            IImportItem item = new EuroCardImportItem(mockWriter.Object,SetupExcelSheet()) { OutputFile = "teststring" };

            ReturnObject ret = item.ConvertToCsv();

            Assert.AreEqual("Input file cannot be null", ret.ErrorMessage);
        }

        [Test]
        public void ConvertToCsv_OuputFile_GuardClause_Returns_Error()
        {
            var mockWriter = new Mock<ICsvFileWriter>();
            IImportItem item = new EuroCardImportItem(mockWriter.Object, SetupExcelSheet()) { InputFile = "teststring" };

            ReturnObject ret = item.ConvertToCsv();

            Assert.AreEqual("Output file cannot be null", ret.ErrorMessage);
        }
    }
}
