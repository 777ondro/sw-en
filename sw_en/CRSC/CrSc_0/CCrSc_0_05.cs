﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;
using System.Windows.Media;

namespace CRSC
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_05 : CCrSc
    {
        // Rectangular/ Square - Flat bar

        //----------------------------------------------------------------------------
        //private float Fh;   // Height/ Depth/ Vyska
        //private float Fb;   // Width  / Sirka
        //private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        //public float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        // Cross-section properties
        //----------------------------------------------------------------------------

        /*float m_fU, m_fA,
                m_fS_y, m_fI_y, m_fW_y_el, m_fW_y_pl, m_ff_y_plel,
                m_fS_z, m_fI_z, m_fW_z_el, m_fW_z_pl, m_ff_z_plel,
                m_fW_t_el, m_fI_t, m_fi_t, m_fW_t_pl, m_ff_t_plel, m_fI_w,
                m_fEta_y_v, m_fA_y_v_el, m_fA_y_v_pl, m_ff_y_v_plel,
                m_fEta_z_v, m_fA_z_v_el, m_fA_z_v_pl, m_ff_z_v_plel;*/

        // Temp

        bool bIndicesCW = true; // Clockwise or counter-clockwise system

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_05() { }
        /*public CCrSc_0_05(float fh, float fb)
        {
            IsShapeSolid = true;
            ITotNoPoints = 4;
            Fh = fh;
            Fb = fb;

            // Create Array - allocate memory
            CrScPointsOut = new float[ITotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();

            // Fill list of indices for drawing of surface - triangles edges
            loadCrScIndices();
        }*/

        public CCrSc_0_05(float fh, float fb)
        {
            IsShapeSolid = true;
            ITotNoPoints = 4;
            Fh = fh;
            Fb = fb;

            // Create Array - allocate memory
            CrScPointsOut = new float[ITotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();

            // Fill list of indices for drawing of surface - triangles edges

            // Particular indices Rozpracovane pre vykreslovanie cela prutu inou farbou
            loadCrScIndicesFrontSide();
            loadCrScIndicesShell();
            loadCrScIndicesBackSide();

            // All indices together
            loadCrScIndices();

            // Calculate cross-section properties
            Calc_A();
            Calc_I_y();
            Calc_I_z();
            Calc_I_t();
        }

        //----------------------------------------------------------------------------
        public void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            CrScPointsOut = Geom2D.GetRectanglePointCoord(Fh, Fb);
        }

        // Perimeter of section
        void Calc_U()
        {
            FU = 2 * (Fh + Fb);
        }
        // Section area
        void Calc_A()
        {
            FA_g = Fb * Fh;
        }


        // First moment o area
        void Calc_S_y()
        {
            FS_y = Fb * MathF.Pow2(Fh) / 8f;
        }
        // Second moment of area
        void Calc_I_y()
        {
            FI_y = Fb * MathF.Pow3(Fh) / 12f;
        }
        // Section modulus - elastic
        void Calc_W_y_el()
        {
            FW_y_el = Fb * MathF.Pow3(Fh) / 6f;
        }
        // Section modulus - plastic
        void Calc_W_y_pl()
        {
            FW_y_pl = Fb * MathF.Pow2(Fh) / 4f;
        }
        // Shape factor - plastic/elastic
        void Calc_f_y_plel()
        {
            Ff_y_plel = 1.5f;
        }


        // First moment o area
        void Calc_S_z()
        {
            FS_z = Fh * MathF.Pow2(Fb) / 8f;
        }
        // Second moment of area
        void Calc_I_z()
        {
            FI_z = Fh * MathF.Pow3(Fb) / 12f;
        }
        // Section modulus - elastic
        void Calc_W_z_el()
        {
            FW_z_el = Fh * MathF.Pow3(Fb) / 6f;
        }
        // Section modulus - plastic
        void Calc_W_z_pl()
        {
            FW_z_pl = Fh * MathF.Pow2(Fb) / 4f;
        }
        // Shape factor - plastic/elastic
        void Calc_f_z_plel()
        {
            Ff_z_plel = 1.5f;
        }


        // Torsional inertia constant
        void Calc_I_t()
        {
            // http://www.xcalcs.com
            if (Fh >= Fb)
                FI_t = Fh * MathF.Pow3(Fb) * ((1 - 192 * Fb / MathF.Pow5(MathF.fPI) * Fh * ((float)Math.Tanh(Math.PI * Fh / (2 * Fb))) + (float)Math.Tanh(3 * Math.PI * Fh / (2 * Fb)) / 243f)) / 3f;
            else
                FI_t = Fb * MathF.Pow3(Fh) * ((1 - 192 * Fh / MathF.Pow5(MathF.fPI) * Fb * ((float)Math.Tanh(Math.PI * Fb / (2 * Fh))) + (float)Math.Tanh(3 * Math.PI * Fb / (2 * Fh)) / 243f)) / 3f;

            // Alternative  - EN 1999-1-1, eq. (J.2)
            if (Fh >= Fb)
                FI_t = (Fh * MathF.Pow3(Fb) / 3.0f) * (1.0f - 0.63f * Fb / Fh + 0.052f * MathF.Pow5(Fb) / MathF.Pow5(Fh));
            else
                FI_t = (Fb * MathF.Pow3(Fh) / 3.0f) * (1.0f - 0.63f * Fh / Fb + 0.052f * MathF.Pow5(Fh) / MathF.Pow5(Fb));
        }
        // Torsional radius of gyration
        void Calc_i_t()
        {
            Fi_t = MathF.Sqrt(FI_t / FA_g);
        }
        // Torsional section modulus - elastic
        void Calc_W_t_el()
        {
            if (Fh >= Fb)
                FW_t_el = FI_t / Fb * (1 - 8 * (1 / (float)Math.Cosh(Math.PI * Fh / (2 * Fb)) + 1 / 9f * (float)Math.Cosh(3 * Math.PI * Fh / (2 * Fb))) / MathF.Pow2(MathF.fPI));
            else
                FW_t_el = FI_t / Fh * (1 - 8 * (1 / (float)Math.Cosh(Math.PI * Fb / (2 * Fh)) + 1 / 9f * (float)Math.Cosh(3 * Math.PI * Fb / (2 * Fh))) / MathF.Pow2(MathF.fPI));
        }
        // Torsional section modulus - plastic
        void Calc_W_t_pl()
        {
            if (Fh >= Fb)
                FW_t_pl = MathF.Pow2(Fb) * (3 * Fh - Fb) / 6f;
            else
                FW_t_pl = MathF.Pow2(Fh) * (3 * Fb - Fh) / 6f;
        }
        // Torsional shape factor plastic/elastic
        void Calc_f_t_plel()
        {
            Ff_t_plel = FW_t_pl / FW_t_el;
        }
        // Section warping constant
        void Calc_I_w()
        {
            if (Fh >= Fb)
                FI_w = (MathF.Pow3(Fh) * MathF.Pow3(Fb) / 144.0f) * (1.0f - 4.884f * MathF.Pow2(Fb) / MathF.Pow2(Fh) + 4.97f * MathF.Pow3(Fb) / MathF.Pow3(Fh) - 1.067f * MathF.Pow5(Fb) / MathF.Pow5(Fh)); // EN 1999-1-1, eq. (J.4)
            else
                FI_w = (MathF.Pow3(Fb) * MathF.Pow3(Fh) / 144.0f) * (1.0f - 4.884f * MathF.Pow2(Fh) / MathF.Pow2(Fb) + 4.97f * MathF.Pow3(Fh) / MathF.Pow3(Fb) - 1.067f * MathF.Pow5(Fh) / MathF.Pow5(Fb)); // EN 1999-1-1, eq. (J.4)
        }



        // Shear factor
        void Calc_Eta_y_v()
        {
            FEta_y_v = 1.2f;
        }
        // Shear effective area - elastic
        void Calc_A_y_v_el()
        {
            FA_y_v_el = 0.75f * FA_g; // Temp
        }
        // Shape factor for shear - plastic/elastic
        void Calc_f_y_v_plel()
        {
            Ff_y_v_plel = 1.00f; // Temp
        }
        // Shear effective area - plastic
        void Calc_A_y_v_pl()
        {
            FA_y_v_pl = Ff_y_v_plel * FA_y_v_el; // Temp
        }


        // Shear factor
        void Calc_Eta_z_v()
        {
            FEta_z_v = 1.2f;
        }
        // Shear effective area - elastic
        void Calc_A_z_v_el()
        {
            FA_z_v_el = 0.75f * FA_g; // Temp
        }
        // Shape factor for shear - plastic/elastic
        void Calc_f_z_v_plel()
        {
            Ff_z_v_plel = 1.00f; // Temp
        }
        // Shear effective area - plastic
        void Calc_A_z_v_pl()
        {
            FA_z_v_pl = Ff_z_v_plel * FA_z_v_el; // Temp
        }

        protected override void loadCrScIndices()
        {
            // const int secNum = 4;  // Number of points in section (2D)
            TriangleIndices = new Int32Collection();

            // Front Side / Forehead
            AddRectangleIndices_CW_1234(TriangleIndices, 0, 1, 2, 3);

            // Back Side
            AddRectangleIndices_CW_1234(TriangleIndices, 4, 7, 6, 5);

            // Shell Surface
            AddRectangleIndices_CW_1234(TriangleIndices, 0, 4, 5, 1);
            AddRectangleIndices_CW_1234(TriangleIndices, 1, 5, 6, 2);
            AddRectangleIndices_CW_1234(TriangleIndices, 2, 6, 7, 3);
            AddRectangleIndices_CW_1234(TriangleIndices, 3, 7, 4, 0);
        }

        protected override void loadCrScIndicesFrontSide()
        {
           TriangleIndicesFrontSide = new Int32Collection();
            // Front Side / Forehead
           if(bIndicesCW)
             AddRectangleIndices_CW_1234(TriangleIndicesFrontSide, 0, 1, 2, 3);
           else
             AddRectangleIndices_CCW_1234(TriangleIndicesFrontSide, 0, 1, 2, 3);
        }

        protected override void loadCrScIndicesShell()
        {
            TriangleIndicesShell = new Int32Collection();
            // Shell Surface
            if (bIndicesCW)
            {
                AddRectangleIndices_CW_1234(TriangleIndicesShell, 0, 4, 5, 1);
                AddRectangleIndices_CW_1234(TriangleIndicesShell, 1, 5, 6, 2);
                AddRectangleIndices_CW_1234(TriangleIndicesShell, 2, 6, 7, 3);
                AddRectangleIndices_CW_1234(TriangleIndicesShell, 3, 7, 4, 0);
            }
            else
            {
                AddRectangleIndices_CCW_1234(TriangleIndicesShell, 0, 4, 5, 1);
                AddRectangleIndices_CCW_1234(TriangleIndicesShell, 1, 5, 6, 2);
                AddRectangleIndices_CCW_1234(TriangleIndicesShell, 2, 6, 7, 3);
                AddRectangleIndices_CCW_1234(TriangleIndicesShell, 3, 7, 4, 0);
            }

        }

        protected override void loadCrScIndicesBackSide()
        {
            TriangleIndicesBackSide = new Int32Collection();
            // Back Side
            if (bIndicesCW) // Back view
              AddRectangleIndices_CCW_1234(TriangleIndicesBackSide, 0, 1, 2, 3);
            else
              AddRectangleIndices_CW_1234(TriangleIndicesBackSide, 0, 1, 2, 3);
        }

    }
}
