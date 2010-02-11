using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    class ProcessStart
    {


    public ProcessStart()

    {


    }

    public void Scia_Start(string[] args)
    {
        Process Scia = new Process();

        Scia.StartInfo.FileName = "C:\\Program Files\\SCIA\\Engineer2009.0\\Esa.exe";
        Scia.StartInfo.Arguments = "ProcessStart.cs";

        Scia.Start();
    }

        
        }
}





    

