using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace M_AS4600
{
    public class AS_4600
    {
        public float Eq_7212_1__(float flambda_c, float fN_y)
        {
            return (float)(Math.Pow(0.658f, MathF.Pow2(flambda_c)) * fN_y); // Eq. (7.2.1.2(1)) // fN_ce
        }
        public float Eq_7212_2__(float flambda_c, float fN_y)
        {
            return (0.877f / MathF.Pow2(flambda_c)); // Eq. (7.2.1.2(2)) // fN_ce
        }
        public float Eq_7212____(float flambda_c, float fN_y)
        {
            if (flambda_c <= 1.5f)
                return Eq_7212_1__(flambda_c, fN_y); // Eq. (7.2.1.2(1))
            else
                return Eq_7212_2__(flambda_c, fN_y); // Eq. (7.2.1.2(2))
        }
        public float Eq_7212_3__(float fN_y, float fN_oc)
        {
            return (MathF.Sqrt(fN_y / fN_oc)); // Eq. (7.2.1.2(3)) // flambda_c
        }
        public float Eq_7212_4__(float fA_g, float ff_oc)
        {
            if (ff_oc > 0)
                return fA_g * ff_oc; // Eq. (7.2.1.2(4)) // fN_oc
            else
                return 0f; // Error
        }
        public float Eq_7212_5__(float fA_g, float ff_y)
        {
            return fA_g * ff_y; // Eq. (7.2.1.2(5)) // fN_y
        }
        public float Eq_7213_1__(float fN_ce)
        {
            return fN_ce; // Eq. (7.2.1.3(1)) // fN_cl
        }
        public float Eq_7213_2__(float fN_ol, float fN_ce)
        {
            return (1f - 0.15f * MathF.Pow4(fN_ol / fN_ce)) * MathF.Pow4(fN_ol / fN_ce) * fN_ce; // Eq. (7.2.1.3(2)) // fN_cl
        }
        public float Eq_7213____(float flambda_l, float fN_ol, float fN_ce)
        {
            if (flambda_l <= 0.776f)
                return Eq_7213_1__(fN_ce); // Eq. (7.2.1.3(1))
            else
                return Eq_7213_2__(fN_ol, fN_ce); // Eq. (7.2.1.3(2))
        }
        public float Eq_7213_3__(float fN_ce, float fN_ol)
        {
            return (MathF.Sqrt(fN_ce / fN_ol)); // Eq. (7.2.1.3(3)) // flambda_l
        }
        public float Eq_7213_4__(float fA_g, float ff_ol)
        {
            if (ff_ol > 0)
                return fA_g * ff_ol; // Eq. (7.2.1.3(4)) // fN_ol
            else
                return 0f; // Error
        }
        public float Eq_7214_1__(float fN_y)
        {
            return fN_y; // Eq. (7.2.1.4(1)) // fN_cd
        }
        public float Eq_7214_2__(float flambda_d, float fN_y, float fN_od)
        {
            return (float)((1.0f - (0.25f * Math.Pow(fN_od / fN_y, 0.6f))) * Math.Pow(fN_od / fN_y, 0.6f) * fN_y); // Eq. (7.2.1.4(2)) // fN_cd
        }
        public float Eq_7214____(float flambda_d, float fN_y, float fN_od)
        {
            if (flambda_d <= 0.561f)
                return Eq_7214_1__(fN_y); // Eq. (7.2.1.4(1))
            else
                return Eq_7214_2__(flambda_d,fN_y, fN_od); // Eq. (7.2.1.4(2))
        }
        public float Eq_7214_3__(float fN_y, float fN_od)
        {
            return (MathF.Sqrt(fN_y / fN_od)); // Eq. (7.2.1.4(3)) // flambda_d
        }
        public float Eq_7214_4__(float fA_g, float ff_od)
        {
            if (ff_od > 0)
                return fA_g * ff_od; // Eq. (7.2.1.4(4)) // fN_od
            else
                return 0f; // Error
        }





        // Annex D
        public float Eq_D111_1__(float fE, float fl_e, float fr)
        {
            return MathF.Pow2(MathF.fPI) * fE / (fl_e / fr); // Eq. (D1.1.1(1)) // ff_oc
        }
        public float Eq_D111_2__(float ff_oz, float ff_ox, float fBeta)
        {
            return (1f / (2f * fBeta)) * ((ff_ox + ff_oz) - MathF.Sqrt(MathF.Pow2(ff_ox + ff_oz) - 4f * fBeta * ff_ox * ff_oz)); // Eq. (D1.1.1(2)) // ff_oxz
        }
        public float Eq_D111_3__(float fE, float fl_ex, float fr_x)
        {
            return MathF.Pow2(MathF.fPI) * fE / (fl_ex / fr_x); // Eq. (D1.1.1(3)) // ff_ox
        }
        public float Eq_D111_4__(float fE, float fl_ey, float fr_y)
        {
            return MathF.Pow2(MathF.fPI) * fE / (fl_ey / fr_y); // Eq. (D1.1.1(4)) // ff_oy
        }
        public float Eq_D111_5__(float fG, float fE, float fJ, float fI_w, float fA_g, float fl_ez, float fr_01)
        {
            return ((fG * fJ) / (fA_g * MathF.Pow2(fr_01))) * (1 + (MathF.Pow2(MathF.fPI) * fE * fI_w) / (fG * fJ * MathF.Pow2(fl_ez))); // Eq. (D1.1.1(5)) // ff_oz
        }
        public float Eq_D111_6__(float fr_x, float fr_y, float fx_o, float fy_o)
        {
            return MathF.Sqrt(MathF.Pow2(fr_x) + MathF.Pow2(fr_y) + MathF.Pow2(fx_o) + MathF.Pow2(fy_o)); // Eq. (D1.1.1(6)) // fr_o1
        }
        public float Eq_D111_7__(float fx_o, float fr_o1)
        {
            return 1.0f - (MathF.Pow2(fx_o / fr_o1)); // Eq. (D1.1.1(7)) // fBeta
        }
        public float Eq_D111_8__(float ff_oz, float ff_ox)
        {
            return ff_oz * ff_ox / (ff_oz + ff_ox); // Eq. (D1.1.1(8)) // ff_oxz
        }
        public void Eq_D111_9__(float ff_oz, float ff_ox, float ff_oy, float fr_o1, float fx_o, float fy_o, out float fa_CEQ, out float fb_CEQ, out float fc_CEQ, out float fd_CEQ)
        {
            // Eq. (D1.1.1(9)) // Cubic Equation to calculate ff_oc
            fa_CEQ = MathF.Pow2(fr_o1) - MathF.Pow2(fx_o) - MathF.Pow2(fy_o);
            fb_CEQ = -((MathF.Pow2(fr_o1) * (MathF.Pow2(ff_ox) + MathF.Pow2(ff_oy) + MathF.Pow2(ff_oz))) - (ff_oy * MathF.Pow2(fx_o) + ff_ox * MathF.Pow2(fy_o)));
            fc_CEQ = MathF.Pow2(fr_o1) * (ff_ox * ff_oy + ff_oy * ff_oz + ff_ox * ff_oz);
            fd_CEQ = -(ff_ox * ff_oy * ff_oz * MathF.Pow2(fr_o1));
        }

        // Distorsial buckling stresses
        // Compression members without holes
        // General channels in compression
        public float Eq_D121_1__(float fE, float fA, float fAlpha_1, float fAlpha_2, float fAlpha_3)
        {
            return (fE / (2.0f * fA)) * ((fAlpha_1 + fAlpha_2) - MathF.Sqrt(MathF.Pow2(fAlpha_1 + fAlpha_2) - 4.0f * fAlpha_3)); // Eq. (D1.2.1(1)) // ff_od
        }
        public float Eq_D121_2__(float fEta, float fBeta_1, float fBeta_2, float fJ, float fLambda, float fk_Phi, float fE)
        {
            return ((fEta / fBeta_1) * (fBeta_2 + 0.039f * fJ * MathF.Pow2(fLambda))) + (fk_Phi / (fBeta_1 * fEta * fE)); // Eq. (D1.2.1(2)) // fAlpha_1
        }
        public float Eq_D121_3__(float fEta, float fI_y, float fy_o, float fBeta_1, float fBeta_3)
        {
            return fEta * (fI_y + 2 * fy_o * fBeta_3 / fBeta_1); // Eq. (D1.2.1(3)) // fAlpha_2
        }
        public float Eq_D121_4__(float fEta, float fAlpha_1, float fI_y, float fBeta_1, float fBeta_3)
        {
            return fEta * ((fAlpha_1 * fI_y) - ((fEta / fBeta_1) * MathF.Pow2(fBeta_3))); // Eq. (D1.2.1(4)) // fAlpha_3
        }
        public float Eq_D121_5__(float fh_x, float fI_x, float fI_y, float fA)
        {
            return MathF.Pow2(fh_x)  + ((fI_x + fI_y) / fA); // Eq. (D1.2.1(5)) // fBeta_1
        }
        public float Eq_D121_6__(float fI_w, float fI_x, float fx_o, float fh_x)
        {
            return fI_w + fI_x * MathF.Pow2(fx_o - fh_x); // Eq. (D1.2.1(6)) // fBeta_2
        }
        public float Eq_D121_7__(float fI_xy, float fx_o, float fh_x)
        {
            return fI_xy * (fx_o - fh_x); // Eq. (D1.2.1(7)) // fBeta_3
        }
        public float Eq_D121_8__(float fBeta_2, float fy_o, float fh_y, float fI_y, float fBeta_3)
        {
            return fBeta_2 + (fy_o - fh_y) * (fI_y * (fy_o - fh_y) - 2.0f * fBeta_3); // Eq. (D1.2.1(8)) // fBeta_4
        }
        public float Eq_D121_9__(float fBeta_4, float fb_w, float ft)
        {
            return 4.8f * (float)Math.Pow(fBeta_4 * fb_w / MathF.Pow3(ft), 0.25f); // Eq. (D1.2.1(7)) // fLambda
        }
        public float Eq_D121_10_(float fLambda)
        {
            return MathF.Pow2(MathF.fPI / fLambda); // Eq. (D1.2.1(10)) // fEta
        }
        public float Eq_D121_11_(float fE, float ft, float fb_w, float fLambda, float ff_od_par)
        {
            return (fE * MathF.Pow3(ft) / (5.46f * (fb_w + 0.06f * fLambda))) * (1.0f - (((1.11f * ff_od_par) / (fE * MathF.Pow2(ft))) * ((MathF.Pow3(fb_w) * fLambda) / (MathF.Pow2(fb_w) + MathF.Pow2(fLambda))))); // Eq. (D1.2.1(11)) // fk_Phi
        }
        public float Eq_D121_12_(float fEta, float fBeta_1, float fBeta_2, float fJ, float fLambda)
        {
            return (fEta / fBeta_1) * (fBeta_2 + 0.039f * fJ * MathF.Pow2(fLambda)); // Eq. (D1.2.1(12)) // fAlpa_1 for calculation of f'od
        }

        public float Eq_D121_1__(float fE, float fA, float fI_x, float fI_y, float fI_xy, float fJ, float fI_w, float fx_o, float fy_o, float fh_x, float fh_y, float fb_w, float ft)
        {
            float fBeta_1_temp = Eq_D121_5__(fh_x, fI_x, fI_y, fA);
            float fBeta_2_temp = Eq_D121_6__(fI_w, fI_x, fx_o, fh_x);
            float fBeta_3_temp = Eq_D121_7__(fI_xy, fx_o, fh_x);
            float fBeta_4_temp = Eq_D121_8__(fBeta_2_temp, fy_o, fh_y, fI_y, fBeta_3_temp);

            float fLambda_temp = Eq_D121_9__(fBeta_4_temp, fb_w, ft);
            float fEta_temp = Eq_D121_10_(fLambda_temp);

            float fAlpha_1_temp_D121_12 = Eq_D121_12_(fEta_temp, fBeta_1_temp, fBeta_2_temp, fJ, fLambda_temp);
            float fAlpha_3_temp_D121_12 = Eq_D121_4__(fEta_temp, fAlpha_1_temp_D121_12, fI_y, fBeta_1_temp, fBeta_3_temp);
            float fAlpha_2_temp = Eq_D121_3__(fEta_temp, fI_y, fy_o, fBeta_1_temp, fBeta_3_temp);

            float ff_od_par_temp = Eq_D121_1__(fE, fA, fAlpha_1_temp_D121_12, fAlpha_2_temp, fAlpha_3_temp_D121_12);

            float fk_Phi_temp = Eq_D121_11_(fE, ft, fb_w, fLambda_temp, ff_od_par_temp);

            float fAlpha_1_temp = Eq_D121_2__(fEta_temp, fBeta_1_temp, fBeta_2_temp, fJ, fLambda_temp, fk_Phi_temp, fE);
            float fAlpha_3_temp = Eq_D121_4__(fEta_temp, fAlpha_1_temp, fI_y, fBeta_1_temp, fBeta_3_temp);

            return (fE / (2.0f * fA)) * ((fAlpha_1_temp + fAlpha_2_temp) - MathF.Sqrt(MathF.Pow2(fAlpha_1_temp + fAlpha_2_temp) - 4.0f * fAlpha_3_temp)); // Eq. (D1.2.1(1)) // ff_od
        }

        // D 1.2.1.2 Simple lipped channels in compression
        public float Eq_D121_13_(float fE, float fA, float fAlpha_1, float fAlpha_2, float fAlpha_3)
        {
            return (fE / (2.0f * fA)) * ((fAlpha_1 + fAlpha_2) - MathF.Sqrt(MathF.Pow2(fAlpha_1 + fAlpha_2) - 4.0f * fAlpha_3)); // Eq. (D1.2.1(13)) // ff_od
        }
        public float Eq_D121_14_(float fEta, float fBeta_1, float fI_x, float fb_f, float fJ, float fLambda, float fk_Phi, float fE)
        {
            return ((fEta / fBeta_1) * (fI_x * MathF.Pow2(fb_f) + 0.039f * fJ * MathF.Pow2(fLambda))) + (fk_Phi / (fBeta_1 * fEta * fE)); // Eq. (D1.2.1(14)) // fAlpha_1
        }
        public float Eq_D121_15_(float fEta, float fI_y, float fBeta_1, float fy_par, float fb_f, float fI_xy)
        {
            return fEta * (fI_y + ((2.0f / fBeta_1) * fy_par * fb_f * fI_xy)); // Eq. (D1.2.1(15)) // fAlpha_2
        }
        public float Eq_D121_16_(float fEta, float fAlpha_1, float fI_y, float fBeta_1, float fI_xy, float fb_f)
        {
            return fEta * ((fAlpha_1 * fI_y) - ((fEta / fBeta_1) * MathF.Pow2(fI_xy) * MathF.Pow2(fb_f))); // Eq. (D1.2.1(16)) // fAlpha_3
        }
        public float Eq_D121_17_(float fx_par, float fI_x, float fI_y, float fA)
        {
            return MathF.Pow2(fx_par) + ((fI_x + fI_y) / fA); // Eq. (D1.2.1(17)) // fBeta_1
        }
        public float Eq_D121_18_(float fI_x, float fb_f, float fb_w, float ft)
        {
            return 4.8f * (float)Math.Pow(fI_x * MathF.Pow2(fb_f) * fb_w / MathF.Pow3(ft), 0.25f); // Eq. (D1.2.1(18)) // fLambda
        }
        public float Eq_D121_19_(float fLambda)
        {
            return MathF.Pow2(MathF.fPI / fLambda); // Eq. (D1.2.1(19)) // fEta
        }
        public float Eq_D121_20_(float fE, float ft, float fb_w, float fLambda, float ff_od_par)
        {
            return (fE * MathF.Pow3(ft) / (5.46f * (fb_w + 0.06f * fLambda))) * (1.0f - (((1.11f * ff_od_par) / (fE * MathF.Pow2(ft))) * ((MathF.Pow3(fb_w) * fLambda) / (MathF.Pow2(fb_w) + MathF.Pow2(fLambda))))); // Eq. (D1.2.1(20)) // fk_Phi
        }
        public float Eq_D121_12_(float fEta, float fBeta_1, float fI_x, float fb_f, float fJ, float fLambda)
        {
            return (fEta / fBeta_1) * (fI_x * MathF.Pow2(fb_f) + 0.039f * fJ * MathF.Pow2(fLambda)); // Eq. (D1.2.1(21)) // fAlpa_1 for calculation of f'od
        }
        public float Eq_D121_22_(float fb_f, float fd_l, float ft)
        {
            return (fb_f + fd_l) * ft; // Eq. (D1.2.1(22)) // fA
        }
        public float Eq_D121_23_(float fb_f, float fd_l)
        {
            return (MathF.Pow2(fb_f) + 2.0f * fb_f * fd_l) / (2.0f * (fb_f + fd_l)); // Eq. (D1.2.1(23)) // fx_par
        }
        public float Eq_D121_24_(float fb_f, float fd_l)
        {
            return (MathF.Pow2(fd_l)) / (2.0f * (fb_f + fd_l)); // Eq. (D1.2.1(24)) // fy_par
        }
        public float Eq_D121_25_(float fb_f, float fd_l, float ft)
        {
            return (MathF.Pow3(ft) * (fb_f + fd_l)) / 3.0f; // Eq. (D1.2.1(25)) // fJ
        }
        public float Eq_D121_26_(float fb_f, float fd_l, float ft, float fy_par)
        {
            return ((fb_f * MathF.Pow3(ft)) / 12.0f) + ((ft * MathF.Pow3(fd_l)) / 12.0f) + fb_f * ft * MathF.Pow2(fy_par) + fd_l * ft * MathF.Pow2((fd_l / 2.0f) - fy_par); // Eq. (D1.2.1(26)) // fI_x
        }
        public float Eq_D121_27_(float fb_f, float fd_l, float ft, float fx_par)
        {
            return ((ft * MathF.Pow3(fb_f)) / 12.0f) + ((fd_l * MathF.Pow3(ft)) / 12.0f) + fd_l * ft * MathF.Pow2(fb_f - fx_par) + fb_f * ft * MathF.Pow2(fx_par - (fb_f / 2.0f)); // Eq. (D1.2.1(27)) // fI_y
        }
        public float Eq_D121_28_(float fb_f, float fd_l, float ft, float fx_par, float fy_par)
        {
            return (fb_f  * ft * ((fb_f / 2.0f) - fx_par) * (-fy_par)) + (fd_l * ft * ((fd_l / 2.0f) - fy_par) * (fb_f - fx_par)); // Eq. (D1.2.1(28)) // fI_xy
        }
        public void Calc_CFL_Properties(float fb_f, float fd_l, float ft, out float fA, out float fx_par, out float fy_par, out float fJ, out float fI_x, out float fI_y, out float fI_xy)
        {
            fA = Eq_D121_22_(fb_f, fd_l, ft);
            fx_par = Eq_D121_23_(fb_f, fd_l);
            fy_par = Eq_D121_24_(fb_f, fd_l);
            fJ = Eq_D121_25_(fb_f, fd_l, ft);
            fI_x = Eq_D121_26_(fb_f, fd_l, ft, fy_par);
            fI_y = Eq_D121_27_(fb_f, fd_l, ft, fx_par);
            fI_xy = Eq_D121_28_(fb_f, fd_l, ft, fx_par, fy_par);
        }

        // Local buckling
        public float Eq_D131____(float fk, float fE, float fnu, float ft, float fb)
        {
            return ((fk * MathF.Pow2(MathF.fPI) * fE) / (12.0f * (1.0f - MathF.Pow2(fnu)))) * MathF.Pow2(ft / fb) ; // Eq. (D1.3.1) // ff_ol
        }

    }
}
