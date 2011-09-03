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
        // Internal Forces

        public float m_fN_Ed;
        public float m_fM_Ed_1_y;
        public float m_fM_Ed_1_z;
        public float m_fM_Ed_1;

        public float m_fN_0_Ed_qp;
        public float m_fM_0_Ed_qp_y;
        public float m_fM_0_Ed_qp_z;
        public float m_fM_0_Ed_qp;

        public float m_fM_0_1_y;
        public float m_fM_0_1_z;
        public float m_fM_0_2_y;
        public float m_fM_0_2_z;

        public float m_fe_Sd_y;
        public float m_fe_Sd_z;

        // Concrete

        public float  m_fGamma_Mc = 1.5f;

        private float m_fLambda = 0.8f;

        public float FLambda
        {
            get { return m_fLambda; }
            set { m_fLambda = value; }
        }

        public float m_fAlpha_cc;
        public float m_fEta;


       // Reinforcement
       // Material
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

        private float m_fE_s;

        public float FE_s
        {
            get { return m_fE_s; }
            set { m_fE_s = value; }
        }

        public float m_fGamma_Ms = 1.15f;

        // Reinforcement  -  Geometry and properties
        // Longitudinal


        // Shear / Transversal
        private float m_fd_s_s;

        public float Fd_s_s
        {
            get { return m_fd_s_s; }
            set { m_fd_s_s = value; }
        }
        private float m_fA_s_s;

        public float FA_s_s
        {
            get { return m_fA_s_s; }
            set { m_fA_s_s = value; }
        }
        private float m_fs_s;

        public float Fs_s
        {
            get { return m_fs_s; }
            set { m_fs_s = value; }
        }



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

        float m_fA;

        public float FA
        {
            get { return m_fA; }
            set { m_fA = value; }
        }
        float m_fI_y;

        public float FI_y
        {
            get { return m_fI_y; }
            set { m_fI_y = value; }
        }
        float m_fI_z;

        public float FI_z
        {
            get { return m_fI_z; }
            set { m_fI_z = value; }
        }

        private float m_fA_s_t;

        public float FA_s_t
        {
            get { return m_fA_s_t; }
            set { m_fA_s_t = value; }
        }

        private float m_fA_s_1;

        public float FA_s_1
        {
            get { return m_fA_s_1; }
            set { m_fA_s_1 = value; }
        }

        float m_fad;

        public float Fad
        {
            get { return m_fad; }
            set { m_fad = value; }
        }
        float m_fbc;

        public float Fbc
        {
            get { return m_fbc; }
            set { m_fbc = value; }
        }
        float m_fa_s_t_y;

        public float Fa_s_t_y
        {
            get { return m_fa_s_t_y; }
            set { m_fa_s_t_y = value; }
        }

        float m_fa_s_t_z;

        public float Fa_s_t_z
        {
            get { return m_fa_s_t_z; }
            set { m_fa_s_t_z = value; }
        }

        float m_ft_b;

        public float Ft_b
        {
            get { return m_ft_b; }
            set { m_ft_b = value; }
        }


        private int m_iNo;

        public int INo
        {
            get { return m_iNo; }
            set { m_iNo = value; }
        }

        float m_fA_c;

        public float FA_c
        {
            get { return m_fA_c; }
            set { m_fA_c = value; }
        }

        private float m_fd_s;

        public float Fd_s
        {
            get { return m_fd_s; }
            set { m_fd_s = value; }
        }

        float m_fu;

        public float Fu
        {
            get { return m_fu; }
            set { m_fu = value; }
        }

        float m_fI_s_y;

        public float FI_s_y
        {
            get { return m_fI_s_y; }
            set { m_fI_s_y = value; }
        }
        float m_fI_s_z;

        public float FI_s_z
        {
            get { return m_fI_s_z; }
            set { m_fI_s_z = value; }
        }

        float m_fi_s_y;

        public float Fi_s_y
        {
            get { return m_fi_s_y; }
            set { m_fi_s_y = value; }
        }

        float m_fi_s_z;

        public float Fi_s_z
        {
            get { return m_fi_s_z; }
            set { m_fi_s_z = value; }
        }


        // Member Geometry

        public float m_fL;
        public float m_fBeta_y;
        public float m_fBeta_z;

        public float m_fL_0_y;
        public float m_fL_0_z;


        // Settings and Auxiliary Values for Buckling

        public float m_fn_bal;
        public float m_ft_0;
        public float m_ft;
        public float m_fRH;
        public float m_fT_Delta_t_i;
        public float m_fAlpha;
        public float m_fc_y;
        public float m_fc_z;







        // Results

        public float m_fPhi_ef_y;
        public float m_fPhi_ef_z;

        public float m_fN_Rd;
        public float m_fM_Rd_y;
        public float m_fM_Rd_z;

        public float m_fDesRatio1;
        public float m_fDesRatio2;
        public float m_fDesRatio;

        MatTemp m_objDatabase = new MatTemp(); // Objet databazy Concrete a Reinforcement

        public EN1992_1_1Form()
        {
            InitializeComponent();

            // Text and Default Values
            // Temporary
            TestCode_data();
            // Display Deafult Values
            Set_DefaultInput_Data();
        }
        
        // Metoda ktora sa spusti po stlaceni tlacidla calculate
        private void Calculate_Click(object sender, EventArgs e)
        {
            // Načítanie dat
            Load_data();

            // Vypocet vstupov 
            // Premenne ktore je mozne spocitat pred hlavnym vypoctom
            // Sive policka

            // Resulting IF
            m_fM_Ed_1 =MathF.Sqrt(MathF.Pow2(m_fM_Ed_1_y) + MathF.Pow2(m_fM_Ed_1_z));
            //m_fM_Ed_1 = (float)Math.Sqrt(Math.Pow(m_fM_Ed_1_y,2) + Math.Pow(m_fM_Ed_1_z,2));
            m_fM_0_Ed_qp = MathF.Sqrt(MathF.Pow2(m_fM_0_Ed_qp_y) + MathF.Pow2(m_fM_0_Ed_qp_z));
            //m_fM_0_Ed_qp = (float)Math.Sqrt(Math.Pow(m_fM_0_Ed_qp_y, 2) + Math.Pow(m_fM_0_Ed_qp_z, 2));

            m_fe_Sd_y = Math.Abs(m_fM_Ed_1_y / m_fN_Ed);
            m_fe_Sd_z = Math.Abs(m_fM_Ed_1_z / m_fN_Ed);

            m_fL_0_y = m_fBeta_y * m_fL;
            m_fL_0_z = m_fBeta_z * m_fL;

            float fA_s_1 = MathF.fPI * MathF.Pow2(Fd_s)/ 4; // Plocha jedneho pruta pre konkretny priemer a polohu
            float fA_s_t_1; //Plocha vsetkych nosnych pozdlznych prutov jedneho typu
            FA_s_t = fA_s_t_1 = m_iNo * fA_s_1; // Suma plocha vsetkych nosnych pozdlznych prutov


            Fad = m_fd_s_s;
            Fbc = m_fd_s_s;

            Fa_s_t_y = Ft_b + m_fd_s_s + m_fd_s / 2f;
            Fa_s_t_z = Ft_b + m_fd_s_s + m_fd_s / 2f;

            // Suma !!! 
            FI_s_y = ((1f / 64f) * MathF.fPI * MathF.Pow4(m_fd_s) + fA_s_1 * MathF.Pow2((fh / 2f) - Fa_s_t_y)) * m_iNo;
            FI_s_z = ((1f / 64f) * MathF.fPI * MathF.Pow4(m_fd_s) + fA_s_1 * MathF.Pow2((fb / 2f) - Fa_s_t_z)) * m_iNo;

            Fi_s_y = MathF.Sqrt(FI_s_y / FA_s_t);
            Fi_s_z = MathF.Sqrt(FI_s_z / FA_s_t);

            int iNo_Cut = 2; // Pocet strihov na v jednom reze prierezu 2 alebo 4 /2 alebo 4 strizny strmen
            float m_fA_s_s_1 = MathF.fPI * MathF.Pow2(m_fd_s_s) / 4; // Plocha prierezu pruta jedneho strmena / jeden prut smykovej vystuze
            m_fA_s_s = m_fA_s_s_1 * iNo_Cut; // Celkova plocha smykovej vystuze v reze

            FA = Fb * Fh;
            FA_c = FA - FA_s_t;

            Fu = 2 * Fb + 2 * fh; // obvod prierezu
            
            // Vypocet vysledkov
            // Vytvori sa objekt triedy vypoctu
            EC2 objekt_EC2 = new EC2(

            m_fN_Ed,
            m_fM_Ed_1_y,
            m_fM_Ed_1_z,

            m_fM_0_1_y,
            m_fM_0_2_y,
            m_fM_0_1_z,
            m_fM_0_2_z,

            m_fN_0_Ed_qp,
            m_fM_0_Ed_qp_y,
            m_fM_0_Ed_qp_z,

            m_fM_Ed_1,
            m_fM_0_Ed_qp,

            Fb,
            Fh,
            Fa_s_t_y,
            Fa_s_t_z,
            FA,
            FA_s_t,
            FA_c,
            Fu,
            Fi_s_y,
            Fi_s_z,

            m_fn_bal,
            m_ft_0,
            m_ft,
            m_fRH,
            m_fT_Delta_t_i,
            m_fAlpha,
            m_fc_y,
            m_fc_z,

            m_fL_0_y,
            m_fL_0_z,

           m_objDatabase.m_ff_cm * 1000000f,
           m_objDatabase.m_ff_ck * 1000000f,
           m_objDatabase.m_fE_cm * 1000000f,
           m_fLambda,
           m_fEta,
           m_fGamma_Mc,

           m_ff_yk * 1000000f,
           m_fE_s * 1000000f,
           m_fGamma_Ms);


            m_fN_Rd = objekt_EC2.FN_Rd;
            m_fM_Rd_y = objekt_EC2.FM_Rd_y;
            m_fM_Rd_z = objekt_EC2.FM_Rd_z;

            m_fDesRatio1 = objekt_EC2.FDesRatio1;
            m_fDesRatio2 = objekt_EC2.FDesRatio2;
            m_fDesRatio = objekt_EC2.FDesRatio;
 
            // Naplnia sa premenne po vypocte
            this.FI_y = objekt_EC2.FI_y;
            this.FI_z = objekt_EC2.FI_z;
           
            
            
            //MessageBox.Show("Vysledky v EN 1992_1_1 Form \n " + (" A = " + D_A + " mm2 \n Iy = " + D_I_y + " mm4 \n Iz = " + D_I_z + " mm4"));

            // zapísanie výsledkov do READONLY textboxov
            this.Set_data();


        }
        // Metoda - Load data from textboxes
        // Tato metoda nacita udaje z textboxov a skonvertuje na cislo
        public void Load_data()
        {
            string sHeaderFormatError = "FORMAT ERROR";
            string sTextFormatError = "Wrong numerical format! Enter number, please.";
            try
            {
                // Internal Forces
                m_fN_Ed = (float)Convert.ToDouble(Value_N_Ed.Text.ToString());
                m_fM_Ed_1_y = (float)Convert.ToDouble(Value_M_Ed_1_y.Text.ToString());
                m_fM_Ed_1_z = (float)Convert.ToDouble(Value_M_Ed_1_z.Text.ToString());
                
                m_fN_0_Ed_qp = (float)Convert.ToDouble(Value_N_0_Ed_qp.Text.ToString());
                m_fM_0_Ed_qp_y = (float)Convert.ToDouble(Value_M_0_Ed_qp_y.Text.ToString());
                m_fM_0_Ed_qp_z = (float)Convert.ToDouble(Value_M_0_Ed_qp_z.Text.ToString());
                
                m_fM_0_1_y = (float)Convert.ToDouble(Value_M_0_1_y.Text.ToString());
                m_fM_0_1_z = (float)Convert.ToDouble(Value_M_0_1_z.Text.ToString());
                m_fM_0_2_y = (float)Convert.ToDouble(Value_M_0_2_y.Text.ToString());
                m_fM_0_2_z = (float)Convert.ToDouble(Value_M_0_2_z.Text.ToString());

                // Cross-Section
                fb = (float)Convert.ToDouble(Value_b.Text.ToString());
                fh = (float)Convert.ToDouble(Value_h.Text.ToString());

               // Member
               m_fL = (float)Convert.ToDouble(Value_L.Text.ToString());
               m_fBeta_y =  (float)Convert.ToDouble(Value_Beta_y.Text.ToString());
               m_fBeta_z = (float)Convert.ToDouble(Value_Beta_z.Text.ToString());

                // Reinforcement
               m_ft_b = (float)Convert.ToDouble(Value_tb.Text.ToString());
            }
            catch
            {
                MessageBox.Show(sHeaderFormatError, sTextFormatError);
            }
       }

        public void TestCode_data()
        {
            // Internal Forces
            m_fN_Ed = -2880f;
            m_fM_Ed_1_y = 85f;
            m_fM_Ed_1_z = 50f;

            m_fN_0_Ed_qp = -800f;
            m_fM_0_Ed_qp_y = 23f;
            m_fM_0_Ed_qp_z = 15f;

            m_fM_0_1_y = 85f;
            m_fM_0_1_z = 85f;
            m_fM_0_2_y = 85f;
            m_fM_0_2_z = 85f;

            // Transformacia na N, Nm

            m_fN_Ed *= 1000f;
            m_fM_Ed_1_y *= 1000f;
            m_fM_Ed_1_z *= 1000f;

            m_fN_0_Ed_qp *= 1000f;
            m_fM_0_Ed_qp_y *= 1000f;
            m_fM_0_Ed_qp_z *= 1000f;

            m_fM_0_1_y *= 1000f;
            m_fM_0_1_z *= 1000f;
            m_fM_0_2_y *= 1000f;
            m_fM_0_2_z *= 1000f;

            // Cross -section Data
            fb = 0.4f;
            fh = 0.4f;

            // Member Data
            m_fL = 2.8f;
            m_fBeta_y = 1f;
            m_fBeta_z = 1f;

            // Reinforcement
            m_fd_s = 0.025f;
            m_iNo = 4; // Pocet aktivnych pozdlznych prutov jedneho typu - 1/4 - symetria  !!!!
            m_fd_s_s = 0.008f;
            m_fs_s = 0.25f;

            m_ft_b = 0.0275f;

            // Materials
            // Concrete settings
            // Temp - default
            m_fAlpha_cc = 1.0f;
            m_fEta = 1.0f;
            m_fLambda = 0.8f;
            m_fGamma_Mc = 1.5f;

            m_fn_bal = 0.4f;
            m_ft_0 = 28f; // Days
            m_ft = 25550f; // Days
            m_fRH = 50;
            m_fT_Delta_t_i = 20f;
            m_fAlpha = 0.0f;
            m_fc_y = 9f;
            m_fc_z = 9f;

            // Reinforcement
            m_fGamma_Ms = 1.15f;
        } 

        // Metoda - Nastaví vypocitane hodnoty v textboxoch
        public void Set_data ()
        {
            Update_IF_Data();
            Update_Member_Data();
            Update_CrSc_Data();
            Update_Results_Data();
        }

        public void Set_DefaultInput_Data()
        {
            // Cross -section Data
            Value_b.Text = fb.ToString();
            Value_h.Text = fh.ToString();

            // Member Data
            Value_Beta_y.Text = m_fBeta_y.ToString();
            Value_Beta_z.Text = m_fBeta_z.ToString();

            // Reinforcement
            Value_Gamma_Ms.Text = m_fGamma_Ms.ToString();
            Value_ss.Text = m_fs_s.ToString();
            Value_tb.Text = m_ft_b.ToString();

            // Materials
            // Concrete settings
            Value_Alpha_cc.Text = m_fAlpha_cc.ToString();
            Value_Eta.Text = m_fEta.ToString();
            Value_Lambda.Text = m_fLambda.ToString();
            Value_Gamma_Mc.Text = m_fGamma_Mc.ToString();

            Value_n_bal.Text = m_fn_bal.ToString();
            Value_t_0.Text = m_ft_0.ToString();
            Value_t.Text = m_ft.ToString();
            Value_RH.Text = m_fRH.ToString();
            Value_T_Delta_t_i.Text = m_fT_Delta_t_i.ToString();
            Value_Alpha.Text = m_fAlpha.ToString();
            Value_c_y.Text = m_fc_y.ToString();
            Value_c_z.Text = m_fc_z.ToString();
        }

        public void Update_IF_Data()
        {
            // Internal Forces
            Value_M_Ed_1.Text = m_fM_Ed_1.ToString();
            Value_M_0_Ed_qp.Text = m_fM_0_Ed_qp.ToString();

            Value_e_Sd_y.Text = m_fe_Sd_y.ToString();
            Value_e_Sd_z.Text = m_fe_Sd_z.ToString();
        }

        public void Update_Member_Data()
        {
            // Member
            Value_L_y.Text = m_fL_0_y.ToString();
            Value_L_z.Text = m_fL_0_z.ToString();
        }

        public void Update_CrSc_Data()
        {
            // Cross-Section
            Value_A.Text = FA.ToString();
            Value_Iy.Text = FI_y.ToString();
            Value_Iz.Text = FI_z.ToString();
        }

        public void Update_Concrete_Data()
        {
            // Concrete Database Data

            Value_f_ck.Text = m_objDatabase.m_ff_ck.ToString();
            Value_f_ck_cube.Text = m_objDatabase.m_ff_ck_cube.ToString();
            Value_f_cm.Text = m_objDatabase.m_ff_cm.ToString();
            Value_f_ctm.Text = m_objDatabase.m_ff_ctm.ToString();
            Value_E_cm.Text = m_objDatabase.m_fE_cm.ToString();
        }

        public void Update_Reinforcement_Data()
        {
            // Reinforcement Database Data

            Value_f_yk.Text = m_ff_yk.ToString();
            Value_f_uk.Text = m_ff_uk.ToString();
            Value_E_s.Text = m_fE_s.ToString();
            // Value_Gamma_Ms.Text = m_fGamma_Ms.ToString();
        }

        public void Update_Reinforcement_Long_Data()
        {
            // Longitudinal Reinforcement Data

            Value_As.Text = FA_s_t.ToString();
        }

        public void Update_Reinforcement_Trans_Data()
        {
            // Transversal Reinforcement Data

            Value_A_ss.Text = FA_s_s.ToString();
        }

        public void Update_Results_Data()
        {
            // Results
            Value_M_Rd.Text = m_fM_Rd_y.ToString();
            Value_DesRatio.Text = m_fDesRatio.ToString();
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

            Update_Concrete_Data();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Not implemented yet
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_ff_yk = m_objDatabase.Get_Reinf_f_yk((short)comboBox3.SelectedIndex);
            m_ff_uk = m_objDatabase.Get_Reinf_f_tk((short)comboBox3.SelectedIndex);
            m_fE_s = 200000f; // 200 000 MPa

            Update_Reinforcement_Data();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fd_s = m_objDatabase.Get_Reinf_d_s((short)comboBox4.SelectedIndex);

            Update_Reinforcement_Long_Data();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fd_s_s = m_objDatabase.Get_Reinf_d_s((short)comboBox5.SelectedIndex);

            Update_Reinforcement_Trans_Data();
        }
    }
}
