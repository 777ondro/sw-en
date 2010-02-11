using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        public DataSet getProcesses()
        {
            /*
            This method is created by Anton Zamov.
            web site: http://zamov.online.fr

            Feel free to use and redistribute this method
            in condition that you keep this message intact.
            */

            Process[] procs;
            TimeSpan cputime;

            procs = Process.GetProcesses();

            DataSet myDataSet = new DataSet("myDataSet");

            DataTable tProc = new DataTable("nc");

            // name, pid, time, mem, peakmem, handles, threads;

            DataColumn pName = new DataColumn("name", typeof(string));
            DataColumn pPid = new DataColumn("pid", typeof(string));
            DataColumn pTime = new DataColumn("time", typeof(string));
            DataColumn pMem = new DataColumn("mem", typeof(string));
            DataColumn pPeakmem = new DataColumn("peakmem", typeof(string));
            DataColumn pHandles = new DataColumn("handles", typeof(string));
            DataColumn pThreads = new DataColumn("threads", typeof(string));



            tProc.Columns.Add(pName);
            tProc.Columns.Add(pPid);
            tProc.Columns.Add(pTime);
            tProc.Columns.Add(pMem);
            tProc.Columns.Add(pPeakmem);
            tProc.Columns.Add(pHandles);
            tProc.Columns.Add(pThreads);


            myDataSet.Tables.Add(tProc);


            string name, pid, time, mem, peakmem, handles, threads;

            DataRow newRow2;

            foreach (Process proc in procs)
            {


                proc.Refresh();

                cputime = proc.TotalProcessorTime;

                name = proc.ProcessName;

                pid = proc.Id.ToString();

                time = String.Format(
                      "{0}:{1}:{2}",
                      ((cputime.TotalHours - 1 < 0 ? 0 : cputime.TotalHours - 1)).ToString("##0"),
                      cputime.Minutes.ToString("00"),
                      cputime.Seconds.ToString("00")
                      );


                mem = (proc.WorkingSet64 / 1024).ToString() + "k";

                peakmem = (proc.PeakWorkingSet64 / 1024).ToString() + "k";

                handles = proc.HandleCount.ToString();

                threads = proc.Threads.Count.ToString();

                newRow2 = tProc.NewRow();

                newRow2["name"] = name;
                newRow2["pid"] = pid;
                newRow2["time"] = time;
                newRow2["mem"] = mem;
                newRow2["peakmem"] = peakmem;
                newRow2["handles"] = handles;
                newRow2["threads"] = threads;

                tProc.Rows.Add(newRow2);
                proc.Close();
            }

            procs = null;

            return myDataSet;
        } 
    }
}
