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
    }
}
