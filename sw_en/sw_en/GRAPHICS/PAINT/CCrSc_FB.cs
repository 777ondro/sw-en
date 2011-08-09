using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_FB : CCrSc
    {
        // Rectangular/ Square - Flat bar

        //----------------------------------------------------------------------------
        private float m_fh;   // Height/ Depth/ Vyska
        private float m_fb;   // Width  / Sirka
        private int m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
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

        public int ITotNoPoints
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
        public CCrSc_FB()  {   }
        public CCrSc_FB(float fh, float fb)
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

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fb / 2f;    // y
            m_CrScPoint[0, 1] = m_fh / 2f;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_CrScPoint[0, 0];   // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];    // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[0, 0];   // y
            m_CrScPoint[2, 1] = -m_CrScPoint[0, 1];   // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[0, 0];    // y
            m_CrScPoint[3, 1] = -m_CrScPoint[0, 1];   // z
        }
    }
}
