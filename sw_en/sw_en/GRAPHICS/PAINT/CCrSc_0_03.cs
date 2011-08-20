using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_03 : CCrSc
    {
        // Ellipse

        //----------------------------------------------------------------------------
        private float m_fa;   // Major Axis Dimension (2x Length of Semimajor Axis)
        private float m_fb;   // Minor Axis Dimension (2x Length of Semiminor Axis)
        private int m_iTotNoPoints; // Total Number of Cross-section Points for Drawing (withCentroid Point)
        public float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
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

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_03()  {   }
        public CCrSc_0_03(float fa, float fb, int iTotNoPoints)
        {
            // m_iTotNoPoints = 36+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
            m_fa = fa;
            m_fb = fb;
            m_iTotNoPoints = iTotNoPoints; // + 1 auxialiary node in centroid / stredovy bod v tazisku

            if (iTotNoPoints < 2 || fa <= 0f || fb <= 0f)
                return;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Basic Ellipse Function
            // Zbytocne vytvaram nove pole !!!!
            float [,] arrtemp = new float [m_iTotNoPoints - 1,2];
            arrtemp =  Geom2D.GetEllipsePoints(0.5f * m_fa, 0.5f * m_fb, m_iTotNoPoints - 1);
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)
            // Outside Points Coordinates
            for (int i = 0; i < ITotNoPoints-1; i++)
            {
                m_CrScPoint[i, 0] = arrtemp[i, 0];  // y
                m_CrScPoint[i, 1] = arrtemp[i, 1];  // z
            }

            // Centroid
            m_CrScPoint[ITotNoPoints-1, 0] = 0f;
            m_CrScPoint[ITotNoPoints-1, 1] = 0f;
        }
    }
}
