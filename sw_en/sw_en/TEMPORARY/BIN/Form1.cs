using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace WindowsFormsApplication1
{
  public partial class Form1 : Form
  {

    string martintext;
    public string Martintext
    {
      get { return martintext; }
      set { martintext = value; }
    }
    string processname;

    public string Processname
    {
        get { return processname; }
        set { processname = value; }
    }
    public Form1()
    {
      InitializeComponent();

      martintext = textBox1.Text.ToString();
      martintext = martintext.ToString();
      
      
      
      
      
      
      InitializeTreeView();
      Form1_Load();
      LoadProcess_combo();
      

    }
    public void button1_Click(object sender, EventArgs e)
    {
      Class2 objekt2 = new Class2();
      objekt2.method1();

      double _NEd = 1000;
      double _Aeff = 20;


      objekt2.check1(_NEd, _Aeff);

    }
    private void button2_Click(object sender, EventArgs e)
    {
      Class1 objekt1 = new Class1();
      objekt1.method1();

    }
    private void toolStripMenuItem9_Click(object sender, EventArgs e)
    {
        

        

        /*
      Form2 objektForm2 = new Form2();
      objektForm2.Show();

      //OtherForm newForm = new OtherForm();
      //newForm.Show();
      richTextBox1.Show();

      MessageBox.Show(" Hiding Form2. System turn off. ");

      objektForm2.Hide();
      objektForm2.Activate();
      objektForm2.Focus();
         * */




    }
    
    public void runClass2()
    {

      Class2 objekt2 = new Class2();

      double _My_Ed = 100;
      double _Wy = 105402;
      objekt2.check2(_My_Ed, _Wy);

    }
    public void Close_applicationForm1(bool close)
    {
      // Form 1 close
      close = true;
      if (close == true)
        this.Close();
      MessageBox.Show(" System exit point. Whole data will be lost. \n \n This is not a total failure, we are just playing.\n \n DEBUG IS FINISHED! ");
      Application.Exit();
    }
    private void menu1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Form3 objektForm3 = new Form3();

    }
    private void form3CloseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      object o = Form3.ActiveForm;
      string name = o.ToString();
      if (name == "Form3")
        Hide();
      Application.Restart();
      //Application.ExitThread();


      MessageBox.Show(" Internal error. Application was restarted. Application is loaded. ");

      // Application.Run ();
      // Application.RunDialog (Form1);
      // Form.ShowDialog (Form1) ;
      // ShowDialog();

    }
    public static void Restart() { }
    private void button3_Click(object sender, EventArgs e)
    {
      string textVOkne = "Chcete skutocne spustit tuto aplikaciu?";
      string textVTitulku = "Acrobat Reader";

      DialogResult dr = MessageBox.Show(textVOkne, textVTitulku,
         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
         MessageBoxDefaultButton.Button1);

      switch (dr)
      {
          case DialogResult.Yes:
              MessageBox.Show("V poriadku, pokračujeme", textVTitulku);
              try
              { 
                  System.Diagnostics.Process.Start(@"C:\Program Files\Adobe\Reader 8.0\Reader\AcroRd32.exe");
              }

              catch
              {
                  try
                  { // alternatívna cesta
                      System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Adobe\Acrobat 9.0\Acrobat\Acrobat.exe");
                  }
                  catch
                  {
                      MessageBox.Show("Adobe Acrobat nie je nainštalovaný alebo nie je možné násjsť cestu k programu", "EXCEPION");
                  };
              }
              finally
              {
                  // tato cast kodu (blok) sa vykona vzdy
              };

              break;
          case DialogResult.No:
              MessageBox.Show("Nevadi, snáď nabudúce", textVTitulku);
              break;
          default:
              MessageBox.Show("Tak si to poriadne rozmysli!!", textVTitulku);
          break;
      }

      //System.Diagnostics.Process.Start(@"C:\Program Files\Adobe\Reader 8.0\Reader\AcroRd32.exe");
    }
    private void TreeNodeMouseClickEventArgs(object sender, EventArgs e)
    {

    }
    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      TreeNode clickedNode = e.Node;
    }
    private void form3ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {

      object o = Form3.ActiveForm;
      Hide();

    }
      // Populates a TreeView control with example nodes. 
private void InitializeTreeView()
    {
        treeView1.BeginUpdate();

        // Node1.Expand();
        // Node1.Collapse();

        // Level 0
        treeView1.Nodes.Add("Main0 - Level 0");
        treeView1.Nodes.Add("Main1 - Level 0");
        treeView1.Nodes.Add("Main2 - Level 0");
        treeView1.Nodes.Add("Main3 - Level 0");

        // Level 1
        treeView1.Nodes[0].Nodes.Add("Node 01 Level 1");
        treeView1.Nodes[0].Nodes.Add("Node 02 Level 1");
        treeView1.Nodes[0].Nodes.Add("Node 03 Level 1");
        treeView1.Nodes[0].Nodes.Add("Node 04 Level 1");
        treeView1.Nodes[1].Nodes.Add("Node 11 Level 1");
        treeView1.Nodes[1].Nodes.Add("Node 12 Level 1");
        treeView1.Nodes[1].Nodes.Add("Node 13 Level 1");
        treeView1.Nodes[1].Nodes.Add("Node 14 Level 1");
        treeView1.Nodes[3].Nodes.Add("Node 31 Level 1");
        treeView1.Nodes[3].Nodes.Add("Node 32 Level 1");
        treeView1.Nodes[3].Nodes.Add("Node 33 Level 1");
        treeView1.Nodes[3].Nodes.Add("Node 34 Level 1");

        // Level 2
        treeView1.Nodes[0].Nodes[0].Nodes.Add("Node 000 Level 2");
        treeView1.Nodes[0].Nodes[1].Nodes.Add("Node 010 Level 2");
        treeView1.Nodes[1].Nodes[1].Nodes.Add("Node 110 Level 2");
        treeView1.Nodes[1].Nodes[2].Nodes.Add("Node 121 Level 2");
        treeView1.Nodes[3].Nodes[0].Nodes.Add("Node 300 Level 2");
        // Level 3
        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add("Node 0000 Level 3");
        treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Node 0100 Level 3");
        treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Node 0101 Level 3");
        treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Node 0102 Level 3");
        treeView1.EndUpdate();
    }

private void showToolStripMenuItem_Click(object sender, EventArgs e)
{
    Form2 objektForm2 = new Form2();
    objektForm2.Show();
}

private void showForm4ToolStripMenuItem_Click(object sender, EventArgs e)
{
    Form4 objektForm4 = new Form4();
    objektForm4.Show();
}

private void showForm6ToolStripMenuItem_Click(object sender, EventArgs e)
{

}

private void showForm6ToolStripMenuItem1_Click(object sender, EventArgs e)
{
    Form6 objektForm6 = new Form6();
    objektForm6.Show();
}

private void showForm5ToolStripMenuItem_Click(object sender, EventArgs e)
{
    Form5 objektForm5 = new Form5();
    objektForm5.Show();
}

private void cLOSEToolStripMenuItem_Click(object sender, EventArgs e)
{

    

}

public class TerminateProcessExample
{

    public static void Main_Terminate()
    {

        // Create a new Process and run notepad.exe.
        using (Process process = Process.Start("notepad.exe"))
        {

            // Wait for 5 seconds and terminate the notepad process.
            MessageBox.Show("Waiting 5 seconds before terminating" +
                " notepad.exe.");

            Thread.Sleep(5000);

            // Terminate notepad process.
            MessageBox.Show("Terminating Notepad with CloseMainWindow.");

            // Try to send a close message to the main window.
            if (!process.CloseMainWindow())
            {

                // Close message did not get sent - Kill Notepad.
                MessageBox.Show("CloseMainWindow returned false - " +
                    " terminating Notepad with Kill.");
                process.Kill();

            }
            else
            {

                // Close message sent successfully; wait for 2 seconds
                // for termination confirmation before resorting to Kill.
                // bool WaitForExit(int milliseconds);
                if (!process.WaitForExit(2000))
                {

                    MessageBox.Show("CloseMainWindow failed to" +
                        " terminate - terminating Notepad with Kill.");
                    process.Kill();
                }
            }
        }

        // Wait to continue.
        MessageBox.Show("Main method complete.");
        
    }
}

public void eXAMPLEToolStripMenuItem_Click(object sender, EventArgs e)
{
    TerminateProcessExample.Main_Terminate();
}

private void rESTARTToolStripMenuItem_Click(object sender, EventArgs e)
{
    Application.Restart();

}

private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
{

}

private void sCIAToolStripMenuItem_Click(object sender, EventArgs e)
{

    // 1
    ProcessStartInfo startInfo = new ProcessStartInfo("C:\\Program Files\\SCIA\\Engineer2009.0\\Esa.exe");
    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
    Process.Start(startInfo);

    // 2
    /*
    ProcessStart scia_start = new ProcessStart();
    string[] args = new string[] {"Esa.exe", "ProcessStart.cs"};
    scia_start.Scia_Start(args);
    */

    // C:\Program Files\SCIA\Engineer2009.0\Esa.exe
        

}

private void wwwdlubalczToolStripMenuItem_Click(object sender, EventArgs e)
{
    Class7.Main4();

}

private void sCIAToolStripMenuItem1_Click(object sender, EventArgs e)
{
    processname = "Esa.exe";
    Process_kill();
}
private void wwwdlToolStripMenuItem_Click(object sender, EventArgs e)
{
    processname = "IExplore.exe";
    Process_kill();
}
    // Get the process by its name 

public  Process GetaProcess(string processname)
{
Process[] aProc = Process.GetProcessesByName(processname);

if (aProc.Length > 0)
return aProc[0];

else return null;
}
// To Kill the process
private void Process_kill()
{
    try
    {
        Process[] aProc = Process.GetProcessesByName(processname);
        Process myprc = GetaProcess(processname);
        myprc.Exited += new EventHandler(sCIAToolStripMenuItem1_Click);
        myprc.Kill();
    }
    catch
    {
        MessageBox.Show("Unexpected error in process " + processname);
    }
    ;
}

public bool FindAndKillProcess(string processname)
{
    //here we're going to get a list of all running processes on
    //the computer
    foreach (Process clsProcess in Process.GetProcesses())
    {
        //now we're going to see if any of the running processes
        //match the currently running processes by using the StartsWith Method,
        //this prevents us from incluing the .EXE for the process we're looking for.
        //. Be sure to not
        //add the .exe to the name you provide, i.e: NOTEPAD,
        //not NOTEPAD.EXE or false is always returned even if
        //notepad is running
        if (clsProcess.ProcessName.StartsWith(processname))
        {
            //since we found the proccess we now need to use the
            //Kill Method to kill the process. Remember, if you have
            //the process running more than once, say IE open 4
            //times the loop thr way it is now will close all 4,
            //if you want it to just close the first one it finds
            //then add a return; after the Kill
            try
            {
                clsProcess.Kill(); //process killed
            }
            catch
            {
                MessageBox.Show("Unexpected error in process " + processname);  //exception
            }


            return true;// return true
        }
    }
    //process not found, return false
    return false;
}

public void LoadProcess_combo()
{

    Process[] processlist = Process.GetProcesses();

    foreach (Process theprocess in processlist)
    {
        Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
    

    //richTextBox1

    };

    // Get the list of processes
    Process[] p = Process.GetProcesses();

    // Convert that array to our array. We overload ToString() so that the
    // ListBox can actually do soemthing meaninfull.
    ProcessWrapper[] pw = Array.ConvertAll<Process, ProcessWrapper>
(p, new Converter<Process, ProcessWrapper>(ProcessToWrapper));

// Add in all our processes.
toolStripComboBox1.Items.AddRange(pw);
}

// This does the conversion
private ProcessWrapper ProcessToWrapper(Process p)
{
return new ProcessWrapper(p);
}

public class ProcessWrapper
{
private Process _process;
public ProcessWrapper(Process p)
{
_process = p;
}
public override string ToString()
{
return _process.ProcessName;
}
}

private void toolStripComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
{

    ToolStripComboBox toolStripComboBox1 = (ToolStripComboBox)sender; 

    string selected_process_name = toolStripComboBox1.SelectedItem.ToString();
    string selected_process_name2 = toolStripComboBox1.Text.ToString();

    MessageBox.Show((

        "selected_process_name is " + selected_process_name + "\n" +

        "selected_process_name2 is " + selected_process_name2), "Selected process name control"
            );

    string textWindow = "Do you really want to kill selected proces " + selected_process_name;
    string textHeader = "PROCESS KILL - CONFIRMATION WINDOW";

    DialogResult dr = MessageBox.Show(textWindow, textHeader,
       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1);

    switch (dr)
    {
        case DialogResult.Yes:
            try
            {
                this.FindAndKillProcess(selected_process_name); //process killed
            }
            catch
            {
                MessageBox.Show("Unexpected error in process " + selected_process_name);  //exception
            }
            
            break;
        case DialogResult.No:
            {
               // No Action, close items view

                
            }
            break;
        default:
            MessageBox.Show("Select another process from list", textHeader);
            break;
    }


        





        // SelectedIndexChanged event.
        // this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(toolStripComboBox1_SelectedIndexChanged);


    }



private void toolStripComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
{

    ComboBox toolStripComboBox1 = (ComboBox) sender;

    // Change the length of the text box depending on what the user has 
    // selected and committed using the SelectionLength property.
    if (toolStripComboBox1.SelectionLength > 0)
    {
    try
        {
            string processname = toolStripComboBox1.SelectedText;
            this.FindAndKillProcess(processname);
        }
        catch
        {
            MessageBox.Show("Unexpected error in process " + processname);
        }
    }


}

      private void showForm8ToolStripMenuItem_Click(object sender, EventArgs e)
      {
          Form8 objektForm8 = new Form8();
          objektForm8.Show();
      }

      private void showToolStripMenuItem2_Click(object sender, EventArgs e)
      {
          Form10 objektForm10 = new Form10();
          objektForm10.Show();
      }

      private void showToolStripMenuItem1_Click(object sender, EventArgs e)
      {
          Form9 objektForm9 = new Form9();
          objektForm9.Show();
      }

      private void showForm7ToolStripMenuItem_Click(object sender, EventArgs e)
      {
          Form7 objektForm7 = new Form7();
          objektForm7.Show();
      }
      private void showToolStripMenuItem3_Click(object sender, EventArgs e)
      {
          Form11 objektForm11 = new Form11();
          objektForm11.Show();

      }
         
           



      

    //Handle the Exit Event of the Process
    public void Handle_process()
    {
        
        Process myprc = GetaProcess(processname);
        myprc.Exited += new EventHandler(myprc_Exited);
    }
    public void myprc_Exited(object sender, EventArgs e)
    {
        MessageBox.Show(((Process)sender).ProcessName + " Process has exited!");
    }
// Exit current application or kill process
    private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
        Environment.Exit(0);
        Process.GetCurrentProcess().Kill();
        Thread.CurrentThread.Abort();
 

    }


      

      private void button4_Click(object sender, EventArgs e)
      {
      
// Create an OpenFileDialog object.
OpenFileDialog openFile1 = new OpenFileDialog();

// Initialize the filter to look for text files.
openFile1.Filter = "Text Files|*.txt";

// If the user selected a file, load its contents into the RichTextBox.
if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    richTextBox1.LoadFile(openFile1.FileName,RichTextBoxStreamType.PlainText);
      }

      private void button5_Click(object sender, EventArgs e)
      {






      }

      private void showToolStripMenuItem4_Click(object sender, EventArgs e)
      {
          Form12 objektForm12 = new Form12();
          objektForm12.Show();
      }

      private void showToolStripMenuItem5_Click(object sender, EventArgs e)
      {
          Form13 objektForm13 = new Form13();
          objektForm13.Show();
      }

      private void showToolStripMenuItem6_Click(object sender, EventArgs e)
      {
          Form14 objektForm14 = new Form14();
          objektForm14.Show();
      }

      private void showToolStripMenuItem7_Click(object sender, EventArgs e)
      {
          Form15 objektForm15 = new Form15();
          objektForm15.Show();
      }

      public void Form1_Load()
      {
  // Create the ToolTip and associate with the Form container.
  ToolTip toolTip1 = new ToolTip();

  // Set up the delays for the ToolTip.
  toolTip1.AutoPopDelay = 5000;
  toolTip1.InitialDelay = 1000;
  toolTip1.ReshowDelay = 500;
  // Force the ToolTip text to be displayed whether or not the form is active.
  toolTip1.ShowAlways = true;
     
  // Set up the ToolTip text for the Button and Checkbox.
  toolTip1.SetToolTip(this.button6, "My button6");
  // toolTip1.SetToolTip(this.checkBox1, "My checkBox1");

          maskedTextBox1.Mask = "00/00/0000";
          maskedTextBox1.ValidatingType = typeof(System.DateTime);
          maskedTextBox1.TypeValidationCompleted += new TypeValidationEventHandler(maskedTextBox1_TypeValidationCompleted);
          maskedTextBox1.KeyDown += new KeyEventHandler(maskedTextBox1_KeyDown);

          toolTip1.IsBalloon = true;

          // another code source - load masked text box datatype
          // Displaying design-time pop-up form for MaskedTextBox.Mask property
          Assembly asm = Assembly.Load("System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
          Type editor = asm.GetType("System.Windows.Forms.Design.MaskDesignerDialog");
          ConstructorInfo ci = editor.GetConstructor(new Type[] { typeof(MaskedTextBox), typeof(System.ComponentModel.Design.IHelpService) });
          Form dlg = ci.Invoke(new object[] { maskedTextBox1, null }) as Form;
          if (DialogResult.OK == dlg.ShowDialog(this))
          {
              PropertyInfo pi = editor.GetProperty("Mask");
              maskedTextBox1.Mask = pi.GetValue(dlg, null) as string;
          }


         
          
         




      }

      void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
      {
          // Create the ToolTip and associate with the Form container.
          ToolTip toolTip1 = new ToolTip();

          // Set up the delays for the ToolTip.
          toolTip1.AutoPopDelay = 1000;
          toolTip1.InitialDelay = 500;
          toolTip1.ReshowDelay = 200;
          // Force the ToolTip text to be displayed whether or not the form is active.
          toolTip1.ShowAlways = true;

          // Set up the ToolTip text for the Button.
          toolTip1.SetToolTip(this.button6, "My button6");

          if (!e.IsValidInput)
          {
              
              toolTip1.ToolTipTitle = "Invalid Date";
              toolTip1.Show("The data you supplied must be a valid date in the format mm/dd/yyyy.", maskedTextBox1, 0, -20, 5000);
          }
          else
          {
              //Now that the type has passed basic type validation, enforce more specific type rules.
              DateTime userDate = (DateTime)e.ReturnValue;
              if (userDate < DateTime.Now)
              {
                  toolTip1.ToolTipTitle = "Invalid Date";
                  toolTip1.Show("The date in this field must be greater than today's date.", maskedTextBox1, 0, -20, 5000);
                  e.Cancel = true;
              }
          }
      }

      // Hide the tooltip if the user starts typing again before the five-second display limit on the tooltip expires.
      void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
      {
          // Create the ToolTip and associate with the Form container.
          ToolTip toolTip1 = new ToolTip();
          toolTip1.Hide(maskedTextBox1);
      }

      private void button6_Click(object sender, EventArgs e)
      {
          // TIME



          string t;
          int years;
          int months;
          int days;

          int hours;
          int minutes;
          int seconds;
          
         

          DateTime dt = DateTime.Now;
          seconds = dt.Second;
          minutes = dt.Minute;
          hours = dt.Hour;

          days = dt.Day;
          months = dt.Month;
          years = dt.Year;

          DateTime Date = dt.Date;
          

          string longdate = dt.ToLongDateString();
          string longtime = dt.ToLongTimeString();
          string shortdate = dt.ToShortDateString();
          string shorttime = dt.ToShortTimeString();
          string generalDateandTime = dt.ToString();




          MessageBox.Show((

              "Year: " + years.ToString() + "\n" +
              "Month: " + months.ToString() + "\n" +
              "Day: " + days.ToString() + "\n" +
              "Hours: " + hours.ToString() + "\n" +
              "Minutes: " + minutes.ToString() + "\n" +
              "Seconds: " + seconds.ToString() + "\n" +

              "\n" +
              "\n" +
              "Long Date: " + longdate + "\n" +
              "Long Time: " + longtime + "\n" +
              "Short Date: " + shortdate + "\n" +
              "Short Time: " + shorttime + "\n" +
              "\n" +
              "General Date and Time: " + generalDateandTime.ToString () + "\n" +
              "\n" +
              "Date and Start Time: " + Date.ToString ()


              ),
              "DATE and TIME");

          maskedTextBox1.Clear ();
          maskedTextBox1.Text = generalDateandTime.ToString();

         


          // update time if seconds change 
          if (seconds != dt.Second)
          {
              seconds = dt.Second;

              t = dt.ToString("T");
              MessageBox.Show(t);
          } 
      }

      ///
      /// ENUMERATED
      /// 
      // pomocka, priame zadanie čísla pozície v enum FooSize { SMALL = 10, MEDIUM = 100, LARGE = 1000 };
      // tradičné zadanie v C, C++
      /*
      #define SPRING   0
      #define SUMMER   1
      #define FALL     2
      #define WINTER   3

      */



      /*
      // Add this line to your using list:

    
      using System.Diagnostics;

// Now you can get a list of the processes with the Process.GetProcesses() method, as seen in this example:

    Process[] processlist = Process.GetProcesses();

    foreach(Process theprocess in processlist){
    Console.WriteLine(”Process: {0} ID: {1}”, theprocess.ProcessName, theprocess.Id);
    }

// Some interesting properties of the Process object:

    p.StartTime // (Shows the time the process started)
    p.TotalProcessorTime // (Shows the amount of CPU time the process has taken)
    p.Threads // (gives access to the collection of threads in the process)
       */
       
      

























   
   

   

    
}
}













