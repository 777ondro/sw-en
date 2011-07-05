using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M_EC8.SOLV.CS.EQ
{
    class CL_3
    {
        public float Eq_32______(float fa_g, float fS, float fT, float fT_B, float feta)
        {
            if (fT_B > 0f)
                return fa_g * fS * (1f + (fT / fT_B) * (feta * 2.5f - 1f)); // Eq. (3.2) Se(T)
            else
                return 0f;
        }
        public float Eq_33______(float fa_g, float fS, float feta)
        {
            return fa_g * fS * feta * 2.5f; // Eq. (3.3) Se(T)
        }
        public float Eq_34______(float fa_g, float fS, float feta, float fT_C, float fT)
        {
            if (fT > 0f)
                return fa_g * fS * feta * 2.5f * fT_C / fT; // Eq. (3.4) Se(T)
            else
                return 0f;
        }
        public float Eq_35______(float fa_g, float fS, float feta, float fT_C, float fT_D, float fT)
        {
            if (Math.Pow(fT, 2f) > 0f)
                return fa_g * fS * feta * 2.5f * ((fT_C * fT_D) / (float)Math.Pow(fT, 2f)); // Eq. (3.5) Se(T)
            else
                return 0f;
        }
        public float Eq_37______(float fS_e, float fT)
        {
            return fS_e * (fT) * (float)Math.Pow((fT / (2f * (float)Math.PI)), 2f); // Eq. (3.7) SDe(T)
        }
        public float Eq_38______(float fa_vg, float fT, float fT_B, float feta)
        {
            if (fT_B > 0f)
                return fa_vg * (1f + (fT / fT_B) * (feta * 3f - 1f)); // Eq. (3.8) Sve(T)
            else
                return 0f;
        }
        public float Eq_39______(float fa_vg, float feta)
        {
            return fa_vg * feta * 3f; // Eq. (3.9) Sve(T)
        }
        public float Eq_310_____(float fa_vg, float feta, float fT_C, float fT)
        {
            if (fT > 0f)
                return fa_vg * feta * 3f * fT_C / fT; // Eq. (3.10) Sve(T)
            else
                return 0f;
        }
        public float Eq_311_____(float fa_vg, float feta, float fT_C, float fT_D, float fT)
        {
            if (Math.Pow(fT, 2f) > 0f)
                return fa_vg * feta * 3f * ((fT_C * fT_D) / (float)Math.Pow(fT, 2f)); // Eq. (3.4) Se(T)
            else
                return 0f;
        }
        public float Eq_312_____(float fa_g, float fS, float fT_C, float fT_D)
        {
            return 0.025f * fa_g * fS * fT_C * fT_D; // Eq. (3.12) dg
        }



    }
}
