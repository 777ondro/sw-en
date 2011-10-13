using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_50 : CCrSc
    {
        // Welded doubly symmetric I section

        /*
         
        
         
         
        1  ________________  2       ____|/
          |_______  _______|    t_f     /|
       12    11  | | 4       3           |
                 | |                     |
                 | |                     |
                 | |  t_w             h  |
                 |*|                     |
                 | |                     |
             10  | | 5       6           |
        9  ______| |_______              |
          |________________|         ____|/
        8                    7          /|
                  b
          |/_______________|/
         /|               /|
      
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */


        //----------------------------------------------------------------------------
        private float m_fh;   // Height / Vyska
        private float m_fb;   // Width  / Sirka
        private float m_ft_f; // Flange Thickness / Hrubka pasnice
        private float m_ft_w; // Web Thickness  / Hrubka steny/stojiny
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fh
        {
            get { return m_fh; }
            set { m_fh = value; }
        }
        public float Fb
        {
            get { return m_fb; }
            set { m_fb = value; }
        }
        public float Ft_f
        {
            get { return m_ft_f; }
            set { m_ft_f = value; }
        }
        public float Ft_w
        {
            get { return m_ft_w; }
            set { m_ft_w = value; }
        }
        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }
        /*
        public float[,] CrScPoint
        {
            get { return m_CrScPoint; }
            set { m_CrScPoint = value; }
        }
        */

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_50()  {   }
        public CCrSc_0_50(float fh, float fb, float ft_f, float ft_w)
        {
            m_iTotNoPoints = 12;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_I_DS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0,0] = -m_fb / 2f;    // y
            m_CrScPoint[0,1] = m_fh / 2f;     // z

            // Point No. 2
            m_CrScPoint[1,0] = -m_CrScPoint[0,0];    // y
            m_CrScPoint[1,1] = m_CrScPoint[0,1];     // z

            // Point No. 3
            m_CrScPoint[2,0] = -m_CrScPoint[0,0];             // y
            m_CrScPoint[2,1] = m_CrScPoint[0,1] - m_ft_f;     // z

            // Point No. 4
            m_CrScPoint[3,0] = m_CrScPoint[2,0] - (m_fb-m_ft_w)/2f;    // y
            m_CrScPoint[3,1] = m_CrScPoint[2,1];                       // z

            // Point No. 5
            m_CrScPoint[4,0] = m_CrScPoint[3,0];      // y
            m_CrScPoint[4,1] = -m_CrScPoint[3,1];     // z

            // Point No. 6
            m_CrScPoint[5,0] = -m_CrScPoint[0,0];     // y
            m_CrScPoint[5,1] = -m_CrScPoint[3,1];     // z

            // Point No. 7
            m_CrScPoint[6,0] = -m_CrScPoint[0,0];     // y
            m_CrScPoint[6,1] = -m_CrScPoint[0,1];     // z

            // Point No. 8
            m_CrScPoint[7,0] = m_CrScPoint[0,0];      // y
            m_CrScPoint[7,1] = -m_CrScPoint[0,1];     // z

            // Point No. 9
            m_CrScPoint[8,0] = m_CrScPoint[0,0];     // y
            m_CrScPoint[8,1] = -m_CrScPoint[3,1];     // z

            // Point No. 10
            m_CrScPoint[9,0] = -m_CrScPoint[4,0];     // y
            m_CrScPoint[9,1] = -m_CrScPoint[3,1];     // z

            // Point No. 11
            m_CrScPoint[10,0] = -m_CrScPoint[4,0];    // y
            m_CrScPoint[10,1] = m_CrScPoint[2,1];     // z

            // Point No. 12
            m_CrScPoint[11,0] = m_CrScPoint[0,0];    // y
            m_CrScPoint[11,1] = m_CrScPoint[2,1];     // z
        }

        // Welded monosymmetric I section

        /*
         
                 b_fu
          |/_______________|/
         /|               /| 
         
         
        1  ________________  2            ____|/
          |_______  _______|    t_fu         /|
       12    11  | | 4       3                |
                 | |                          |
                 | |                          |
                 | |  t_w                  h  |
                 | |                          |
                 |*|                  ____|/  |
             10  | | 5       6           /|   |
     9  _________| |_________         z_c |   |
       |_____________________|  t_fb  ____|/__|/
      8                    7             /|  /|
                  b_fb
       |/____________________|/
      /|                    /|
     
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */

        private float m_fb_fu;   // Width of Upper Flange / Sirka hornej pasnice
        private float m_ft_fu;   // Upper Flange Thickness / Hrubka hornej pasnice
        private float m_fb_fb;   // Width of Bottom Flange / Sirka spodnej pasnice
        private float m_ft_fb;   // Bottom Flange Thickness / Hrubka spodnej pasnice
        private float m_fz_c;    // Centroid coordinate / Suradnica tažiska / Absolute value

        public CCrSc_0_50(float fh, float fb_fu, float fb_fb, float ft_fu,float ft_fb, float ft_w, float fz_c)
        {
            m_iTotNoPoints = 12;
            m_fh = fh;
            m_fb_fu = fb_fu;
            m_fb_fb = fb_fb;
            m_ft_fu = ft_fu;
            m_ft_fb = ft_fb;
            m_ft_w = ft_w;
            m_fz_c = Math.Abs(fz_c);

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord_I_MS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_MS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fb_fu / 2f;    // y
            m_CrScPoint[0, 1] = m_fh - m_fz_c;    // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_CrScPoint[0, 0];    // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];     // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[0, 0];              // y
            m_CrScPoint[2, 1] = m_CrScPoint[0, 1] - m_ft_fu;     // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[2, 0] - (m_fb_fu - m_ft_w) / 2f;    // y
            m_CrScPoint[3, 1] = m_CrScPoint[2, 1];                              // z

            // Point No. 5
            m_CrScPoint[4, 0] = m_CrScPoint[3, 0];      // y
            m_CrScPoint[4, 1] = -m_fz_c + m_ft_fb;      // z

            // Point No. 6
            m_CrScPoint[5, 0] = m_fb_fb / 2f;           // y
            m_CrScPoint[5, 1] = m_CrScPoint[4, 1];      // z

            // Point No. 7
            m_CrScPoint[6, 0] = m_CrScPoint[5, 0];      // y
            m_CrScPoint[6, 1] = -m_fz_c;                // z

            // Point No. 8
            m_CrScPoint[7, 0] = -m_CrScPoint[6, 0];     // y
            m_CrScPoint[7, 1] = m_CrScPoint[6, 1];      // z

            // Point No. 9
            m_CrScPoint[8, 0] = -m_CrScPoint[5, 0];     // y
            m_CrScPoint[8, 1] = m_CrScPoint[5, 1];      // z

            // Point No. 10
            m_CrScPoint[9, 0] = -m_CrScPoint[4, 0];     // y
            m_CrScPoint[9, 1] = m_CrScPoint[4, 1];      // z

            // Point No. 11
            m_CrScPoint[10, 0] = -m_CrScPoint[3, 0];    // y
            m_CrScPoint[10, 1] = m_CrScPoint[3, 1];     // z

            // Point No. 12
            m_CrScPoint[11, 0] = -m_CrScPoint[2, 0];    // y
            m_CrScPoint[11, 1] = m_CrScPoint[2, 1];     // z
        }


        //----------------------------------------------------------------------------
        // Cross-section properties
        //----------------------------------------------------------------------------

        // Parallel-faced flanges (flat)
        // Doubly symmetrical

        float m_fU, m_fA,
                m_fS_y, m_fI_y, m_fW_y_el, m_fW_y_pl, m_ff_y_plel,
                m_fS_z, m_fI_z, m_fW_z_el, m_fW_z_pl, m_ff_z_plel,
                m_fW_t_el, m_fI_t, m_fi_t, m_fW_t_pl, m_ff_t_plel,
                m_fEta_y_v, m_fA_y_v_el, m_fA_y_v_pl, m_ff_y_v_plel,
                m_fEta_z_v, m_fA_z_v_el, m_fA_z_v_pl, m_ff_z_v_plel;

        float m_fh_i, // Web Depth (distance between flange faces)
              m_fd,   // Web Depth (straight part)
              m_fr,   // Radius between faces of web and flange
              m_fI_w; // Section warping constant

        // Web Depth (distance between flange faces)
        void Calc_h_i()
        {
            m_fh_i = m_fh - 2 * m_ft_f;
        }

        // Web Depth (straight part
        void Calc_d()
        {
            m_fd = m_fh - 2 * m_ft_f - 2 * m_fr;
        }

        // Perimeter of section
        void Calc_U()
        {
            m_fU = 4 * m_fb + 2 * m_fh - 2 * m_ft_w + (2 * MathF.fPI - 8) * m_fr;
        }
        // Section area
        void Calc_A()
        {
            m_fA = 2 * m_ft_f * m_fb + (m_fh - 2 * m_ft_f) * m_ft_w + (4 - MathF.fPI) * m_fr * m_fr;
        }


        // First moment o area
        void Calc_S_y()
        {
            m_fS_y = m_ft_f * m_fb * (m_fh / 2 - m_ft_f / 2) + m_ft_w * (m_fh / 2 - m_ft_f) * (m_fh / 2 - m_ft_f) / 2 + (((4 - MathF.fPI) * m_fr * m_fr) / 2) * (m_fh / 2 - m_ft_f - 0.4468f * m_fr);
        }
        // Second moment of area
        void Calc_I_y()
        {
            m_fI_y = 1 / 12f * (m_fb * m_fh * m_fh * m_fh - (m_fb - m_ft_w) * MathF.Pow3(m_fh - 2 * m_ft_f) + 0.03f * MathF.Pow4(m_fr) + 0.2146f * m_fr * m_fr * MathF.Pow2(m_fh - 2 * m_ft_f - 0.4468f * m_fr));
        }
        // Section modulus - elastic
        void Calc_W_y_el()
        {
            m_fW_y_el = 2 * m_fI_y / m_fh;
        }
        // Section modulus - plastic
        void Calc_W_y_pl()
        {
            m_fW_y_pl = (m_ft_w * m_fh * m_fh) / 4 + (m_fb - m_ft_w) * (m_fh - m_ft_f) * m_ft_f + ((4 - MathF.fPI) / 2) * m_fr * m_fr * (m_fh - 2 * m_ft_f) + (((3 * MathF.fPI - 10) / 3) * m_fr * m_fr * m_fr);
        }
        // Shape factor - plastic/elastic
        void Calc_f_y_plel()
        {
            m_ff_y_plel = m_fW_y_pl / m_fW_y_el;
        }


        // First moment o area
        void Calc_S_z()
        {
            m_fS_z = 2 * ((m_fb / 2 * m_ft_f) * m_fb / 4) + 2 * ((((4 * m_fr * m_fr) - (MathF.fPI * m_fr * m_fr)) / 4) * (0.2146f * m_fr + m_ft_w / 2)) + ((m_fh_i * m_ft_w / 2) * m_ft_w / 4);
        }
        // Second moment of area
        void Calc_I_z()
        {
            m_fI_z = 1 / 12f * (2 * m_ft_f * m_fb * m_fb * m_fb + (m_fh - 2 * m_ft_f) * m_ft_w * m_ft_w * m_ft_w) + 0.03f * MathF.Pow4(m_fr) + 0.2146f * m_fr * m_fr * MathF.Pow2(m_ft_w + 0.4468f * m_fr);
        }
        // Section modulus - elastic
        void Calc_W_z_el()
        {
            m_fW_z_el = 2 * m_fI_z / m_fb;
        }
        // Section modulus - plastic
        void Calc_W_z_pl()
        {
            m_fW_z_pl = 2 * (2 * (m_fb / 2 * m_ft_f * m_fb / 4) + ((m_fh - 2 * m_ft_f) * (m_ft_w / 2) * m_ft_w / 4) + 2 * ((((4 - MathF.fPI) * m_fr * m_fr) / 4) * 0.4468f * (m_fr + m_ft_w / 2)));
        }
        // Shape factor - plastic/elastic
        void Calc_f_z_plel()
        {
            m_ff_z_plel = m_fW_z_pl / m_fW_z_el;
        }


        // Torsional inertia constant
        void Calc_I_t()
        {
            m_fI_t = 2 * (m_fb - 0.063f * m_ft_f) * MathF.Pow3(m_ft_f) / 3f + 2 * (m_ft_w / m_ft_f) * (0.145f * +0.1f * m_fr / m_ft_f) * MathF.Pow4((MathF.Pow2(m_fr + m_ft_w / 2) + MathF.Pow2(m_fr + m_ft_f) - MathF.Pow2(m_fr)) / (2 * m_fr + m_ft_f));
            m_fI_t = 2 / 3 * (m_fb - 0.63f * m_ft_f) * MathF.Pow3(m_ft_f) + 1 / 3 * (m_fh - 2 * m_ft_f) * MathF.Pow3(m_ft_w) + 2 * m_ft_w / m_ft_f * (0.145f + 0.1f * m_fr / m_ft_f) * MathF.Pow4(((MathF.Pow2(m_fr + m_ft_w / 2) + (MathF.Pow2(m_fr + m_ft_f) - m_fr * m_fr) / (2 * m_fr + m_ft_f))));
        }
        // Torsional radius of gyration
        void Calc_i_t()
        {
            m_fi_t = MathF.Sqrt(m_fI_t / m_fA);
        }
        // Torsional section modulus - elastic
        void Calc_W_t_el()
        {
            m_fW_t_el = m_fI_t / Math.Max(m_ft_w, m_ft_f); // Min or Max Thickness
        }
        // Torsional section modulus - plastic
        void Calc_W_t_pl()
        {
            m_fW_t_pl = (m_fb - m_ft_w - 2 * m_fr - m_ft_f / 3.0f) * MathF.Pow2(m_ft_f) + m_fd * MathF.Pow2(m_ft_w) / 2.0f /*+   doplnit   */;  // Temp
        }
        // Torsional shape factor plastic/elastic
        void Calc_f_t_plel()
        {
            m_ff_t_plel = m_fW_t_pl / m_fW_t_el;
        }
        // Section warping constant
        void Calc_C_w()
        {
            m_fI_w = m_ft_f * MathF.Pow3(m_fb) * MathF.Pow2(m_fh - m_ft_f) / 24.0f;
        }


        // Shear factor
        void Calc_Eta_y_v()
        {
            m_fEta_y_v = (m_fA / MathF.Pow2(m_fI_y)) /*  Syi^2 / byi *dz */ ;  // Temp
        }
        // Shear effective area - elastic
        void Calc_A_y_v_el()
        {
            m_fA_y_v_el = 2 * m_ft_w / m_fW_y_pl;
        }
        // Shape factor for shear - plastic/elastic
        void Calc_f_y_v_plel()
        {
            m_ff_y_v_plel = 1.00f; // Temp
        }
        // Shear effective area - plastic
        void Calc_A_y_v_pl()
        {
            m_fA_y_v_pl = m_ff_y_v_plel * m_fA_y_v_el; // Temp
        }


        // Shear factor
        void Calc_Eta_z_v()
        {
            m_fEta_z_v = (m_fA / MathF.Pow2(m_fI_z)) /*  Szi^2 / bzi *dy */ ;  // Temp
        }
        // Shear effective area - elastic
        void Calc_A_z_v_el()
        {
            m_fA_z_v_el = m_fI_z /* * Math.Min (bzi / Szi)*/;
        }
        // Shape factor for shear - plastic/elastic
        void Calc_f_z_v_plel()
        {
            m_ff_z_v_plel = 1.00f; // Temp
        }
        // Shear effective area - plastic
        void Calc_A_z_v_pl()
        {
            m_fA_z_v_pl = m_ff_z_v_plel * m_fA_z_v_el; // Temp
        }
    }
}
