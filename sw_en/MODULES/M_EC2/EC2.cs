using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace M_EC2
{
    class EC2
    {

   // Variables

        double d_b;

        public double D_b
        {
            get { return d_b; }
            set { d_b = value; }
        }
        double d_h;

        public double D_h
        {
            get { return d_h; }
            set { d_h = value; }
        }
        double d_A;

        public double D_A
        {
            get { return d_A; }
            set { d_A = value; }
        }
        double d_I_y;

        public double D_I_y
        {
            get { return d_I_y; }
            set { d_I_y = value; }
        }
        double d_I_z;

        public double D_I_z
        {
            get { return d_I_z; }
            set { d_I_z = value; }
        }







        // Konstruktor
        // Sluzi na preberanie premennych z inych tried
        public EC2(double a,double b)

        {
            // priradenie hodnot premennym zavolanim vlastnosti premennej v inej triede
            this.d_b = a;
            this.d_h = b;
            
            //urobime vypocet
            this.EC2_1();

       }

        //////////////////////////////////////////////////////////////
        // HLAVNY VYPOCET
        //////////////////////////////////////////////////////////////
        
        public void EC2_1()

       {
           this.d_A = d_b * d_h;
           this.d_I_y = d_b/12* Math.Pow(d_h, 3);
           this.d_I_z = d_h/12 * Math.Pow(d_b, 3);
           MessageBox.Show("Vysledky v EC2 \n" + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));
            
        }

        public void EC2_2_TAH()
        {
            // NAPR. SEM MOZES PISAT VYPOCET
            // Asi bude najlepsie vytvorit samostatnu metodu pre kazde posudenie (TAH, TLAK, OHYB,  VZPER, OHYB+VZPER, ....)


        }

        public void EC2_3_TLAK()
        {
            // NAPR. SEM MOZES PISAT VYPOCET
            // Asi bude najlepsie vytvorit samostatnu metodu pre kazde posudenie (TAH, TLAK, OHYB,  VZPER, OHYB+VZPER, ....)


        }
        public void EC2_4_OHYB()
        {
            // NAPR. SEM MOZES PISAT VYPOCET
            // Asi bude najlepsie vytvorit samostatnu metodu pre kazde posudenie (TAH, TLAK, OHYB,  VZPER, OHYB+VZPER, ....)

            // Jednotlive metody byte som pomenoval podla nejako podla čísel článkov ale nesmu tam byt bodky "."
        }




    }
}
