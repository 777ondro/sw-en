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
    public class CCrSc_0_24:CCrSc
    {
        // Triangular Prism / Equilateral with Opening
        //----------------------------------------------------------------------------
        private float m_fa;   // Length of Side
        private float m_ft;   // Thickness
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }
        
        public float Ft
        {
            get { return m_ft; }
            set { m_ft = value; }
        }

        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_24()  {   }
        public CCrSc_0_24(float fa, float ft)
        {
            m_iTotNoPoints = 6;
            m_fa = fa;
            m_ft = ft;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_EqLat();
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
            m_CrScPoint[2, 0] = -m_CrScPoint[1, 0];                     // y
            m_CrScPoint[2, 1] = m_CrScPoint[1, 1];                      // z

            // Point No. 4
            m_CrScPoint[3, 0] = 0f;                                                // y
            m_CrScPoint[3, 1] = 2f / 3f * (m_fa / 2f) * MathF.fSqrt3 - 2 * m_ft;   // z

            // Point No. 5
            m_CrScPoint[4, 0] = (m_fa / 2f) - (m_ft / (float)Math.Tan(0.523598775598299f)); // y // tan 0.5, resp. tan 30
            m_CrScPoint[4, 1] = -1f / 3f * (m_fa / 2f) * MathF.fSqrt3 + m_ft;               // z

            // Point No. 6
            m_CrScPoint[5, 0] = -m_CrScPoint[1, 0];                     // y
            m_CrScPoint[5, 1] = m_CrScPoint[1, 1];                      // z
        }
    }
}
