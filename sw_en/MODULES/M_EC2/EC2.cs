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
        public EC2(float fb, float fh, float fA_s, float ff_ck, float fLambda, float fGamma_Mc, float ff_yk, float fGamma_Ms, float fM_Ed)
        {
            // priradenie hodnot premennym zavolanim vlastnosti premennej v inej triede
            // Corss-section
            Fb = fb;
            Fh = fh;

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Výpočet vzperu EN 1992-1-1-5.8.8 Metoda založená na menovitej krivosti:
        // 5.8.8 Metoda zalozena na menovitej krivosti
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Input 
        // fM_0_1
        // fM_0_2
        // fn_bal
        // ft_0
        // ft
        // fRH
        // fT_Delta_t_i
        // fAlpha
        // fc

        public float Eq_Lambda(float fL_0, float fh)
        {
            return fL_0 * MathF.Sqrt(12f / fh); // fLambda
        }
        public float Eq_A(float fPhi_ef)
        {
            return 1 / (1 + 0.2f * fPhi_ef); // fA
        }
        public float Eq_B(float fOmega)
        {
            return MathF.Sqrt(1 + 2 * fOmega); // fB
        }
        public float Eq_C(float fr_m)
        {
            return 1.7f - fr_m;  // fC
        }
        public float Eq_r_m(float fM_0_1, float fM_0_2)
        {
            return fM_0_1 / fM_0_2; // r_m
        }
        public float Eq_fLambda_lim(float fA, float fB, float fC, float fn)
        {
            return 20 * fA * fB * fC / MathF.Sqrt(fn); // fLambda_lim
        }
        /*
                            public float Eq_(float b)
{
return fLambda < fLambda_lim; //
                            }
          */
        public float Eq_e_i(float fL_0)
        {
            return fL_0 / 400; // fe_i
        }
        public float Eq_M_0_Ed(float fM_1_Ed, float fN_Ed, float fe_i)
        {
            return fM_1_Ed + fN_Ed * fe_i; // fM_0_Ed
        }
        public float Eq_e_0(float fM_0_Ed, float fN_Ed)
        {
            return fM_0_Ed / fN_Ed;  // fe_0
        }
        public float Eq_fOmega(float fA_s, float ff_yd, float fA_c, float ff_cd)
        {
            return fA_s * ff_yd / (fA_c * ff_cd); // fOmega
        }
        public float Eq_n_u(float fOmega)
        {
            return 1 + fOmega; // fn_u
        }
        public float Eq_n(float fN_Ed, float fA_c, float ff_cd)
        {
            return fN_Ed / (fA_c * ff_cd); // fn
        }
        public float Eq_K_r(float fn_u, float fn, float fn_bal)
        {
            return Math.Min((fn_u - fn) / (fn_u - fn_bal), 1.0f); //fK_r
        }
        public float Eq_Beta(float ff_ck, float fLambda)
        {
            return 0.35f + ff_ck / 200 - fLambda / 150; // fBeta
        }
        public float Eq_Sigma_c(float fN_0_Ed_qp, float fA_c)
        {
            return fN_0_Ed_qp / fA_c; // fSigma_c
        }
        public float Eq_045f_ck_t_0(float ff_ck_ft_0)
        {
            return 0.45f * ff_ck_ft_0; //
        }
        public float Eq_h_0(float fA_c, float fu)
        {
            return 2 * fA_c / fu; // fh_0
        }
        public float Eq_Alpha_1(float ff_cm)
        {
            return (float)Math.Pow(35 / ff_cm, 0.7f); // fAlpha_1
        }
        public float Eq_Alpha_2(float ff_cm)
        {
            return (float)Math.Pow(35 / ff_cm, 0.2f); // fAlpha_2
        }
        public float Eq_Alpha_3(float ff_cm)
        {
            return (float)Math.Pow(35 / ff_cm, 0.5f); // fAlpha_3
        }
        public float Eq_Phi_RH(float fRH, float fh_0, float fAlpha_1, float fAlpha_2)
        {
            return (1 + (1 - fRH / 100) / (0.1f * MathF.Pow_1_3(fh_0)) * fAlpha_1) * fAlpha_2; // fPhi_RH
        }
        public float Eq_Beta_f_cm(float ff_cm)
        {
            return 16.8f / MathF.Sqrt(ff_cm); // fBeta_f_cm
        }
        public float Eq_Beta_ft_0(float ft_0)
        {
            return 1 / (0.1f + (float)Math.Pow(ft_0, 0.2f)); // fBeta_ft_0
        }
        public float Eq_Phi_0(float fPhi_RH, float fBeta_f_cm, float fBeta_t_0)
        {
            return fPhi_RH * fBeta_f_cm * fBeta_t_0; // fPhi_0
        }
        public float Eq_Beta_H(float fRH, float fh_0, float fAlpha_3)
        {
            return Math.Min(1.5f * (1 + MathF.PowN(0.012f * fRH, 18)) * fh_0 + 250 * fAlpha_3, 1500 * fAlpha_3);    // fBeta_H
        }
        public float Eq_t_0_T(float fT_Delta_t_i, float fDelta_t_i)
        {
            return /*∑*/ (float)Math.Pow(Math.E, -(4000 / (273 + fT_Delta_t_i) - 13.65f)) * fDelta_t_i;  // ft_0_T
        }
        public float Eq_t_0(float ft_0_T, float fAlpha)
        {
            return Math.Max(ft_0_T * (float)Math.Pow(9 / (2 + (float)Math.Pow(ft_0_T, 1.2f) + 1), fAlpha), 0.5f);  // ft_0
        }
        public float Eq_Beta_c_ft_ft_0(float ft, float ft_0, float fBeta_H)
        {
            return (float)Math.Pow((ft - ft_0) / (fBeta_H + ft - ft_0), 0.3f);    // fBeta_c_ft_ft_0
        }
        public float Eq_Ph_Infinity_ft_0(float fPhi_0, float fBeta_c_ft_ft_0)
        {
            return fPhi_0 * fBeta_c_ft_ft_0;  // fPh_Infinity_ft_0
        }
        public float Eq_Phi_ef(float fPhi_Infinity_ft_0, float fM_0_Ed_qp, float fM_0_Ed)
        {
            return fPhi_Infinity_ft_0 * fM_0_Ed_qp / fM_0_Ed; // fPhi_ef
        }
        public float Eq_fK_Phi(float fBeta, float fPhi_ef)
        {
            return Math.Max(1 + fBeta * fPhi_ef, 1.0f); // fK_Phi
        }
        public float Eq_Eps_d(float ff_yd, float fE_s)
        {
            return ff_yd / fE_s; // fEps_d 
        }
        public float Eq_fd(float fh, float fi_s)
        {
            return fh / 2f + fi_s; // fd - Not section depth but factor
        }
        public float Eq_1_r_0(float fEps_d, float fd)
        {
            return fEps_d / (0.45f * fd); // f1_r_0
        }
        public float Eq_1_r(float fK_r, float fK_Phi, float fPhi, float f1_r0)
        {
            return fK_r * fK_Phi * fPhi * f1_r0; // f1_r
        }
        public float Eq_M_2(float fN_Ed, float f1_r, float fL_0, float fc)
        {
            return /*fN_Ed.e2*/ fN_Ed * f1_r * MathF.Pow2(fL_0) / fc; // fM_2
        }
        public float Eq_e_2(float fM_2, float fN_Ed)
        {
            return fM_2 / fN_Ed; // f_e_2
        }
        public float Eq_M_Ed(float fM_0_Ed, float fM_2)
        {
            return fM_0_Ed + fM_2; // fM_Ed
        }
        public float Eq_e_tot(float fM_Ed, float fN_Ed)
        {
            return fM_Ed / fN_Ed; // fe_tot
        }






        // Axial Force and Bending Moment
        // N + M_Ed
        public float Eq_F_s1(float fA_s1, float ff_yd)
        {
            return fA_s1 * ff_yd; // fF_s1
        }
        public float Eq_F_s2(float fA_s2, float ff_yd)
        {
            return fA_s2 * ff_yd; // fF_s2
        }
        public float Eq_F_s(float fA_s1, float fA_s2, float ff_yd)
        {
            return (fA_s1 + fA_s2) * ff_yd; // fF_s
        }
        public float Eq_Delta_F_s(float fA_s1, float fA_s2, float ff_yd)
        {
            return (fA_s2 - fA_s1) * ff_yd; // fDelta_F_s
        }
        public float Eq_e_0(float fh)
        {
            return Math.Min(fh / 30, 20); // fe_0
        }
        //Bod 0
        public float Eq_N_Rd_0(float fb, float fh, float fEta, float ff_cd, float fA_s, float fSigma_s)
        {
            return -(fb * fh * fEta * ff_cd +/*∑*/fA_s * fSigma_s); // fN_cu = fN_Rd_0
        }
        public float Eq_M_Rd_0(float fA_s1, float fz_s1, float fA_s2, float fz_s2, float fSigma_s)
        {
            return (fA_s2 * fz_s2 - fA_s1 * fz_s1) * fSigma_s; // fM_cu = fM_Rd_0
        }
        //Bod 0´
        public float Eq_N_eu(float fb, float fh, float fEta, float ff_cd, float fA_s, float fSigma_s)
        {
            return -(0.8f * fb * fh * fEta * ff_cd +/*∑*/fA_s * fSigma_s);  // fN_eu = fN_Rd_0´ 
        }
        //Bod 1
        public float Eq_N_Rd_1(float ffLamda, float fb, float fd, float ff_cd, float fF_s2)
        {
            return -(fLambda * fb * fd * ff_cd + fF_s2); // fN_Rd_1
        }
        public float Eq_M_Rd_1(float fLambda, float fb, float fd, float fh, float ff_cd, float fF_s2, float fz_s2)
        {
            return fLambda * fb * fd * (0.5f * fh - 0.4f * fd) * ff_cd + fF_s2 * fz_s2; // fM_Rd_1
        }
        // fd> = fEps_lim_2*fd_2; // Distance between face and centroid of reinforcement
        //Bod 2
        public float Eq_N_cu_lim(float fLamda, float fEps_lim, float fb, float fd, float ff_cd, float fDelta_F_s)
        {
            return -(fLambda * fEps_lim * fb * fd * ff_cd + fDelta_F_s); // fN_cu_lim 
        }
        public float Eq_M_cu_lim(float fLamda, float fEps_lim, float fb, float fd, float fh, float ff_cd, float fF_s1, float fz_1, float fF_s2, float fz_2)
        {
            return fLambda * fEps_lim * fb * fd * (0.5f * fh - 0.4f * fEps_lim * fd) * ff_cd + fF_s2 * fz_2 + fF_s1 * fz_1; // fM_cu_lim = fM_Rd_lim
        }
        //Bod 3
        public float Eq_N_Rd_3()
        {
            return 0f; // fN_Rd_3
        }
        public float Eq_M_Rd_3(float fF_s1, float fd, float fLambda, float fx)
        {
            return fF_s1 * (fd - 0.5f * fLambda * fx); // fM_Rd_3
        }
        public float Eq_x(float fF_s1, float fLamda, float fb, float ff_cd)
        {
            return fF_s1 / fLambda * fb * fEta * ff_cd; // fx
        }
        //Bod 4
        public float Eq_N_Rtd_lim(float fF_s1)
        {
            return fF_s1; // fN_Rtd_lim = fF_s1
        }
        public float Eq_M_Rtd_lim(float fF_s1, float fz_1)
        {
            return fF_s1 * fz_1; // fM_Rtd_lim
        }
        //Bod 5
        public float Eq_N_Rtd_0(float fF_s1, float fF_s2)
        {
            return fF_s1 + fF_s2;  // fN_Rtd_0
        }
        public float Eq_M_Rtd_0(float fF_s1, float fz_1, float fF_s2, float fz_2)
        {
            return fF_s1 * fz_1 - fF_s2 * fz_2; // fM_Rtd_0
        }


        // Interaction of Interanl Forces
        // Axial Force and Biaxial Bending
        public float Eq_N_Rd(float fA_c, float ff_cd, float fA_s, float ff_yd)
        {

            return fA_c * ff_cd + fA_s * ff_yd; // fN_Rd
        }
        public float Eq_Ratio_M_Ed_M_Rd(float fM_Ed, float fM_Rd)
        {
            return fM_Ed / fM_Rd; // Design Ratio
        }
        public float Eq_Ratio_N_Ed_N_Rd(float fN_Ed, float fN_Rd)
        {
            return fN_Ed / fN_Rd; //Design Ratio
        }

        static float[,] arrTable_a = new float[3, 2]
{
{0.1f, 1.0f},
{0.7f, 1.5f},
{1.0f, 2.0f}
};

        public float Get_a_Table(float fRatio_N_Ed_N_Rd)
        {
            if (fRatio_N_Ed_N_Rd < 0.7f)
                return (((fRatio_N_Ed_N_Rd - arrTable_a[0, 0]) / ((arrTable_a[1, 0] - arrTable_a[0, 0]) / (arrTable_a[1, 1] - arrTable_a[0, 1]))) + 1);
            else
                return (((fRatio_N_Ed_N_Rd - arrTable_a[1, 0]) / ((arrTable_a[2, 0] - arrTable_a[1, 0]) / (arrTable_a[2, 1] - arrTable_a[1, 1]))) + 1.5f);
        }

        public float Eq_DesRatio(float fM_y_Ed, float fM_z_Ed, float fM_y_Rd, float fM_z_Rd, float fa)
        {
            return ((float)Math.Pow(fM_z_Ed / fM_z_Rd, fa) + (float)Math.Pow(fM_y_Ed / fM_y_Ed, fa)) / 1f; // Design Ratio
        }










    }
}
