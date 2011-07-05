using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Class2
    {
        public Class2()
        {

        }
        public void method1()
        {
            MessageBox.Show("Run system control");
            MessageBox.Show("System control is runing");

        }
        public void method2()
        {
            MessageBox.Show("Finishing - Los pescadores pescan peces en el mar. Nosotros somos ellos?");
        }
        
        public void check1(double _NEd, double _A)
        {

            double gamaM0 = 1;
            double fy = 355;

            double _Nt_Rd = fy * _A / gamaM0;

            double ratio1 = _NEd / _Nt_Rd;

            MessageBox.Show("Result is \n" + ratio1.ToString());

        }
        public void check2(double _My_Ed, double _Wy)
        {

            double gamaM0 = 1;
            double fy = 355;

            double _My_Rd = fy * _Wy / gamaM0;

            double ratio2 = _My_Ed / _My_Rd;

            MessageBox.Show("Result is \n" + ratio2.ToString());

        }











    }
}
