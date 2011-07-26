using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace M_EC1.EC_1_4
{
    public class EC_1_4
    {
        public float Eq_41______(float fc_dir, float fc_season, float fv_b_0)
        {
            return fc_dir * fc_season * fv_b_0; // Eq. (4.1) // fv_b
        }
        public float Eq_42______(float fK, float fp, float fn)
        {
            return (float)Math.Pow((1f - fK * MathF.Ln(-MathF.Ln(1f - fp))) / (1f - fK * MathF.Ln(-MathF.Ln(0.98f))), fn); // Eq. (4.2) // fc_prob
        }
        public float Eq_43______(float fc_r_z, float fc_0_z, float fv_b)
        {
            return fc_r_z * fc_0_z * fv_b; // Eq. (4.3) // fv_m
        }
        public float Eq_44______(float fz_min, float fz_max, float fz, float fz_0, float fk_r, float fc_r_z_min)
        {
            // Eq. (4.4) // fc_r_z

            if (fz_min <= fz && fz <= fz_max)
                return fk_r * MathF.Ln(fz / fz_0);
            else if (fz < fz_min)
                return fc_r_z_min;
            else
                return 0f;
        }
        public float Eq_45______(float fz_0, float fz_0_II)
        {
            return 0.19f * (float)Math.Pow((fz_0 / fz_0_II), 0.07f); // Eq. (4.5) // fk_r 
        }
        public float Eq_46______(float fk_r, float fv_b, float fk_l)
        {
            return fk_r * fv_b * fk_l; // Eq. (4.6) //  fSigma_v
        }
        public float Eq_47______(float fk_l, float fc_0_z_min, float fz_min, float fz_0)
        {
            return fk_l / (fc_0_z_min * MathF.Ln(fz_min / fz_0)); // Eq. (4.7) // fl_v_z_min
        }
        public float Eq_48______(float fl_v_z, float fRho_air, float fv_m)
        {
            return (1f + 7f * fl_v_z) * 0.5f * fRho_air * (float)MathF.Pow2(fv_m); // Eq. (4.8) // fq_p_z
        }
        public float Eq_49______(float fq_p_z, float fq_b)
        {
            return fq_p_z / fq_b; // Eq. (4.9) // fC_e_z
        }
        public float Eq_410_____(float fRho_air, float fv_b)
        {
            return 0.5f * fRho_air * MathF.Pow2(fv_b); // Eq. (4.10) // fq_b
        }
        public float Eq_51________(float fq_p, float fc_pe)
        {
            return fq_p * fc_pe; // Eq (5.1) // fw_e
        }
        public float Eq_52________(float fq_p, float fc_pi)
        {
            return fq_p * fc_pi; // Eq (5.2) // fw_i
        }
        public float Eq_53________(float fc_s, float fc_d, float fc_f, float fq_p, float fA_ref)
        {
            return fc_s * fc_d * fc_f * fq_p * fA_ref; //Eq (5.3) // fF_w 
        }
        public float Eq_54________(float fc_s, float fc_d, float fSigma_c_f, float fq_p, float fA_ref)
        {
            return fc_s * fc_d * fSigma_c_f * fq_p * fA_ref; // Eq (5.4) // fF_w
        }
        public float Eq_55________(float fc_s, float fc_d, float fSigma_w_e, float fA_ref)
        {
            return fc_s * fc_d * fSigma_w_e * fA_ref; // Eq (5.5) // fF_w_e
        }
        public float Eq_56________(float fSigma_w_i, float fA_ref)
        {
            return fSigma_w_i * fA_ref; // Eq (5.6) // fF_w_i
        }
        public float Eq_57________(float fc_fr, float fq_p, float fA_f)
        {
            return fc_fr * fq_p * fA_f; // Eq (5.7) // fF_fr 
        }
        public float Eq_61________(float fk_p, float fl_v, float fB_2, float fR_2)
        {
            return (1f + 2f * fk_p * fl_v * MathF.Sqrt(fB_2 + fR_2)) / 1f + 7f * fl_v; // Eq (6.1) // fc_s_c_d 
        }
        public float Eq_62________(float fk_p, float fl_v, float fB_2)
        {
            return (1f + 2f * fk_p * fl_v * MathF.Sqrt(fB_2)) / (1f + 7f * fl_v); // Eq (6.2) // fc_s
        }
        public float Eq_63________(float fk_v, float fl_v, float fB_2, float fR_2)
        {
            return (1f + 2f * fk_v * fl_v * MathF.Sqrt(fB_2 + fR_2)) / (1f + 7f * fl_v * MathF.Sqrt(fB_2)); // Eq (6.3) // fc_d 
        }
        public float Eq_71________(float fc_pe)
        {
            return 0.75f * fc_pe; // Eq (7.1) // fc_pi
        }
        public float Eq_72________(float fc_pe)
        {
            return 0.90f * fc_pe; // Eq (7.2) // fc_pi
        }
        public float Eq_76________(float fPsi_s, float fc_p_net)
        {
            return fPsi_s * fc_p_net; // Eq (7.6) // fc_p_net_s
        }
        public float Eq_79________(float fc_f_0, float fPsi_r, float fPsi_Lambda)
        {
            return fc_f_0 * fPsi_r * fPsi_Lambda; // Eq (7.9) // fc_f
        }
        public float Eq_710________(float fl, float fb)
        {
            return fl * fb; // Eq (7.10) // fA_ref
        }
        public float Eq_711________(float fc_f_0, float fPsi_Lambda)
        {
            return fc_f_0 * fPsi_Lambda; // Eq (7.11) // fc_f
        }
        public float Eq_712________(float fl, float fb)
        {
            return fl * fb; // Eq (7.12) // fA_ref_x
        }
        public float Eq_713________(float fc_f_0, float fPsi_Lambda)
        {
            return fc_f_0 * fPsi_Lambda; // Eq (7.13) // fc_f
        }
        public float Eq_714________(float fl, float fb)
        {
            return fl * fb; // Eq (7.14) // fA_ref
        }
        public float Eq_715________(float fb, float fv, float fNu)
        {
            return fb * fv / fNu; // Eq (7.15) // fRe
        }
        public float Eq_716_________(float fc_p_0, float fPsi_Lambda_Alpha)
        {
            return fc_p_0 * fPsi_Lambda_Alpha; // Eq (7.16) // fc_pe 
        }
        public float Eq_718________(float fl, float fb)
        {
            return fl * fb; // Eq (7.18) // fA_ref
        }
        public float Eq_719_________(float fc_f_0, float fPsi_Lambda)
        {
            return fc_f_0 * fPsi_Lambda; // Eq (7.19) // fc_f
        }
        public float Eq_720________(float fl, float fb)
        {
            return fl * fb; // Eq (7.20) // fA_ref
        }
        public float Eq_721________(float fc_f_0, float fPsi_Lambda, float fK)
        {
            return fc_f_0 * fPsi_Lambda * fK;  // Eq (7.21) // fc_f
        }
        public float Eq_723________(float fPi, float fb)
        {
            return fPi * MathF.Pow2(fb) / 4f; // Eq (7.23) // fA_ref 
        }
        public float Eq_724________(float fz_g, float fb)
        {
            return fz_g + fb / 2f; // (7.24) // fz_e 
        }
        public float Eq_725_______(float fc_f_0, float fPsi_Lambda)
        {
            return fc_f_0 * fPsi_Lambda; // Eq (7.25) // fc_f 
        }
        public float Eq_726________(float fA, float fA_c)
        {
            return fA / fA_c; // Eq (7.26) // fPhi 
        }
        public float Eq_728________(float fA, float fA_c)
        {
            return fA / fA_c; // Eq (7.28) // fPhi 
        }
        public float Eq_82________(float fRho, float fv_b, float fc, float fA_ref_x)
        {
            return 0.5f * fRho * MathF.Pow2(fv_b) * fc * fA_ref_x; // Eq (8.2) // fF_w
        }
        public float Eq_83________(float fb, float fl)
        {
            return fb * fl; // Eq (8.3) // fA_ref_z
        }


        // Pokracovanie


    }
}
