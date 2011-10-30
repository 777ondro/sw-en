using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CRSC
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_04:CCrSc
    {
        // Triangular Prism / Equilateral

        //----------------------------------------------------------------------------
        private float m_fa;   // Length of Side
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }

        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_04()  {   }
        public CCrSc_0_04(float fa)
        {
            m_iTotNoPoints = 3;
            m_fa = fa;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            m_CrScPoint = Geom2D.GetTrianEqLatPointCoord1(m_fa);
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

        public CCrSc_0_04(float fh, float fb)
        {
            m_iTotNoPoints = 3;
            m_fh = fh;
            m_fb = fb;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data

            // Isosceles
            m_CrScPoint = Geom2D.GetTrianIsosCelPointCoord(m_fh,m_fb);
            // Right - angled
            m_CrScPoint = Geom2D.GetTrianRightAngPointCoord(m_fh, m_fb);
        }

        public CCrSc_0_04(float fN0y, float fN0z, float fN1y, float fN1z, float fN2y, float fN2z)
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

        // Scalene - general
        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_Scalene()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)
        }
    }
}
