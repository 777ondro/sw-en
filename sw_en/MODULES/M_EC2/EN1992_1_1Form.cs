using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using M_BASE.Concrete;
using MATH;

namespace M_EC2
{
    public partial class EN1992_1_1Form : Form
    {


        float fb;

        public float Fb
        {
            get { return fb; }
            set { fb = value; }
        }
        float fh;

        public float Fh
        {
            get { return fh; }
            set { fh = value; }
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

        private float fA_s;

        public float FA_s
        {
            get { return fA_s; }
            set { fA_s = value; }
        }

        private float fA_s_1;

        public float FA_s_1
        {
            get { return fA_s_1; }
            set { fA_s_1 = value; }
        }

        private int iNo;

        public int INo
        {
            get { return iNo; }
            set { iNo = value; }
        }

        private float fd_s;

        public float Fd_s
        {
            get { return fd_s; }
            set { fd_s = value; }
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
            // Lokalne ktore nevstupuju pramo do objektu vypoctu


            float fA_s_1 = MathF.fPI * MathF.Pow2(Fd_s)/ 4; // Plocha jedneho pruta
            FA_s = iNo * FA_s_1;


            // Vytvori sa objekt triedy vypoctu
            EC2 objekt_EC2 = new EC2(this.Fb,this.Fh);
            
            // Naplnia sa premenne po vypocte
            this.D_A =   objekt_EC2.D_A;
            this.D_I_y = objekt_EC2.D_I_y;
            this.D_I_z = objekt_EC2.D_I_z;

            this.FA_s = objekt_EC2.FA_s;
            
            
            
            //MessageBox.Show("Vysledky v EN 1992_1_1 Form \n " + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));

            // zapísanie výsledkov do READONLY textboxov
            this.Set_data();


        }
        // Metoda - Load data from textboxes
        // Tato metoda nacita udaje z textboxov a skonvertuje na cislo
        public void Load_data()
        {

            try
            {
                fb = (float)Convert.ToDouble(d_b_textB.Text.ToString());
            }
            catch
            {
                MessageBox.Show("FORMAT ERROR", "Wrong numerical format! Enter real number, please.");
            }

            try
            {
                fh = (float)Convert.ToDouble(d_h_textB.Text.ToString());
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



            Value_As.Text = FA_s.ToString();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Diameter_Index = comboBox4.SelectedIndex;

            MatTemp objTemp = new MatTemp();
            //objTemp.Get_Reinf_f_yk

        }

        private void ComboBox_Rein3_SelectedIndexChanged(object sender, EventArgs e)
        {
            INo = ComboBox_Rein3.SelectedIndex;
            INo++; //Number of bars
        }





    }
}
