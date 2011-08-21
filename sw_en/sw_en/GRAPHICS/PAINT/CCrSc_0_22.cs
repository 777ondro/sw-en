using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_22 : CCrSc
    {
        // Tube / Rura

        //----------------------------------------------------------------------------
        private float m_fd;   // Diameter/ Priemer
        private float m_ft;   // Thickness/ Hrubka
        private short m_iNoPoints; // Number of Cross-section Points for Drawing in One Circle
        public  float[,] m_CrScPointOut; // Array of Outside Points and values in 2D
        public float[,] m_CrScPointIn; // Array of Inside Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fd
        {
            get { return m_fd; }
            set { m_fd = value; }
        }

        public float Ft
        {
            get { return m_ft; }
            set { m_ft = value; }
        }

        public short INoPoints
        {
            get { return m_iNoPoints; }
            set { m_iNoPoints = value; }
        }

        float m_fr_out;
        float m_fr_in;

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_22()  {   }
        public CCrSc_0_22(float fd, float ft, short iNoPoints)
        {
            m_iNoPoints = iNoPoints; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fd = fd;
            m_ft = ft;

            float fd_in = m_fd - 2 * m_ft;

            // Radii
            m_fr_out = m_fd / 2f;
            m_fr_in = fd_in / 2f;

            if (iNoPoints < 2 || m_fr_in == m_fr_out)
                return;

            // Create Array - allocate memory
            m_CrScPointOut = new float[m_iNoPoints, 2];
            m_CrScPointIn = new float[m_iNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();
        }
        public CCrSc_0_22(float fd, float ft)
        {
            m_iNoPoints = 72; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fd = fd;
            m_ft = ft;

            float fd_in = m_fd - 2 * m_ft;

            // Radii
            m_fr_out = m_fd / 2f;
            m_fr_in = fd_in / 2f;

            if (m_fr_in == m_fr_out)
                return;

            // Create Array - allocate memory
            m_CrScPointOut = new float[m_iNoPoints, 2];
            m_CrScPointIn = new float[m_iNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates
            m_CrScPointOut = Geom2D.GetCirclePointCoord(m_fr_out, INoPoints);

            // Inside Points
            m_CrScPointIn = Geom2D.GetCirclePointCoord(m_fr_in, INoPoints);
        }
    }
}




