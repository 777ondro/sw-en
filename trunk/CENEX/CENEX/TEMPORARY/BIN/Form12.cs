
/*This program lists all of the current processes on your machine.
  
*and gives you the option to terminate them
 *
 * 
 * 
 * You've before compiling this code to adjust namespace names or create the windows application with Process_Manager name
second to add in the form ListView control with name lvwProcesses, TextBox control with name txtMachine and Button control with name cmdRefersh and give in click event handler this method cmdRefersh_Click
  
*/



using System;

using System.Drawing;

using System.Collections;

using System.ComponentModel;

using System.Windows.Forms;

using System.Data;

using Microsoft.Win32;

using System.IO;
using System.Management;

using System.Text;

using System.Diagnostics;

using System.Resources;
using System.Threading;
  
      //^ Headers



namespace WindowsFormsApplication1
  
      {
  
      public partial class Form12 : Form
  
      {
          string selected_process_name;

          public string Selected_process_name
          {
              get { return selected_process_name; }
              set { selected_process_name = value; }
          }

          Process selected_process;

          public Process Selected_process
          {
              get { return selected_process; }
              set { selected_process = value; }
          }



 
      public Form12()
 
      {
  
      InitializeComponent();
  
      Process myProcess = new Process();
 
      //Declare process

      txtMachine.Text = System.Environment.MachineName.ToString();//set the machine name to textbox

     // My auxiliary textbox information
      txtUserDomainName.Text = System.Environment.UserDomainName.ToString();//set the UserDomainName to textbox
      txtUserInteractive.Text = System.Environment.UserInteractive.ToString();//set the UserInteractive to textbox
      txtUserName.Text = System.Environment.UserName.ToString();//set the UserName to textbox
      txtVersion.Text = System.Environment.Version.ToString();//set the Version to textbox
      txtWorkingSet.Text = System.Environment.WorkingSet.ToString();//set the WorkingSet to textbox


      txtCurrentDirectory.Text = System.Environment.CurrentDirectory.ToString();//set the CurrentDirectory to textbox
      txtExitCode.Text = System.Environment.ExitCode.ToString();//set the ExitCode to textbox
      txtHasShutdownStarted.Text = System.Environment.HasShutdownStarted.ToString();//set the HasShutdownStarted to textbox
      txtOSVersion.Text = System.Environment.OSVersion.ToString();//set the OSVersion to textbox
      txtProcessorCount.Text = System.Environment.ProcessorCount.ToString();//set the ProcessorCount to textbox
      txtStackTrace.Text = System.Environment.StackTrace.ToString();//set the CStackTrace to textbox
      txtSystemDirectory.Text = System.Environment.SystemDirectory.ToString();//set the SystemDirectory to textbox
      txtTickCount.Text = System.Environment.TickCount.ToString();//set the TickCount to textbox


      this.FillList(txtMachine.Text); // fill listview of processes

      // selected proces details
      // Main_proc();

      }
  
       
  
      private void FillList(string MachineName)
  
      {
 
      Process[] Prc;
  
      ListViewItem lvwP;
  
       
  
      Cursor.Current = Cursors.WaitCursor;
  
      //Set cusrsor as wait
  
       
  
      try
 
      {
  
      lvwProcesses.Items.Clear();//Clear the items
 
      Prc = Process.GetProcesses(MachineName.ToString());//Get the process's of our machine
 
       
  
      foreach (Process Prcs in Prc)
  
      {
  
      lvwP = lvwProcesses.Items.Add(Prcs.ProcessName.ToUpper());
  
      if (MachineName != System.Environment.MachineName)
  
      {
 
      //try to add
  
      lvwP.SubItems.Add("Unavailable...");
 
      lvwP.SubItems.Add("Unavailable...");
 
      lvwP.SubItems.Add(Prcs.Id.ToString());
  
      }
  
      else
  
      {
  
      //if failed try this
  
      lvwP.SubItems.Add(Prcs.MainWindowTitle);
  
      lvwP.SubItems.Add(Prcs.Responding.ToString());
 
      lvwP.SubItems.Add(Prcs.Id.ToString());
 
      }
  
      }
 
      }
  
      catch
 
      {
  
      lvwProcesses.Items.Add("Error enumerating items...");//error
 
      }
  
       
  
      Cursor.Current = Cursors.Default;//defualt cursor
  
      }
  
       
  
      private void Form12_Load(object sender, EventArgs e)
  
      {
  
      FillList(txtMachine.Text);//fill list
  
      }
  
       
  
      

      private void cmdRefersh_Click(object sender, EventArgs e)
      {
          FillList(txtMachine.Text);//refresh
      }


      public void selected_process_name_find()
      {
          
          // METHOD:    int selected_process = lvwProcesses.SelectedIndices[0];


          // string item = listBox1.SelectedIndex.ToString();  
          // if selectionMode is MultipleSimple or MultipleExtended you can use this to get a collection of the selected items.
          // string items = listBox1.SelectedItems;

          /// METHOD
          /*
          int index = 0;
          if (this.lvwProcesses.SelectedItems.Count > 0)
              index = this.lvwProcesses.SelectedIndices[0];
          */

          /// METHOD Specimen
          // ListViewItem.Selected=true;
          // listView1.SelectedItems[0].SubItems[2].Text;
          /// METHOD
          for (int i = 0; i < lvwProcesses.Items.Count; i++)
              // is i the index of the row I selected?
              if (lvwProcesses.Items[i].Selected == true)
              {
                  //I show here the second field text (SubItems[0].Text) from the selected row(Items[i])
                  string selected_process_name = lvwProcesses.Items[i].SubItems[0].Text;

                  MessageBox.Show("Selected process name is " + selected_process_name);

                  break;
              }

          // METHOD :      yourListView.SelectedItems[row_Index].SubItems[col_index].Text
          // if you only want the first selected item, then replace row_index with 0.
          // single selection mode listview will most likly to use 0 as the row_index

          /* METHOD
          int row_Index = index;
          int col_Index = 0;

          string selected_process_name = lvwProcesses.SelectedItems[row_Index].SubItems[col_Index].Text;

          MessageBox.Show(" Selected process name is" + selected_process_name, "Selected process control message");
           */

      }

      






      private void selected_process_details (string selected_process_name)
{
    
    Process[] selected_process;
    selected_process = Process.GetProcessesByName(selected_process_name);
    

    
   // Main method
    this.Main_proc();


}

private void selected_process_remove ()
    {
        if (lvwProcesses.SelectedItems.Count > 0)
            lvwProcesses.SelectedItems[0].Remove();
    }






private void tabControl1_TabIndexChanged(object sender, System.EventArgs e)
{
this.tabControl1.SelectedIndex = 2;
this.tabControl1.TabPages[2].Parent.Focus(); // Select is also
this.selected_process_details(selected_process_name);
}


private void lvwProcesses_SelectedIndexChanged(object sender, EventArgs e)
{
    
    if (lvwProcesses.CheckedItems.Count > 1)
        {
        MessageBox.Show(" Select just one process please ", "Message header - just one process is allowed");
        foreach (ListViewItem listItem in lvwProcesses.Items)
        listItem.Checked = false;
        }
    else
    {
        this.selected_process_name_find();
        
        this.selected_process_details(selected_process_name);
    }
    
    
    


}
      
        public void Main_proc()
        {

            // Define variables to track the peak
            // memory usage of the process.
            long peakPagedMem = 0,
                 peakWorkingSet = 0,
                 peakVirtualMem = 0;

            //Process selected_process = null;

            for (int i = 0; i < lvwProcesses.Items.Count; i++)
                // is i the index of the row I selected?
                if (lvwProcesses.Items[i].Selected == true)
                {
                    //I show here the second field text (SubItems[0].Text) from the selected row(Items[i])
                    selected_process_name = lvwProcesses.Items[i].SubItems[0].Text;

                    break;
                }

            


            Process[] aProc = Process.GetProcessesByName(selected_process_name);


            if (aProc.Length > 0) 
              {  
                Process selected_process = aProc[0];
                MessageBox.Show("Selected process name is " + selected_process.ProcessName.ToString());




                int selected_item_listview_ID = lvwProcesses.SelectedIndices[0];
                int selected_process_ID;
                selected_process_ID = selected_process.Id;
                MessageBox.Show("Process2 ID is " + selected_process_ID.ToString());
                Process selected_process_2 = Process.GetProcessById(selected_process_ID);
                selected_process_ID = selected_process.Id;

                MessageBox.Show("Process name is " + selected_process.ProcessName.ToString());
                MessageBox.Show("Process2 name is " + selected_process_2.ProcessName.ToString());
              }
            else
            {
                MessageBox.Show("Process name is " + selected_process_name);
                MessageBox.Show("Process name is " + selected_process.ProcessName.ToString());

                MessageBox.Show ("Unknow process");
            };


            try
            {
                // Start the process.
                // selected_process = Process.Start("NotePad.exe");
                

                // Display the process statistics until
                // the user closes the program.
                do
                {
                    //if (!selected_process.HasExited)
                    {
                        // Refresh the current process property values.
                        //selected_process.Refresh();

                        

                        // Display current process statistics.
                        

                        MessageBox.Show("Process name is " + selected_process.ProcessName.ToString());
                        string m1 = selected_process.PagedSystemMemorySize64.ToString();
                        string m2 = selected_process.NonpagedSystemMemorySize64.ToString();                      
                        string m3 = selected_process.PagedMemorySize64.ToString();
                        string m4 = selected_process.PeakVirtualMemorySize64.ToString();
                        string m5 = selected_process.PeakWorkingSet64.ToString();                     
                        string m6 = selected_process.PeakPagedMemorySize64.ToString();
                        string m7 = selected_process.PrivateMemorySize64.ToString ();
                        string m8 = selected_process.VirtualMemorySize64.ToString();
                        string m9 = selected_process.WorkingSet64.ToString();

                        MessageBox.Show(("Memory data " + "\n" +

                        "PagedSystemMemorySize64 " + m1 + "\n" +
                        "NonpagedSystemMemorySize64 " + m2 + "\n" +
                        "PagedMemorySize64 " + m3 + "\n" +
                        "PeakVirtualMemorySize64 " + m4 + "\n" +
                        "PeakWorkingSet64 " + m5 + "\n" +
                        "PeakPagedMemorySize64 " + m6 + "\n" +
                        "PrivateMemorySize64 " + m7 + "\n" +
                        "VirtualMemorySize64 " + m8 + "\n" +
                        "WorkingSet64 " + m9), " MEMORY "
                          );


                        string p1 = selected_process.TotalProcessorTime.ToString();
                        string p2 = selected_process.UserProcessorTime.ToString();
                        string p3 = selected_process.PrivilegedProcessorTime.ToString();
                        string p4 = selected_process.Id.ToString();
                        string p5 = selected_process.PriorityClass.ToString();
                        string p6 = selected_process.MachineName.ToString();
                        string p7 = selected_process.ProcessorAffinity.ToString();
                        string p8 = selected_process.ProcessName.ToString();
                        string p9 = selected_process.MainModule.ToString();

                        MessageBox.Show(("Processor data " + "\n" +

                        "TotalProcessorTime " + p1 + "\n" +
                        "UserProcessorTime " + p2 + "\n" +
                        "PrivilegedProcessorTime " + p3 + "\n" +
                        "Id " + p4 + "\n" +
                        "PriorityClass " + p5 + "\n" +
                        "MachineName " + p6 + "\n" +
                        "ProcessorAffinity " + p7 + "\n" +
                        "ProcessName " + p8 + "\n" +
                        "MainModule " + p9), " PROCESS and PROCESSOR "
                          );

                        
                      
                        









                        label_a1.Text = " Process name: ";
                        label_a2.Text = selected_process.ProcessName.ToString();
                        MessageBox.Show(label_a2.Text);
                        label_b1.Text = " Physical memory usage: ";
                        label_b2.Text = selected_process.WorkingSet64.ToString ();
                        label_c1.Text = " Base priority:";
                        label_c2.Text = selected_process.BasePriority.ToString ();
                        label_d1.Text = " Priority class:";
                        label_d2.Text = selected_process.PriorityClass.ToString ();
                        label_e1.Text = " User processor time:";
                        label_e2.Text = selected_process.UserProcessorTime.ToString ();
                        label_f1.Text = " Privileged processor time:";
                        label_f2.Text = selected_process.PrivilegedProcessorTime.ToString ();
                        label_g1.Text = " Total processor time:";
                        label_g2.Text = selected_process.TotalProcessorTime.ToString ();
                        label_h1.Text = " PagedSystemMemorySize64:";
                        label_h2.Text = selected_process.PagedSystemMemorySize64.ToString ();
                        label_i1.Text = " PagedMemorySize64:";
                        label_i2.Text = selected_process.PagedMemorySize64.ToString ();

                        // Update the values for the overall peak memory statistics.
                        peakPagedMem = selected_process.PeakPagedMemorySize64;
                        peakVirtualMem = selected_process.PeakVirtualMemorySize64;
                        peakWorkingSet = selected_process.PeakWorkingSet64;

                        // Control if process is running
                        /*
                        if (selected_process.Responding)
                        {
                            Console.WriteLine("Status = Running");
                        }
                        else
                        {
                            Console.WriteLine("Status = Not Responding");
                        }
                         */

                    }
                }
                while (!selected_process.WaitForExit(1000));


                Console.WriteLine();
                Console.WriteLine("Process exit code: {0}",selected_process.ExitCode);

                // Display peak memory statistics for the process.
                Console.WriteLine("Peak physical memory usage of the process: {0}",
                    peakWorkingSet);
                Console.WriteLine("Peak paged memory usage of the process: {0}",
                    peakPagedMem);
                Console.WriteLine("Peak virtual memory usage of the process: {0}",
                    peakVirtualMem);

            }
            finally
            {
                /* close process
                if (selected_process != null)
                {
                    selected_process.Close();
                }
                */
            }
        }
      }
    }


  
        
       
 
      /* This could be easily expanded to sstart process's to.
  
  
      *Enjoy!
  
      */