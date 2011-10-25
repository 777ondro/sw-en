using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace SharedLibraries.EXPIMP
{
	public class CExportToExcel
	{
		string fileName;
        DataSet ds;
		public CExportToExcel(string filename, DataSet ds)
        {
            this.fileName = filename;
            this.ds = ds;
        }

        public void writeToExcel()
        {

            System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection(
        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName +
        ";Extended Properties=Excel 8.0;");
            try
            {
                objConn.Open();
            }
            catch (OleDbException) { return; }


            System.Data.OleDb.OleDbCommand objCmd = new System.Data.OleDb.OleDbCommand();
            objCmd.Connection = objConn;

            try
            {
                objCmd.CommandText = "CREATE TABLE kurz ( Premenna char(40),Hodnota char(20), " +
                        "Jednotka char(20), Premenna1 char(40), Hodnota1 char(20),Jednotka1 char(20), Premenna2 char(40), " +
                        "Hodnota2 char(20),Jednotka2 char(20) )"; ;
                objCmd.ExecuteNonQuery();
            }
            catch (OleDbException e) { /*MessageBox.Show(e.Message);*/ }

            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    objCmd.CommandText = @"INSERT INTO [kurz$] VALUES ('" + r.ItemArray[0].ToString() + "','"
                        + r.ItemArray[1].ToString() + "','" + r.ItemArray[2].ToString() + "','" + r.ItemArray[3].ToString() + "','"
                        + r.ItemArray[4].ToString() + "','" + r.ItemArray[5].ToString() + "','" + r.ItemArray[6].ToString() + "','"
                        + r.ItemArray[7].ToString() + "','" + r.ItemArray[8].ToString() + "')";
                    objCmd.ExecuteNonQuery();
                }
            }
            catch (OleDbException e) { /*MessageBox.Show(e.Message); */}
            

            // Close the connection.
            objConn.Close();

        }
	}
}
