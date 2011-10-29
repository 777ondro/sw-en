using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CENEX ///////////////////////////////////////////////ROZPRACOVANE ////////////////////////////////////////////////////////////////////
{
    public class CCrSc_3_08 : CCrSc
    {
        // Rolled mono-symmetric T section - tapered flanges

        //----------------------------------------------------------------------------
        private float m_fh;                 // Height-depth / Vyska
        private float m_fb;                 // Width  / Sirka
        private float m_ft_f;               // Flange Thickness / Hrubka pasnice
        private float m_ft_w;               // Web Thickness  / Hrubka steny/stojiny
        private float m_fz_c;               // Centroid coordinate / Suradnica tažiska / Absolute value
        private float m_fr_1;               // Radius - web to flange face
        private float m_fr_2;               // Radius - flange edge
        private float m_fr_3;               // Radius - web edge - optional
        private float m_fd;                 // Web depth - straigth part - perpendicular distance
        private float m_fSlopeTaper;        // Slope of Taper
        private short m_iNumOfArcSegment;   // Number of Arc Segments
        private short m_iNumOfArcPoints;    // Number of Arc Points
        private short m_iTotNoPoints;       // Total Number of Cross-section Points for Drawing
        public float[,] m_CrScPoint;        // Array of Points and values in 2D
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
        public float Ft_f
        {
            get { return m_ft_f; }
            set { m_ft_f = value; }
        }
        public float Ft_w
        {
            get { return m_ft_w; }
            set { m_ft_w = value; }
        }
        public float Fz_c
        {
            get { return m_fz_c; }
            set { m_fz_c = value; }
        }
        public float Fr_1
        {
            get { return m_fr_1; }
            set { m_fr_1 = value; }
        }
        public float Fr_2
        {
            get { return m_fr_2; }
            set { m_fr_2 = value; }
        }
        public float Fr_3
        {
            get { return m_fr_3; }
            set { m_fr_3 = value; }
        }

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

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_3_08()  {   }
        public CCrSc_3_08(float fh, float fb, float ft_f, float ft_w, float fz_c, float fr_1, float fr_2, float fr_3, float fd)
        {
            m_iNumOfArcSegment = 4;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iTotNoPoints = (short)(4 * (short)m_iNumOfArcPoints + (5 + 2));

            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fz_c = fz_c;
            m_fr_1 = fr_1;
            m_fr_2 = fr_2;
             m_fr_3 = fr_3;
            m_fd = fd;

            //if()
              m_fSlopeTaper = ((m_fh - m_fd - 2*( m_fr_1 + m_fr_2)) / 2.0f) / ((m_fb - m_ft_w - 2*(m_fr_1 + m_fr_2)) / 2.0f);
            //else
            //  m_fSlopeTaper = 0.08f; // Default

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_I_DS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 5 + 2;

            // Point No. 1
            m_CrScPoint[0,0] = -m_fb / 2f + m_fr_2;    // y
            m_CrScPoint[0,1] = m_fh / 2f;              // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_ft_w / 2.0f - m_fr_1;   // y
            m_CrScPoint[1,1] = m_CrScPoint[0,1];           // z

            // Point No. 3
            m_CrScPoint[2,0] = -m_CrScPoint[1,0];      // y
            m_CrScPoint[2,1] = m_CrScPoint[0,1];       // z

            // Point No. 4
            m_CrScPoint[3,0] = -m_CrScPoint[0,0];      // y
            m_CrScPoint[3,1] = m_CrScPoint[0,1];       // z

            // Point No. 5
            m_CrScPoint[4,0] = m_ft_w / 2.0f;         // y
            m_CrScPoint[4,1] = m_CrScPoint[3, 1] - m_fr_2 - ((m_fh - m_fd - 2 * (m_fr_1 + m_fr_2)) / 2.0f);     // z

            // Point No. 6
            m_CrScPoint[5,0] = m_CrScPoint[4,0];      // y
            m_CrScPoint[5,1] = -m_CrScPoint[4,1];     // z

            // Surface points

            // Point No. 7 - 1st Edge point - upper left
            m_CrScPoint[iNumberAux, 0] = -m_fb / 2.0f;     // y
            m_CrScPoint[iNumberAux, 1] = m_fh  - m_fz_c;      // z

            int iRadiusAngle = 90; // Radius Angle

            // 2nd radius - centre "3" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + i + 1, 0] = m_CrScPoint[3, 0] + Geom2D.GetPositionX(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + i + 1, 1] = m_CrScPoint[3, 1] + Geom2D.GetPositionY_CW(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 3rd radius - centre "4" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[4, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[4, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 4th radius - centre "5" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[5, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[5, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 5th radius - centre "6" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[6, 0] + Geom2D.GetPositionX(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[6, 1] + Geom2D.GetPositionY_CW(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 6th radius - centre "9" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 4 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[9, 0] + Geom2D.GetPositionX(m_fr_2, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 4 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[9, 1] + Geom2D.GetPositionY_CW(m_fr_2, 180 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 7th radius - centre "10" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 5 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[10, 0] - m_fr_1 + Geom2D.GetPositionX(m_fr_1, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 5 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[10, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 270 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 8th radius - centre "11" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 6 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[11, 0] - m_fr_1 + Geom2D.GetPositionX(m_fr_1, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 6 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[11, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 0 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 1st radius - centre "0" (90-180 degrees)
            // Do not create last point of segment - it is already defined
            for (short i = 0; i < m_iNumOfArcPoints - 1; i++)
            {
                m_CrScPoint[iNumberAux + 7 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[0, 0] + Geom2D.GetPositionX(m_fr_2, 90 + i * iRadiusAngle / m_iNumOfArcSegment);      // y
                m_CrScPoint[iNumberAux + 7 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[0, 1] + Geom2D.GetPositionY_CW(m_fr_2, 90 + i * iRadiusAngle / m_iNumOfArcSegment);    // z
            }
        }
    }
}
