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
    public class CCrSc_B_TRIAN:CCrSc
    {
        // Triangular Prism / Equilateral

        //----------------------------------------------------------------------------
        private float m_fa;   // Length of Side
        private int m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }

        public int ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_B_TRIAN()  {   }
        public CCrSc_B_TRIAN(float fa)
        {
            m_iTotNoPoints = 3;
            m_fa = fa;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_EqLat();
        }


        private float m_fh;  // Height
        private float m_fb;  // Base Length
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

        public CCrSc_B_TRIAN(float fh, float fb)
        {
            m_iTotNoPoints = 3;
            m_fh = fh;
            m_fb = fb;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            // Isosceles
            CalcCrSc_Coord_Isoscel();
            // Right - angled
            CalcCrSc_Coord_RightAng();

        }

        public CCrSc_B_TRIAN(float fN0y, float fN0z, float fN1y, float fN1z, float fN2y, float fN2z)
        {
            m_iTotNoPoints = 3;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            // CalcCrSc_Coord_Scalene();

            // Point No. 1
            m_CrScPoint[0, 0] = fN0y;     // y
            m_CrScPoint[0, 1] = fN0z;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = fN1y;     // y
            m_CrScPoint[1, 1] = fN1z;     // z

            // Point No. 3
            m_CrScPoint[2, 0] = fN2y;     // y
            m_CrScPoint[2, 1] = fN2z;     // z
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_EqLat()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = 0f;                                     // y
            m_CrScPoint[0, 1] = 2f / 3f * (m_fa/2f) * MathF.fSqrt3;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = m_fa / 2f;                              // y
            m_CrScPoint[1, 1] = -1f / 3f * (m_fa/2f) * MathF.fSqrt3;    // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[1, 0];                // y
            m_CrScPoint[2, 1] = m_CrScPoint[1, 1];                 // z
        }

        // Isosceles
        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_Isoscel()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = 0f;                 // y
            m_CrScPoint[0, 1] = 2f / 3f * m_fh;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = m_fb / 2f;          // y
            m_CrScPoint[1, 1] = -1f / 3f * m_fh;    // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[1, 0];  // y
            m_CrScPoint[2, 1] = m_CrScPoint[1, 1];   // z
        }


        // Right triangle (right-angled triangle, rectangled triangle)
        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_RightAng()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fb / 3f;        // y
            m_CrScPoint[0, 1] = 2f / 3f * m_fh;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = 2f / 3f * m_fb;     // y
            m_CrScPoint[1, 1] = -1f / 3f * m_fh;    // z

            // Point No. 3
            m_CrScPoint[2, 0] = m_CrScPoint[0, 0];  // y
            m_CrScPoint[2, 1] = m_CrScPoint[1, 1];  // z
        }

        // Scalene - general
        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_Scalene()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)



        }
    }
}
