using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace M_EC2
{
    public partial class EN1992_1_1Form : Form
    {


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






        public EN1992_1_1Form()
        {
            InitializeComponent();







        }
        
        // Metoda ktora sa spusti po stlaceni tlacidla calculate
        private void Calculate_Click(object sender, EventArgs e)
        {
            // Načítanie dat
            this.Load_data();
            // Vypocet vysledkov
            // Vytvori sa objekt triedy
            EC2 objekt_EC2 = new EC2(this.D_b,this.D_h);
            
            this.D_A =   objekt_EC2.D_A;
            this.D_I_y = objekt_EC2.D_I_y;
            this.D_I_z = objekt_EC2.D_I_z;
            MessageBox.Show("Vysledky v EN 1992_1_1 Form \n " + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));

            // zapísanie výsledkov do READONLY textboxov
            this.Set_data();


        }
        // Metoda - Load data from textboxes
        // Tato metoda nacita udaje z textboxov a skonvertuje na cislo
        public void Load_data()
        {

            try
            {
                d_b = Convert.ToDouble(d_b_textB.Text.ToString());
            }
            catch
            {
                MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
            }

            try
            {
                d_h = Convert.ToDouble(d_h_textB.Text.ToString());
            }
            catch
            {
                MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
            }

        }






        // Metoda - Nastaví vypocitane hodnoty v textboxoch
        public void Set_data ()

        {
                     
            // Nastavia sa vypocitane hodnoty (skonvetovane z double na string)

            d_A_textB.Text =  D_A.ToString();
            d_Iy_textB.Text = D_I_y.ToString();
            d_Iz_textB.Text = D_I_z.ToString();

        }





    }
}
