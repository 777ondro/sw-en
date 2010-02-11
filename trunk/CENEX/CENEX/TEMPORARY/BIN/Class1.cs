using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Class1
    {

        public Class1 ()
{
    

}
        public void method1 ()
        {

            
            MessageBox.Show("Show system message.");

            Class2 objekt2 = new Class2 ();
            objekt2.method1 ();
            objekt2.method2 ();
            Class3 objekt3 = new Class3();
            objekt3.arrays_specimen();
           
        }


    }
}
