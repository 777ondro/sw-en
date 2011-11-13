using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Drawing;

namespace SharedLibraries.EXPIMP
{
	public static class CExportToExcel
	{
		public static void ExportToExcel(this DataSet myDataSet, string fileName, string worksheetName)
		{
			if (myDataSet == null)
			{
				throw new ArgumentNullException("myDataSet");
			}

			var excel = new Application();

			try
			{
				Workbook book;

				if (File.Exists(fileName))
				{
					book = excel.Workbooks.Open(fileName);
				}
				else
				{
					book = excel.Workbooks.Add();
				}

				dynamic sheet = book.Sheets.Add();

				sheet.Name = worksheetName;

				for (int i = 0; i < myDataSet.Tables[0].Columns.Count; i++)
				{
					sheet.Cells[1, i + 1] = myDataSet.Tables[0].Columns[i].ColumnName;
					sheet.Cells[1, i + 1].Font.Bold = true;
					sheet.Cells[1, i + 1].Interior.Color = ColorTranslator.ToOle(Color.Gray);
				}

				for (int row = 0; row < myDataSet.Tables[0].Rows.Count; row++)
				{
					for (int column = 0; column < myDataSet.Tables[0].Columns.Count; column++)
					{
						sheet.Cells[row + 2, column + 1] =
							myDataSet.Tables[0].Rows[row][column].ToString();
					}
				}

				for (int i = 0; i < myDataSet.Tables[0].Columns.Count; i++)
				{
					sheet.Columns[i + 1].AutoFit();
				}

				book.SaveAs(fileName, AccessMode: XlSaveAsAccessMode.xlShared);
				book.Close();
			}
			finally
			{
				excel.Workbooks.Close();
				excel.Quit();
			}
		}
		
	}
}
