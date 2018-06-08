using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MATH;
using MATERIAL;
using CRSC;

namespace M_AS4600
{
    public class CCalcul
    {
        CCrSc_TW cs; // Thin-walled cross-section

        float fN_c = 50f;
        float ff_y;
        float fE;
        float fG;
        float fNu;

        float fA_g;
        float ff_oc;
        float flambda_c;
        float fN_y;
        float fN_oc;
        float fN_ce;

        float ff_oz; //z = x
        float ff_ox; //x = y
        float ff_oy; //y = z

        float fr_x, fr_y, fr_o1, fx_o, fy_o;

        float ff_ol;
        float flambda_l;
        float fN_ol;
        float fN_cl;

        float ff_od;
        float flambda_d;
        float fN_od;
        float fN_cd;

        float fb, ft;
        float fd_l = 0.0f;

        float fk;

        float fx_cfl_par, fy_cfl_par;

        public float fA_cfl, fJ_cfl, fI_x_cfl, fI_y_cfl, fI_xy_cfl, fI_w_cfl;

        public CCalcul(CCrSc_TW cs_temp)
        {
            AS_4600 eq = new AS_4600();

            cs = cs_temp;

            //Set cross-section properties
            fb = (float)cs.b;
            ft = (float)cs.t_min;
            fA_g = (float)cs.A_g;

            // Set material properties
            ff_y = 5e+8f; // Temporary // Pa
            fE = cs.m_Mat.m_fE;
            fG = cs.m_Mat.m_fG;
            fNu = cs.m_Mat.m_fNu;

            float fa_CEQ = 0f;
            float fb_CEQ = 0f;
            float fc_CEQ = 0f;
            float fd_CEQ = 0f;

            float fl_ez = 5f;

            float fl_ex = 5f;
            float fl_ey = 5f;

            fx_o = (float)cs.D_y_s;
            fy_o = (float)cs.D_z_s;

            fr_x = (float)cs.i_y_rg;
            fr_y = (float)cs.i_z_rg;

            //float fr_o1 = cs.i_yz_rg;
            fr_o1 = eq.Eq_D111_6__(fr_x, fr_y, fx_o, fy_o);

            ff_oz = eq.Eq_D111_5__(fG, fE, (float)cs.I_t, (float)cs.I_w, fA_g, fl_ez, fr_o1);

            ff_ox = eq.Eq_D111_3__(fE, fl_ex, fr_x);
            ff_oy = eq.Eq_D111_3__(fE, fl_ey, fr_y);

            eq.Eq_D111_9__(ff_oz, ff_ox, ff_oy, fr_o1, fx_o, fy_o, out fa_CEQ, out fb_CEQ, out fc_CEQ, out fd_CEQ);
            CCardanoCubicEQSolver cubic_solver = new CCardanoCubicEQSolver(fa_CEQ, fb_CEQ, fc_CEQ, fd_CEQ);

            float ff_oc_real_1 = (float)cubic_solver.x_min_positive;

            ff_oc = (float)cubic_solver.x_min_positive > 0 ? (float)cubic_solver.x_min_positive : 0f;

            //if(ff_oc <= 0f)
            // Error  

            // 7.2.1.2.1 Compression members without holes
            fN_y = eq.Eq_7212_5__(fA_g, ff_y);
            fN_oc = eq.Eq_7212_4__(fA_g, ff_oc);
            flambda_c = eq.Eq_7212_3__(fN_y, fN_oc);
            fN_ce = eq.Eq_7212_1__(flambda_c, fN_y);

            // 7.2.1.3 Local buckling
            // 7.2.1.3.1 Compression members without holes

            fk = 4.0f; //see kst

            ff_ol = eq.Eq_D131____(fk, fE, fNu, ft, fb);
            fN_ol = eq.Eq_7213_4__(fA_g, ff_ol);
            flambda_l = eq.Eq_7213_3__(fN_ce, fN_ol);
            fN_cl = eq.Eq_7213____(flambda_l, fN_ol, fN_ce);

            // 7.2.1.4 Distorsial buckling
            // 7.2.1.4.1 Compression members without holes

            // General channel in compression (picture D2(a))

            eq.Calc_CFL_Properties(fb, fd_l, ft, out fA_cfl, out fx_cfl_par, out fy_cfl_par, out fJ_cfl, out fI_x_cfl, out fI_y_cfl, out fI_xy_cfl);
            // The values of A, J, Ix, Iy, Ixy, Iw are for the compression flange and lip alone.
            float fh_x = fb;
            float fh_y = 0.0f; // No lip
            float fb_w = fb; // ???

            ff_od = eq.Eq_D121_1__(fE, fA_cfl, fI_x_cfl, fI_y_cfl, fI_xy_cfl, fJ_cfl, fI_w_cfl, fx_o, fy_o, fh_x, fh_y, fb_w, ft);
            fN_od = eq.Eq_7214_4__(fA_g, ff_od);
            flambda_d = eq.Eq_7214_3__(fN_ce, fN_ol);
            fN_cd = eq.Eq_7214____(flambda_d, fN_y, fN_od);

            float fN_c_min = MathF.Min(fN_ce, fN_cl, fN_cd);

            float fDesignRatio = Math.Abs(fN_c / fN_c_min);

            MessageBox.Show("Calculation finished.\n"
                          + "Design Ratio η = "+ fDesignRatio + " [-]");
        }
    }
}
