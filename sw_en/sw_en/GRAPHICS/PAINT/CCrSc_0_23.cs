using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_23 : CCrSc
    {
        // Ellipse

        //----------------------------------------------------------------------------
        private float m_fa;   // Major Axis Dimension (2x Length of Semimajor Axis)
        private float m_fb;   // Minor Axis Dimension (2x Length of Semiminor Axis)
        private float m_ft;   // Thickness
        private float m_fAngle; // Angle of Rotation
        private short m_iNoPoints; // Number of Cross-section Points for Drawing in One Ellipse (36)
        public float[,] m_CrScPointOut; // Array of Outside Points and values in 2D
        public float[,] m_CrScPointIn; // Array of Inside Points and values in 2D
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

        float m_fr_out_major;
        float m_fr_in_major;
        float m_fr_out_minor;
        float m_fr_in_minor;

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_23() { }
        public CCrSc_0_23(float fa, float fb, float ft, short iNoPoints)
        {
            m_iNoPoints = iNoPoints; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fa = fa;
            m_fb = fb;
            m_ft = ft;

            m_fAngle = 90f;

            // Radii
            m_fr_out_major = m_fa / 2f;
            m_fr_in_major = m_fa / 2f - m_ft;

            m_fr_out_minor = m_fb / 2f;
            m_fr_in_minor = m_fb / 2f - m_ft;

            if (iNoPoints < 2 || m_fr_in_major == m_fr_out_major || m_fr_in_minor == m_fr_out_minor)
                return;


            // Create Array - allocate memory
            m_CrScPointOut = new float[m_iNoPoints, 2];
            m_CrScPointIn = new float[m_iNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();
        }
        public CCrSc_0_23(float fa, float fb, float ft)
        {
            m_iNoPoints = 72; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fa = fa;
            m_fb = fb;
            m_ft = ft;

            m_fAngle = 90f;

            // Radii
            m_fr_out_major = m_fa / 2f;
            m_fr_in_major = m_fa / 2f - m_ft;

            m_fr_out_minor = m_fb / 2f;
            m_fr_in_minor = m_fb / 2f - m_ft;

            if (m_fr_in_major == m_fr_out_major || m_fr_in_minor == m_fr_out_minor)
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
            // INoPoints = 72; // vykreslujeme ako n-uholnik

            // Outside Points Coordinates
            m_CrScPointOut = Geom2D.GetEllipsePointCoord(m_fr_out_major, m_fr_out_minor, m_fAngle, INoPoints);

            // Inside Points
            m_CrScPointIn = Geom2D.GetEllipsePointCoord(m_fr_in_major, m_fr_in_minor, m_fAngle, INoPoints);
       }
    }
}