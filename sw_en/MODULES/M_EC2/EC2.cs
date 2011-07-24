using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATH;

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
        public EC2(float b, float h, float fA_s, float ff_ck, float fLambda, float fGamma_Mc, float ff_yk, float fGamma_Ms, float fM_Ed)
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
            EC2_4_OHYB(ff_cd, ff_yd, fA_s, fb, fh, fLambda, fM_Ed, m_fM_Rd1, m_fDesRatio1); // Design
        }

        //////////////////////////////////////////////////////////////
        // HLAVNY VYPOCET
        //////////////////////////////////////////////////////////////

        public void EC2_CrScProp()
        {
            this.d_A = fb * fh;
            this.d_I_y = fb / 12 * Math.Pow(fh, 3);
            this.d_I_z = fh / 12 * Math.Pow(fb, 3);
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
            float fz = Eq_z(fh, fLambda, fx);
            //float fEps_cu;
            //float fEps_sy;

            // Output results
            fM_Rd = Eq_M_Rd(fA_s, ff_yd, fz);
            fRatio = Eq_Ratio(fM_Ed, fM_Rd);
        }


        // Rectangular Cross-ection
        // Uniaxial bending  6.1

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
            return fA_s * ff_yd * fz; // M_Rd
        }
        public float Eq_A_s_min(float ff_ctm, float ff_yk, float fb, float fh)
        {
            return Math.Max(0.26f * (ff_ctm / ff_yk) * fb * fh, 0.0013f * fb * fh); // fA_s_min
        }
        public float Eq_A_s_max(float fA_c, float fh, float fb, float ff_cd, float ff_yd)
        {
            return Math.Min(0.04f * fA_c, 0.8f * 0.45f * fb * fh * ff_cd / ff_yd); // fA_s_max
        }
        public float Eq_Ratio(float fM_Ed, float fM_Rd)
        {
            return fM_Ed / fM_Rd; // Design Ratio
        }

        // 6.2 Shear
        // Shear Force  6.2.2

        public float Eq_C_Rd(float fGamma_Mc)
        {
            return 0.18f / fGamma_Mc; // fC_Rd
        }
        public float Eq_k_Shear622(float fh)
        {
            return 1 + MathF.Sqrt(200 / fh); //fk
        }
        public float Eq_Rho_l(float fA_sl, float fb, float fh)
        {
            return fA_sl / (fb * fh); // fRho_l
        }
        public float Eq_v_min(float fk, float ff_ck)
        {
            return 0.035f * MathF.Pow_3_2(fk) * MathF.Sqrt(ff_ck); //fv_min
        }
        public float Eq_V_Rd_c(float fCRd_c, float fk, float fRho_l, float ff_ck, float fb_w, float fh)
        {
            return (fCRd_c * fk * MathF.Pow_1_3(100 * fRho_l * ff_ck)) * fb_w * fh;  // fV_Rd_c
        }
        public float Eq_Ratio_V_min(float fV_Rd_c, float fv_min, float fb, float fh)
        {
            return fV_Rd_c / (fv_min * fb * fh); // Ratio 
        }
        public float Eq_Ratio_V(float fV_Ed, float fV_Rd_c)
        {
            return fV_Ed / fV_Rd_c; // Design Ratio 
        }

        // Shear Force  6.2.3
        public float Eq_Tau_Rd(float fF_ctk0_05, float fGamma_Mc)
        {
            return 0.25f * fF_ctk0_05 / fGamma_Mc; //  fTau_Rd
        }
        public float Eq_k_Shear623(float fh)
        {
            if (1.6f - fh / 1000 <= 1)
                return 1f;
            else
                return 1.6f - fh / 1000;
        }

        // opakuje sa
        //        public float Eq_Rho_l(float fA_sl, float fb, float fh)
        //        {
        //        return Asl / (fb*fh); // fRho_l
        //        }

        public float Eq_V_cd(float fTau_Rd, float fk, float fRho_l, float fb, float fh)
        {
            // Únosnost přenašená tlačeným betonem
            return fTau_Rd * fk * (1.2f + 40f * fRho_l) * fb * fh; // fV_cd 
        }


        //θ
        //cot θ

        public float Eq_v(float ff_ck)
        {
            return 0.6f * (1 - ff_ck / 250); // fv
        }
        public float Eq_Rho_w(float fA_sw, float fb, float fs, float fv, float ff_cd, float ff_y_wd)
        {
            return Math.Min(fA_sw / (fb * fs), 0.5f * fv * ff_cd / ff_y_wd); // fRho_w
        }
        public float Eq_Rho_w_min(float ff_ck, float ff_yk)
        {
            return (0.08f * MathF.Sqrt(ff_ck)) / ff_yk; // fRho_w_min
            //ρw ≥ ρw,min
        }
        public float Eq_z(float fd)
        {
            return 0.9f * fd; // fz
        }

        // Únosnost třmínku
        public float Eq_V_Rd_s(float fA_sw, float ff_y_wd, float fs, float fz, float fDelta)
        {
            return ((fA_sw * ff_y_wd) / fs) * fz * (float)Math.Atan(fDelta); // V_Rd_s
        }

        // Únosnost tlakových diagonál
        public float Eq_V_Rd_max(float fv, float ff_cd, float fb, float fz, float fDelta)
        {
            return fv * ff_cd * fb * fz * ((float)Math.Atan(fDelta) / (1f + (float)MathF.Pow2((float)Math.Atan(fDelta)))); // fV_Rd_max 
        }
        public float Eq_V_Rd(float fV_cd, float fV_Rd_s, float fV_Rd_max)
        {
            return Math.Min(fV_cd + fV_Rd_s, fV_Rd_max); // fV_Rd
        }
    }
}
