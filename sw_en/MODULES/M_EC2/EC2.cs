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



        float fA_s;

        public float FA_s
        {
            get { return fA_s; }
            set { fA_s = value; }
        }
        float ff_yd;

        public float Ff_yd
        {
            get { return ff_yd; }
            set { ff_yd = value; }
        }

        float fLambda;

        public float FLambda
        {
            get { return fLambda; }
            set { fLambda = value; }
        }
        float ff_cd;

        public float Ff_cd
        {
            get { return ff_cd; }
            set { ff_cd = value; }
        }

        private float fEta;

        public float FEta
        {
            get { return fEta; }
            set { fEta = value; }
        }

        private float fGamma_Mc;

        public float FGamma_Mc
        {
            get { return fGamma_Mc; }
            set { fGamma_Mc = value; }
        }







        // Konstruktor
        public EC2(float a,float b)

        {
            // priradenie hodnot premennym zavolanim vlastnosti premennej v inej triede
            this.Fb = a;
            this.Fh = b;
            
            //urobime vypocet
            this.EC2_1();
            EC2_4_OHYB(ff_cd, ff_yd, fA_s, fb, fh,fLambda);

       }

        //////////////////////////////////////////////////////////////
        // HLAVNY VYPOCET
        //////////////////////////////////////////////////////////////
        
        public void EC2_1()

       {
           this.d_A = fb * fh;
           this.d_I_y = fb/12* Math.Pow(fh, 3);
           this.d_I_z = fh/12 * Math.Pow(fb, 3);
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
        public void EC2_4_OHYB(float ff_cd, float ff_yd, float fA_s, float fb, float fh, float fLambda)
        {
            // NAPR. SEM MOZES PISAT VYPOCET
            // Asi bude najlepsie vytvorit samostatnu metodu pre kazde posudenie (TAH, TLAK, OHYB,  VZPER, OHYB+VZPER, ....)

            // Jednotlive metody byte som pomenoval podla nejako podla čísel článkov ale nesmu tam byt bodky "."




            // 6.1 Ohybový moment s normálovou silou nebo bez normálové síly

            float fx = Eq_x(fA_s, ff_yd, fb, fLambda, ff_cd);
            float fXi = Eq_Xi(fx, fh);
            float fXi_bal_1 = Eq_Xi_bal_1(ff_yd);
            float fz=Eq_z(fh, fLambda,fx);
            //float fEps_cu;
            //float fEps_sy;

        }
        public float Eq_x(float fA_s, float ff_yd, float fb, float fLambda, float ff_cd)
        {
            return fA_s * ff_yd / (fb * fLambda * ff_cd); // fx
        }
        public float Eq_Xi(float fx, float fh)
        {
            return fx / fh; // fXi
        }
        public float Eq_Xi_bal_1(float ff_yd)
        {
            return 700f / (700f + ff_yd);  // fXi_bal_1 = fEps_cu / (fEps_cu + fEps_sy) 
        }
        //fXi < fXi_bal_1;
        public float Eq_z(float fh, float fLambda, float fx)
        {
            return fh - fLambda * fx / 2f; // dz
        }
        public float Eq_M_Rd(float fA_s, float ff_yd, float fz)
        {
        return fA_s*ff_yd * fz; // M_Rd
        }
        public float Eq_A_s_min (float ff_ctm, float ff_yk, float fb, float fh)
        {
            return Math.Max(0.26f * (ff_ctm / ff_yk) * fb * fh, 0.0013f * fb * fh); // A_s_min
        }
        public float Eq_A_s_max (float fA_c, float fh, float fb, float ff_cd, float ff_yd)
        {
        return    Math.Min(0.04f*fA_c, 0.8f * 0.45f*fb*fh *ff_cd / ff_yd); // A_s_max
        }
        public float Eq_Ratio(float fM_Ed, float fM_Rd)
        {
            return fM_Ed/ fM_Rd; // Design Ratio
        }
               
    }
}
