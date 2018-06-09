﻿using System;
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
        public float Eq_7222_1__(float fM_o)
        {
            return fM_o; // Eq. (7.2.2.2(1)) // fM_be
        }
        public float Eq_7222_2__(float fM_o, float fM_y)
        {
            return (10f / 9f) * fM_y * (1f - ((10 * fM_y) / (36 * fM_o))); // Eq. (7.2.2.2(2)) // fM_be
        }
        public float Eq_7222_3__(float fM_y)
        {
            return fM_y; // Eq. (7.2.2.2(3)) // fM_be
        }
        public float Eq_7222____(float fM_o, float fM_y)
        {
            if (fM_o < 0.56f * fM_y)
                return Eq_7222_1__(fM_o); // Eq. (7.2.2.2(1))
            else if(fM_o <= 2.78f * fM_y)
                return Eq_7222_2__(fM_o, fM_y); // Eq. (7.2.2.2(2))
            else
                return Eq_7222_3__(fM_y); // Eq. (7.2.2.2(3))
        }
        public float Eq_7222_4__(float fZ_f, float ff_y)
        {
            return fZ_f * ff_y; // Eq. (7.2.2.2(4)) // fM_y
        }
        public float Eq_7222_5__(float fM_p, float fM_y, float fM_o)
        {
            return MathF.Min(fM_p - (fM_p - fM_y) * ((MathF.Sqrt(fM_y / fM_o) - 0.23f) / 0.37f), fM_p); // Eq. (7.2.2.2(5)) // fM_be
        }
        public float Eq_7222_6__(float fS_f, float ff_y)
        {
            return fS_f * ff_y; // Eq. (7.2.2.2(6)) // fM_p
        }
        public float Eq_7223_1__(float fM_be)
        {
            return fM_be; // Eq. (7.2.2.3(1)) // fM_bl
        }
        public float Eq_7223_2__(float fM_ol, float fM_be)
        {
            return (1f - 0.15f * (float)Math.Pow(fM_ol / fM_be, 0.4f)) * (float)Math.Pow(fM_ol / fM_be, 0.4f) * fM_be; // Eq. (7.2.2.3(2)) // fM_bl
        }
        public float Eq_7223_3__(float fM_be, float fM_ol)
        {
            return MathF.Sqrt(fM_be / fM_ol); // Eq. (7.2.2.3(3)) // fLambda_l
        }
        public float Eq_7223_4__(float fZ_f, float ff_ol)
        {
            return fZ_f * ff_ol; // Eq. (7.2.2.3(4)) // fM_ol
        }
        public float Eq_7223_5__(float fM_p, float fM_y, float fC_yl)
        {
            return fM_y + (1.0f - (1.0f / MathF.Pow2(fC_yl))) * (fM_p - fM_y); // Eq. (7.2.2.3(5)) // fM_bl
        }
        public float Eq_7223_6__(float fM_p, float fM_yc, float fM_yt3, float fC_yl)
        {
            return MathF.Min(fM_yc + (1.0f - (1.0f / MathF.Pow2(fC_yl))) * (fM_p - fM_yc), fM_yt3); // Eq. (7.2.2.3(6)) // fM_bl
        }
        public float Eq_7223_7__(float fM_y, float fM_ol)
        {
            return MathF.Sqrt(fM_y / fM_ol); // Eq. (7.2.2.3(7)) // fLambda_l
        }
        public float Eq_7223_8__(float fLambda_l)
        {
            return MathF.Min(MathF.Sqrt(0.776f / fLambda_l), 3.0f); // Eq. (7.2.2.3(8)) // fC_yl
        }
        public float Eq_7223_9__(float fM_p, float fM_y, float fC_yt = 3f)
        {
            return fM_y + (1.0f - (1.0f / MathF.Pow2(fC_yt))) * (fM_p - fM_y); // Eq. (7.2.2.3(9)) // fM_yt3
        }
        public float Eq_7224_1__(float fM_y)
        {
            return fM_y; // Eq. (7.2.2.4(1)) // fM_bd
        }
        public float Eq_7224_2__(float fM_y, float fM_od)
        {
            return (1.0f - 0.22f * (float)Math.Pow(fM_od / fM_y, 0.5f)) * (float)Math.Pow(fM_od / fM_y, 0.5f) * fM_y; // Eq. (7.2.2.4(1)) // fM_bd
        }
        public float Eq_7224____(float fM_y, float fM_od, float fLambda_d)
        {
            if (fLambda_d < 0.673f)
                return Eq_7224_1__(fM_y); // Eq. (7.2.2.4(1))
            else
                return Eq_7224_2__(fM_y, fM_od); // Eq. (7.2.2.4(2))
        }
        public float Eq_7224_3__(float fM_y, float fM_od)
        {
            return MathF.Sqrt(fM_y / fM_od); // Eq. (7.2.2.4(3)) // fLambda_d
        }
        public float Eq_7224_4__(float fZ_f, float ff_od)
        {
            return fZ_f * ff_od; // Eq. (7.2.2.4(4)) // fM_od
        }
        public float Eq_7224_5__(float fM_p, float fM_y, float fC_yd)
        {
            return fM_y + (1.0f - (1.0f / MathF.Pow2(fC_yd))) * (fM_p - fM_y); // Eq. (7.2.2.4(5)) // fM_bd
        }
        public float Eq_7224_6__(float fM_p, float fM_yc, float fM_yt3, float fC_yd)
        {
            return MathF.Min(fM_yc + (1.0f - (1.0f / MathF.Pow2(fC_yd))) * (fM_p - fM_yc), fM_yt3); // Eq. (7.2.2.4(6)) // fM_bd
        }
        public float Eq_7224_7__(float fM_y, float fM_od)
        {
            return MathF.Sqrt(fM_y / fM_od); // Eq. (7.2.2.4(7)) // fLambda_d
        }
        public float Eq_7224_8__(float fLambda_d)
        {
            return MathF.Min(MathF.Sqrt(0.673f / fLambda_d), 3.0f); // Eq. (7.2.2.4(8)) // fC_yd
        }
        public float Eq_7224_9__(float fM_p, float fM_y, float fC_yt = 3f)
        {
            return fM_y + (1.0f - (1.0f / MathF.Pow2(fC_yt))) * (fM_p - fM_y); // Eq. (7.2.2.4(9)) // fM_yt3
        }
        public float Eq_723_1___(float fV_y)
        {
            return fV_y; // Eq. (7.2.3(1)) // fV_v
        }
        public float Eq_723_2___(float fV_y, float fV_cr)
        {
            return 0.815f * MathF.Sqrt(fV_cr * fV_y); // Eq. (7.2.3(2)) // fV_v
        }
        public float Eq_723_3___(float fV_cr)
        {
            return fV_cr; // Eq. (7.2.3(3)) // fV_v
        }
        public float Eq_7232____(float fV_y, float fV_cr, float fLambda_v)
        {
            if (fLambda_v <= 0.815f)
                return Eq_723_1___(fV_y); // Eq. (7.2.3(1))
            else if (fLambda_v <= 1.227f)
                return Eq_723_2___(fV_y, fV_cr); // Eq. (7.2.3(2))
            else
                return Eq_723_3___(fV_cr); // Eq. (7.2.3(3))
        }
        public float Eq_723_4___(float fV_y, float fV_cr)
        {
            return MathF.Sqrt(fV_y / fV_cr); // Eq. (7.2.3(4)) // fLambda_v
        }
        public float Eq_723_5___(float fA_w, float ff_y)
        {
            return 0.6f * fA_w * ff_y; // Eq. (7.2.3(5)) // fV_y
        }
        public float Eq_723_6___(float fh, float ft)
        {
            return fh * ft; // Eq. (7.2.3(6)) // fA_w
        }
        public float Eq_723_7___(float fV_y)
        {
            return fV_y; // Eq. (7.2.3(7)) // fV_v
        }
        public float Eq_723_8___(float fV_y, float fV_cr)
        {
            return (1.0f - 0.15f * (float)Math.Pow(fV_cr / fV_y, 0.4f)) * (float)Math.Pow(fV_cr / fV_y, 0.4f) * fV_y; // Eq. (7.2.3(8)) // fV_v
        }
        public float Eq_7233____(float fV_y, float fV_cr, float fLambda_v)
        {
            if (fLambda_v <= 0.776f)
                return Eq_723_7___(fV_y); // Eq. (7.2.3(7))
            else
                return Eq_723_8___(fV_y, fV_cr); // Eq. (7.2.3(8))
        }
        public void Eq_723_9___(float fM_asterix, float fPhi_b, float fM_s, float fV_asterix, float fPhi_v, float fV_v, out float fPortion_M, out float fPortion_V, out float fRatio)
        {
            fPortion_M = MathF.Pow2(fM_asterix / (fPhi_b * fM_s));
            fPortion_V = MathF.Pow2(fV_asterix / (fPhi_v * fV_v));
            fRatio = fPortion_M + fPortion_V; // Eq. (7.2.3(9))
        }
        public void Eq_723_10__(float fM_asterix, float fPhi_b, float fM_b, out float fResistance_M, out float fRatio)
        {
            fResistance_M =  fPhi_b * fM_b;
            fRatio = Math.Abs(fM_asterix) / fResistance_M; // Eq. (7.2.3(10))
        }
        public void Eq_723_11__(float fV_asterix, float fPhi_v, float fV_v, out float fResistance_V, out float fRatio)
        {
            fResistance_V = fPhi_v * fV_v;
            fRatio = Math.Abs(fV_asterix) / fResistance_V; // Eq. (7.2.3(11))
        }
        public void Eq_723_12__(float fM_asterix, float fPhi_b, float fM_s, float fV_asterix, float fPhi_v, float fV_v, out float fPortion_M, out float fPortion_V, out float fRatio_13, out float fRatio_10)
        {
            fPortion_M = MathF.Pow2(fM_asterix / (fPhi_b * fM_s));
            fPortion_V = MathF.Pow2(fV_asterix / (fPhi_v * fV_v));
            fRatio_13 = 0.6f * fPortion_M + fPortion_V; // Eq. (7.2.3(12))
            fRatio_10 = ((0.6f * fPortion_M) + fPortion_V) / 1.3f; // Eq. (7.2.3(12))
        }
        public void Eq_724_____(float fPhi_c, float fPhi_b, float fN_asterix, float fN_c, float fM_x_asterix,  float fM_bx, float fM_y_asterix, float fM_by, out float fPortion_N, out float fPortion_Mx, out float fPortion_My, out float fRatio)
        {
            fPortion_N = Math.Abs(fN_asterix) / (fPhi_c * fN_c);
            fPortion_Mx = Math.Abs(fM_x_asterix) / (fPhi_b * fM_bx);
            fPortion_My = Math.Abs(fM_y_asterix) / (fPhi_b * fM_by);
            fRatio = fPortion_N + fPortion_Mx + fPortion_My; // Eq. (7.2.4)
        }
        public void Eq_725_1___(float fPhi_t, float fPhi_b, float fN_asterix, float fN_t, float fM_x_asterix, float fM_bx, float fM_y_asterix, float fM_by, out float fPortion_N, out float fPortion_Mx, out float fPortion_My, out float fRatio)
        {
            fPortion_N = Math.Abs(fN_asterix) / (fPhi_t * fN_t);
            fPortion_Mx = Math.Abs(fM_x_asterix) / (fPhi_b * fM_bx);
            fPortion_My = Math.Abs(fM_y_asterix) / (fPhi_b * fM_by);
            fRatio = - fPortion_N + fPortion_Mx + fPortion_My; // Eq. (7.2.5(1))
        }
        public void Eq_725_2___(float fPhi_t, float fPhi_b, float fN_asterix, float fN_t, float fM_x_asterix, float fM_sxf, float fM_y_asterix, float fM_syf, out float fPortion_N, out float fPortion_Mx, out float fPortion_My, out float fRatio)
        {
            fPortion_N = Math.Abs(fN_asterix) / (fPhi_t * fN_t);
            fPortion_Mx = Math.Abs(fM_x_asterix) / (fPhi_b * fM_sxf);
            fPortion_My = Math.Abs(fM_y_asterix) / (fPhi_b * fM_syf);
            fRatio = fPortion_N + fPortion_Mx + fPortion_My; // Eq. (7.2.5(2))
        }
        public float Eq_725_3___(float fZ_ft, float ff_y)
        {
            return fZ_ft * ff_y; // Eq. (7.2.5(3)) // fM_sxf, fM_syf
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

        // D2 Members in bending
        // D2.1 Global buckling moments
        public float Eq_D211_1__(float fC_b, float fA_g, float fr_o1, float ff_oy, float ff_oz)
        {
            return fC_b * fA_g * fr_o1 * MathF.Sqrt(ff_oy * ff_oz); // Eq. (D2.1.1(1)) // fM_o
        }
        public float Eq_D211_2__(float fM_max, float fM_3, float fM_4, float fM_5)
        {
            return (12.5f * Math.Abs(fM_max)) / (2.5f * Math.Abs(fM_max) + 3.0f * Math.Abs(fM_3) + 4.0f * Math.Abs(fM_4) + 3.0f * Math.Abs(fM_5)); // Eq. (D2.1.1(2)) // fC_b
        }
        public float Eq_D211_3__(float fr_x, float fr_y, float fx_o, float fy_o)
        {
            return MathF.Sqrt(MathF.Pow2(fr_x) + MathF.Pow2(fr_y) + MathF.Pow2(fx_o) + MathF.Pow2(fy_o)); // Eq. (D2.1.1(3)) // fr_o1
        }
        public float Eq_D211_4__(float fC_s, float fC_TF, float fA, float fr_o1, float ff_ox, float ff_oz, float fBeta_y)
        {
            return ((fC_s * fA * ff_ox) * ((fBeta_y / 2.0f) + fC_s * MathF.Sqrt(MathF.Pow2(fBeta_y / 2.0f) + ((MathF.Pow2(fr_o1) * ff_oz) / ff_ox)))) / fC_TF; // Eq. (D2.1.1(4)) // fM_o
        }
        public float Eq_D211_5__(float fM_1, float fM_2)
        {
            return 0.6f - 0.4f * (fM_1 / fM_2); // Eq. (D2.1.1(5)) // fC_TF
        }
        public float Eq_D211_7__(float fE, float fC_b, float fd, float fI_yc, float fl)
        {
            return MathF.Pow2(MathF.fPI)  * fE * fC_b * fd * fI_yc / (2.0f * MathF.Pow2(fl)); // Eq. (D2.1.1(7)) // fM_o
        }
        public float Eq_D221_1__(float fI_x, float fb_f, float fb_w, float ft)
        {
            return 4.8f * (float)Math.Pow((fI_x * MathF.Pow2(fb_f) * fb_w) / (2.0f * MathF.Pow3(ft)), 0.25f); // Eq. (D2.2.1(1)) // fLambda
        }
        public float Eq_D221_2__(float fE, float fb_w, float fLambda, float ft, float f_od_par)
        {
            return ((2.0f * fE * MathF.Pow3(ft)) / (5.46f * (fb_w + 0.06f * fLambda))) * ((1.0f - (1.11f * f_od_par / (fE * MathF.Pow2(ft)))) * (((MathF.Pow4(fb_w) * MathF.Pow2(fLambda)) / (12.56f * MathF.Pow4(fLambda) + 2.192f * MathF.Pow4(fb_w) + 13.39f * MathF.Pow2(fLambda) * MathF.Pow2(fb_w))))); // Eq. (D2.2.1(2)) // fk_Phi
        }
        public float Eq_D221_3__(float fEta, float fBeta_1, float fI_x, float fJ, float fLambda, float fb_f)
        {
            return ((fEta / fBeta_1) * (fI_x * MathF.Pow2(fb_f) + 0.039f * fJ * MathF.Pow2(fLambda))); // Eq. (D2.2.1(3)) // fAlpha_1
        }

        // D3 Members in shear
        public float Eq_D3_1____(float fE, float fA_w, float fk_v, float fNu, float fd_1, float ft)
        {
            return (MathF.Pow2(MathF.fPI) * fE * fA_w * fk_v) / (12.0f * (1.0f - MathF.Pow2(fNu) * MathF.Pow2(fd_1 / ft))); // Eq. (D3(1)) // fV_cr
        }
        public float Eq_D3_2____(float fa, float fd_1)
        {
            return 4.00f + (5.34f / MathF.Pow2(fa / fd_1)); // Eq. (D3(2)) // fk_v
        }
        public float Eq_D3_3____(float fa, float fd_1)
        {
            return 5.34f + (4.00f / MathF.Pow2(fa / fd_1)); // Eq. (D3(3)) // fk_v
        }
        public float Eq_D3_4____(float fk_ss, float fk_n, float fk_sf)
        {
            return fk_ss + fk_n * (fk_sf - fk_ss); // Eq. (D3(4)) // fk_v
        }
        public float Eq_D3_5____(float fa, float fd_1)
        {
            return 4.00f + (5.34f / MathF.Pow2(fa / fd_1)); // Eq. (D3(5)) // fk_ss
        }
        public float Eq_D3_6____(float fa, float fd_1)
        {
            return 5.34f + (4.00f / MathF.Pow2(fa / fd_1)); // Eq. (D3(6)) // fk_ss
        }
        public float Eq_D3_7____(float fa, float fd_1)
        {
            return (5.34f / MathF.Pow2(fa / fd_1)) + (2.31f / (fa / fd_1)) - 3.44f + 8.39f * (fa / fd_1); // Eq. (D3(7)) // fk_sf
        }
        public float Eq_D3_8____(float fa, float fd_1)
        {
            return 8.98f + (5.61f / MathF.Pow2(fa / fd_1)) - (1.99f / MathF.Pow3(fa / fd_1)); // Eq. (D3(8)) // fk_sf
        }
    }
}
