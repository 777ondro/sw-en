using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Excel;

namespace SharedLibraries.EXPIMP
{
	public static class CImportFromExcel
	{
		public static DataSet ImportFromExcel(string fileName)
		{
			DataSet result = null;
			try
			{
				FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

				//1. Reading from a binary Excel file ('97-2003 format; *.xls)
				//IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				//...
				//2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
				IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				//...
				//3. DataSet - The result of each spreadsheet will be created in the result.Tables
				//DataSet result = excelReader.AsDataSet();
				//...
				//4. DataSet - Create column names from first row
				excelReader.IsFirstRowAsColumnNames = true;
				result = excelReader.AsDataSet();

				//5. Data Reader methods
				//while (excelReader.Read())
				//{
				//    //excelReader.GetInt32(0);
				//}

				//6. Free resources (IExcelDataReader is IDisposable)
				excelReader.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return result;
		}
	}
}
