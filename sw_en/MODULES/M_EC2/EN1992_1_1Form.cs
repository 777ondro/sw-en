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


       // Concrete

        public float  m_fGamma_Mc = 1.5f;

        private float m_fLambda = 0.8f;

        public float FLambda
        {
            get { return m_fLambda; }
            set { m_fLambda = value; }
        }

       // Reinforcement

        private float m_ff_yk;

        public float Ff_yk
        {
            get { return m_ff_yk; }
            set { m_ff_yk = value; }
        }

        private float m_ff_uk;

        public float Ff_uk
        {
            get { return m_ff_uk; }
            set { m_ff_uk = value; }
        }

        public float m_fGamma_Ms = 1.1f;





        // Cross - section 

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

        public float m_fM_Ed = 10000000f;

        public float m_fM_Rd;
        public float m_fDesRatio;



        MatTemp m_objDatabase = new MatTemp(); // Objet databazy Concrete a Reinforcement

        public EN1992_1_1Form()
        {
            InitializeComponent();


        }
        
        // Metoda ktora sa spusti po stlaceni tlacidla calculate
        private void Calculate_Click(object sender, EventArgs e)
        {
            // Načítanie dat
            this.Load_data();

            // Vypocet vstupov 
            // Lokalne ktore nevstupuju pramo do objektu vypoctu

            float fA_s_1 = MathF.fPI * MathF.Pow2(Fd_s)/ 4; // Plocha jedneho pruta
            FA_s = iNo * fA_s_1;


            // Vypocet vysledkov
            // Vytvori sa objekt triedy vypoctu
            EC2 objekt_EC2 = new EC2(fb,fh, fA_s, m_objDatabase.m_ff_ck, m_fLambda, m_fGamma_Mc, m_ff_yk, m_fGamma_Ms, m_fM_Ed);

            m_fM_Rd = objekt_EC2.FM_Rd1;
            m_fDesRatio = objekt_EC2.FDesRatio1;
 
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


            // Concrete

            Value_f_ck.Text = m_objDatabase.m_ff_ck.ToString();
            Value_f_ck_cube.Text = m_objDatabase.m_ff_ck_cube.ToString();
            Value_f_cm.Text = m_objDatabase.m_ff_cm.ToString();
            Value_f_ctm.Text = m_objDatabase.m_ff_ctm.ToString();

            // Reinforcement
            Value_As.Text = FA_s.ToString();


            // Results
            Value_M_Rd.Text = m_fM_Rd.ToString();
            Value_DesRatio.Text = m_fDesRatio.ToString(); 

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fd_s = m_objDatabase.Get_Reinf_d_s((short)comboBox4.SelectedIndex);
        }

        private void ComboBox_Rein3_SelectedIndexChanged(object sender, EventArgs e)
        {
            INo = ComboBox_Rein3.SelectedIndex;
            INo++; //Number of bars
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_objDatabase.m_ff_ck = m_objDatabase.Get_f_ck((short)comboBox1.SelectedIndex);
            m_objDatabase.m_ff_ck_cube = m_objDatabase.Get_f_ck_cube((short)comboBox1.SelectedIndex);
           
            m_objDatabase.GetConData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Not implemented yet
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_ff_yk = m_objDatabase.Get_Reinf_f_yk((short)comboBox3.SelectedIndex);
            m_ff_uk = m_objDatabase.Get_Reinf_f_tk((short)comboBox3.SelectedIndex);
        }





    }
}
