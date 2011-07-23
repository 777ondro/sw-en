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

        private float fGamma_Ms;

        public float FGamma_Ms
        {
            get { return fGamma_Ms; }
            set { fGamma_Ms = value; }
        }


        private float fM_Ed;

        public float FM_Ed
        {
            get { return fM_Ed; }
            set { fM_Ed = value; }
        }

        private float m_fM_Rd1;

        public float FM_Rd1
        {
            get { return m_fM_Rd1; }
            set { m_fM_Rd1 = value; }
        }
        private float m_fDesRatio1;

        public float FDesRatio1
        {
            get { return m_fDesRatio1; }
            set { m_fDesRatio1 = value; }
        }

        // Konstruktor
        public EC2(float b,float h, float fA_s, float ff_ck, float fLambda, float fGamma_Mc, float ff_yk, float fGamma_Ms, float fM_Ed)

        {
            // priradenie hodnot premennym zavolanim vlastnosti premennej v inej triede
            // Corss-section
            Fb = b;
            Fh = h;

            // Interanl force
            FM_Ed = fM_Ed;
            
            // Concrete
            FLambda = fLambda;
            //Ff_ck = ff_ck;
            FGamma_Mc = fGamma_Mc;

            Ff_cd = ff_ck / FGamma_Mc;

            // Steel
            FA_s = fA_s;
            //Ff_yk = ff_yk;
            FGamma_Ms = fGamma_Ms;
            Ff_yd = ff_yk / FGamma_Ms;


            //urobime vypocet
            EC2_CrScProp(); // Prierez
            EC2_4_OHYB(ff_cd, ff_yd, fA_s, fb, fh,fLambda,fM_Ed, m_fM_Rd1, m_fDesRatio1); // Design
       }

        //////////////////////////////////////////////////////////////
        // HLAVNY VYPOCET
        //////////////////////////////////////////////////////////////
        
        public void EC2_CrScProp()
        {
           this.d_A = fb * fh;
           this.d_I_y = fb/12* Math.Pow(fh, 3);
           this.d_I_z = fh/12 * Math.Pow(fb, 3);
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
        public void EC2_4_OHYB(float ff_cd, float ff_yd, float fA_s, float fb, float fh, float fLambda, float fM_Ed, float fM_Rd, float fRatio)
        {
            // NAPR. SEM MOZES PISAT VYPOCET
            // Asi bude najlepsie vytvorit samostatnu metodu pre kazde posudenie (TAH, TLAK, OHYB,  VZPER, OHYB+VZPER, ....)

            // Jednotlive metody byte som pomenoval podla nejako podla čísel článkov ale nesmu tam byt bodky "."

            // 6.1 Ohybový moment s normálovou silou nebo bez normálové síly

            // Auxiliary values
            float fx = Eq_x(fA_s, ff_yd, fb, fLambda, ff_cd);
            float fXi = Eq_Xi(fx, fh);
            float fXi_bal_1 = Eq_Xi_bal_1(ff_yd);
            float fz=Eq_z(fh, fLambda,fx);
            //float fEps_cu;
            //float fEps_sy;

            // Output results
            fM_Rd = Eq_M_Rd(fA_s, ff_yd, fz);
            fRatio = Eq_Ratio(fM_Ed,fM_Rd);
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
