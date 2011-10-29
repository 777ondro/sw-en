using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    public class CCrSc_3_09 : CCrSc_0_22
    {
        // Solid round bar

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
        public CCrSc_3_09(float fd, short iTotNoPoints)
        {
            // m_iTotNoPoints = 72+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
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

        public CCrSc_3_09(float fd)
        {
            // m_iTotNoPoints = 72+1; // vykreslujeme ako plny n-uholnik + 1 stredovy bod
            m_fd = fd;
            m_iTotNoPoints = 73; // 1 auxialiary node in centroid / stredovy bod v tazisku

            m_fr_out = m_fd / 2f;

            if (m_fr_out <= 0f)
                return;

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord();
        }
    }
}
