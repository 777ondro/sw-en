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
    public class CCrSc_0_02 : CCrSc
    {
        // Rolled round bar

        //----------------------------------------------------------------------------
        private float m_fd;   // Diameter/ Priemer
        //private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing (withCentroid Point)
        //public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fd
        {
            get { return m_fd; }
            set { m_fd = value; }
        }

        /* public short ITotNoPoints
         {
             get { return m_iTotNoPoints; }
             set { m_iTotNoPoints = value; }
         }*/

        float m_fr_out; // Radius

        public float Fr_out
        {
            get { return m_fr_out; }
            set { m_fr_out = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_02() { }
        public CCrSc_0_02(float fd, short iTotNoPoints)
        {
            IsShapeSolid = true;
            // m_iTotNoPoints = 72+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
            m_fd = fd;
            ITotNoPoints = iTotNoPoints; // + 1 auxialiary node in centroid / stredovy bod v tazisku

            Fr_out = m_fd / 2f;

            if (iTotNoPoints < 2 || m_fr_out <= 0f)
                return;

            // Create Array - allocate memory
            CrScPointsOut = new float[ITotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();

            // Fill list of indices for drawing of surface - triangles edges
            loadCrScIndices();
        }
        public CCrSc_0_02(float fd)
        {
            IsShapeSolid = true;

            // m_iTotNoPoints = 72+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
            m_fd = fd;
            ITotNoPoints = 73; // 1 auxialiary node in centroid / stredovy bod v tazisku

            Fr_out = m_fd / 2f;

            if (m_fr_out <= 0f)
                return;

            // Create Array - allocate memory
            CrScPointsOut = new float[ITotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();

            // Fill list of indices for drawing of surface - triangles edges
            loadCrScIndices();
        }

        //----------------------------------------------------------------------------
        public void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates
            CrScPointsOut = Geom2D.GetCirclePointCoord(m_fr_out, ITotNoPoints);

            // Centroid
            CrScPointsOut[ITotNoPoints - 1, 0] = 0f;
            CrScPointsOut[ITotNoPoints - 1, 1] = 0f;
        }

        //----------------------------------------------------------------------------
        // Cross-section properties
        //----------------------------------------------------------------------------

        float m_fU, m_fA,
            m_fW_t_el, m_fI_t, m_fW_t_pl, m_ff_t_plel,
            m_fS, m_fI, m_fW_el, m_fW_pl, m_ff_plel,
            m_fEta_v, m_fA_v_el, m_fA_v_pl, m_ff_v_plel;

        // Perimeter of section
        void Calc_U()
        {
            m_fU = MathF.fPI * m_fd;
        }
        // Section area
        void Calc_A()
        {
            m_fA = MathF.fPI * MathF.Pow2(m_fd) / 4f;
        }
        // Torsional inertia constant
        void Calc_I_t()
        {
            m_fI_t = MathF.fPI * MathF.Pow4(m_fd) / 32f;
        }
        // Torsional section modulus - elastic
        void Calc_W_t_el()
        {
            m_fW_t_el = MathF.fPI * MathF.Pow3(m_fd) / 16f;
        }
        // Torsional section modulus - plastic
        void Calc_W_t_pl()
        {
            m_fW_t_pl = MathF.fPI * MathF.Pow3(m_fd) / 12f;
        }
        // Torsional shape factor plastic/elastic
        void Calc_f_t_plel()
        {
            m_ff_t_plel = m_fW_t_pl / m_fW_t_el;
        }
        // First moment o area
        void Calc_S()
        {
            m_fS = MathF.Pow3(m_fd) / 12f;
        }
        // Second moment of area
        void Calc_I()
        {
            m_fI = MathF.fPI * MathF.Pow4(m_fd) / 64f;
        }
        // Section modulus - elastic
        void Calc_W_el()
        {
            m_fW_el = MathF.fPI * MathF.Pow3(m_fd) / 32f;
        }
        // Section modulus - plastic
        void Calc_W_pl()
        {
            m_fW_pl = MathF.Pow3(m_fd) / 6f;
        }
        // Shape factor - plastic/elastic
        void Calc_f_plel()
        {
            m_ff_plel = m_fW_pl / m_fW_el;
        }
        // Shear factor
        void Calc_Eta_v()
        {
            m_fEta_v = 1.175f;
        }
        // Shear effective area - elastic
        void Calc_A_v_el()
        {
            float fNu = 0.3f; // Poisson factor
            m_fA_v_el = 2 * (1 + fNu) * m_fA / (3 + 2 * fNu);
        }
        // Shape factor for shear - plastic/elastic
        void Calc_f_v_plel()
        {
            m_ff_v_plel = 1.33f;
        }
        // Shear effective area - plastic
        void Calc_A_v_pl()
        {
            //?????  
            m_fA_v_pl = m_ff_v_plel * m_fA_v_el;
        }

        protected override void loadCrScIndices()
        {

            const int secNum = 73;  // Number of points in section (2D) includes centroid
            TriangleIndices = new Int32Collection();

            // Front Side / Forehead
            for (int i = 0; i < secNum - 1; i++)
            {
                if (i < secNum - 2)
                {
                    TriangleIndices.Add(i);
                    TriangleIndices.Add(secNum - 1);
                    TriangleIndices.Add(i + 1);
                }
                else // Last Element
                {
                    TriangleIndices.Add(i);
                    TriangleIndices.Add(secNum - 1);
                    TriangleIndices.Add(0);
                }
            }

            // Back Side
            for (int i = 0; i < secNum - 1; i++)
            {
                if (i < secNum - 2)
                {
                    TriangleIndices.Add(secNum + i);
                    TriangleIndices.Add(secNum + i + 1);
                    TriangleIndices.Add(secNum + secNum - 1);
                }
                else // Last Element
                {
                    TriangleIndices.Add(secNum + i);
                    TriangleIndices.Add(secNum);
                    TriangleIndices.Add(secNum + secNum - 1);
                }
            }

            // Shell Surface OutSide
            for (int i = 0; i < secNum - 1; i++)
            {
                if (i < secNum - 2)
                    AddRectangleIndices_CW_1234(TriangleIndices, i, secNum + i, secNum + i + 1, i + 1);
                else
                    AddRectangleIndices_CW_1234(TriangleIndices, i, secNum + i, secNum, 0); // Last Element

            }
        }
    }
}
