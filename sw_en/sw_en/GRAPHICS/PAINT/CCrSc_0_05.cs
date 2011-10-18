using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_05 : CCrSc
    {
        // Rectangular/ Square - Flat bar

        //----------------------------------------------------------------------------
        private float m_fh;   // Height/ Depth/ Vyska
        private float m_fb;   // Width  / Sirka
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
        public CCrSc_0_05()  {   }
        public CCrSc_0_05(float fh, float fb)
        {
            m_iTotNoPoints = 4;
            m_fh = fh;
            m_fb = fb;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_CrScPoint = Geom2D.GetRectanglePointCoord(m_fh, m_fb);
        }


        //----------------------------------------------------------------------------
        // Cross-section properties
        //----------------------------------------------------------------------------

        float m_fU, m_fA,
                m_fS_y, m_fI_y, m_fW_y_el, m_fW_y_pl, m_ff_y_plel,
                m_fS_z, m_fI_z, m_fW_z_el, m_fW_z_pl, m_ff_z_plel,
                m_fW_t_el, m_fI_t, m_fi_t, m_fW_t_pl, m_ff_t_plel, m_fI_w,
                m_fEta_y_v, m_fA_y_v_el, m_fA_y_v_pl, m_ff_y_v_plel,
                m_fEta_z_v, m_fA_z_v_el, m_fA_z_v_pl, m_ff_z_v_plel;

        // Perimeter of section
        void Calc_U()
        {
            m_fU = 2* (m_fh + m_fb);
        }
        // Section area
        void Calc_A()
        {
            m_fA = m_fb * m_fh;
        }


        // First moment o area
        void Calc_S_y()
        {
            m_fS_y = m_fb * MathF.Pow2(m_fh) / 8f;
        }
        // Second moment of area
        void Calc_I_y()
        {
            m_fI_y = m_fb * MathF.Pow3(m_fh) / 12f;
        }
        // Section modulus - elastic
        void Calc_W_y_el()
        {
            m_fW_y_el = m_fb * MathF.Pow3(m_fh) / 6f;
        }
        // Section modulus - plastic
        void Calc_W_y_pl()
        {
            m_fW_y_pl = m_fb * MathF.Pow2(m_fh) / 4f;
        }
        // Shape factor - plastic/elastic
        void Calc_f_y_plel()
        {
            m_ff_y_plel = 1.5f;
        }


        // First moment o area
        void Calc_S_z()
        {
            m_fS_z = m_fh * MathF.Pow2(m_fb) / 8f;
        }
        // Second moment of area
        void Calc_I_z()
        {
            m_fI_z = m_fh * MathF.Pow3(m_fb) / 12f;
        }
        // Section modulus - elastic
        void Calc_W_z_el()
        {
            m_fW_z_el = m_fh * MathF.Pow3(m_fb) / 6f;
        }
        // Section modulus - plastic
        void Calc_W_z_pl()
        {
            m_fW_z_pl = m_fh * MathF.Pow2(m_fb) / 4f;
        }
        // Shape factor - plastic/elastic
        void Calc_f_z_plel()
        {
            m_ff_z_plel = 1.5f;
        }


        // Torsional inertia constant
        void Calc_I_t()
        {
            // http://www.xcalcs.com
            if (m_fh >= m_fb)
                m_fI_t = m_fh * MathF.Pow3(m_fb) * ((1 - 192 * m_fb / MathF.Pow5(MathF.fPI) * m_fh * ((float)Math.Tanh(Math.PI * m_fh / (2 * m_fb))) + (float)Math.Tanh(3 * Math.PI * m_fh / (2 * m_fb)) / 243f)) / 3f;
            else
                m_fI_t = m_fb * MathF.Pow3(m_fh) * ((1 - 192 * m_fh / MathF.Pow5(MathF.fPI) * m_fb * ((float)Math.Tanh(Math.PI * m_fb / (2 * m_fh))) + (float)Math.Tanh(3 * Math.PI * m_fb / (2 * m_fh)) / 243f)) / 3f;

            // Alternative  - EN 1999-1-1, eq. (J.2)
            if (m_fh >= m_fb)
                m_fI_t = (m_fh * MathF.Pow3(m_fb) / 3.0f) * (1.0f - 0.63f * m_fb / m_fh + 0.052f * MathF.Pow5(m_fb) / MathF.Pow5(m_fh));
            else
                m_fI_t = (m_fb * MathF.Pow3(m_fh) / 3.0f) * (1.0f - 0.63f * m_fh / m_fb + 0.052f * MathF.Pow5(m_fh) / MathF.Pow5(m_fb));
        }
        // Torsional radius of gyration
        void Calc_i_t()
        {
            m_fi_t = MathF.Sqrt(m_fI_t / m_fA);
        }
        // Torsional section modulus - elastic
        void Calc_W_t_el()
        {
            if (m_fh >= m_fb)
                m_fW_t_el = m_fI_t / m_fb * (1 - 8 * (1 / (float)Math.Cosh(Math.PI * m_fh / (2 * m_fb)) + 1 / 9f * (float)Math.Cosh(3 * Math.PI * m_fh / (2 * m_fb))) / MathF.Pow2(MathF.fPI));
            else
                m_fW_t_el = m_fI_t / m_fh * (1 - 8 * (1 / (float)Math.Cosh(Math.PI * m_fb / (2 * m_fh)) + 1 / 9f * (float)Math.Cosh(3 * Math.PI * m_fb / (2 * m_fh))) / MathF.Pow2(MathF.fPI));
        }
        // Torsional section modulus - plastic
        void Calc_W_t_pl()
        {
            if (m_fh >= m_fb)
                m_fW_t_pl = MathF.Pow2(m_fb) * (3 * m_fh - m_fb) / 6f;
            else
                m_fW_t_pl = MathF.Pow2(m_fh) * (3 * m_fb - m_fh) / 6f;
        }
        // Torsional shape factor plastic/elastic
        void Calc_f_t_plel()
        {
            m_ff_t_plel = m_fW_t_pl / m_fW_t_el;
        }
        // Section warping constant
        void Calc_I_w()
        {
            if (m_fh >= m_fb)
                m_fI_w = (MathF.Pow3(m_fh) * MathF.Pow3(m_fb) / 144.0f) * (1.0f - 4.884f * MathF.Pow2(m_fb) / MathF.Pow2(m_fh) + 4.97f * MathF.Pow3(m_fb) / MathF.Pow3(m_fh) - 1.067f * MathF.Pow5(m_fb) / MathF.Pow5(m_fh)); // EN 1999-1-1, eq. (J.4)
            else
                m_fI_w = (MathF.Pow3(m_fb) * MathF.Pow3(m_fh) / 144.0f) * (1.0f - 4.884f * MathF.Pow2(m_fh) / MathF.Pow2(m_fb) + 4.97f * MathF.Pow3(m_fh) / MathF.Pow3(m_fb) - 1.067f * MathF.Pow5(m_fh) / MathF.Pow5(m_fb)); // EN 1999-1-1, eq. (J.4)
        }



        // Shear factor
        void Calc_Eta_y_v()
        {
            m_fEta_y_v = 1.2f;
        }
        // Shear effective area - elastic
        void Calc_A_y_v_el()
        {
            m_fA_y_v_el = 0.75f * m_fA; // Temp
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
            m_fEta_z_v = 1.2f;
        }
        // Shear effective area - elastic
        void Calc_A_z_v_el()
        {
            m_fA_z_v_el = 0.75f * m_fA; // Temp
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
