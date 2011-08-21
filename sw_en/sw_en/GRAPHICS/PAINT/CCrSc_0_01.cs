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
    public class CCrSc_0_01:CCrSc
    {
        // Solid Quater Cirlce / Stvrtkruh

        //----------------------------------------------------------------------------
        private float m_fd;   // Diameter/ Priemer
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing (withCentroid Point)
        public float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fd
        {
            get { return m_fd; }
            set { m_fd = value; }
        }

        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        float m_fr_out; // Radius

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_01()  {   }
        public CCrSc_0_01(float fd, short iTotNoPoints)
        {
            // m_iTotNoPoints = 20+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
            m_fd = fd;
            m_iTotNoPoints = iTotNoPoints; // + 1 auxialiary node in centroid / stredovy bod v tazisku

            m_fr_out = m_fd / 2f;

            if (iTotNoPoints < 2 || m_fr_out <= 0f)
                return;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates
            for (int i = 0; i < ITotNoPoints-1; i++)
            {
                m_CrScPoint[i, 0] = GetPositionX(m_fr_out, (MathF.fPI / 2) + i * 90 / (ITotNoPoints - 1));  // y rotation + 90 degrees
                m_CrScPoint[i, 1] = GetPositionY(m_fr_out, (MathF.fPI / 2) + i * 90 / (ITotNoPoints - 1));  // z rotation + 90 degrees
            }

            // Centroid
            m_CrScPoint[ITotNoPoints-1, 0] = 0f;
            m_CrScPoint[ITotNoPoints-1, 1] = 0f;
        }

        // Transformation of coordinates
        private float GetPositionX(float radius, float theta)
        {
            return radius * (float)Math.Cos(theta * Math.PI / 180);
        }

        private float GetPositionY(float radius, float theta)
        {
            return -radius * (float)Math.Sin(theta * Math.PI / 180);
        }
    }
}
