using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CRSC
{
    public class CCrSc_3_02 : CCrSc
    {
        // Rolled monosymmetric U section (channel) - tapered or paralel flanges

        //----------------------------------------------------------------------------
        private short m_sShape;       // Section shape

        // Section shapes types
        // 0 - Four radii, tapered or parallel flanges (6 auxiliary points)
        // 1 - Two radii at flanges tips, tapered or parallel flanges (0 auxiliary points), r1 = 0
        // 2 - Two radii at flanges roots, tapered or parallel flanges (2 auxiliary points), r2 = 0

        private float m_fh;                 // Height / Vyska
        private float m_fb;                 // Width  / Sirka
        private float m_ft_f;               // Flange Thickness / Hrubka pasnice
        private float m_ft_w;               // Web Thickness  / Hrubka steny/stojiny
        private float m_fr_1;               // Radius
        private float m_fr_2;               // Radius - flange edge
        private float m_fd;                 // Web depth - straigth part
        private float m_fy_c;               // Centroid coordinate / Suradnica tažiska / Absolute value
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
        public float Fd
        {
            get { return m_fd; }
            set { m_fd = value; }
        }
                public float Fy_c
        {
          get { return m_fy_c; }
          set { m_fy_c = value; }
        }
        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_3_02()  {   }
        // 0 - Four radii, tapered or parallel flanges (6 auxiliary points)
        public CCrSc_3_02(short sShape, float fh, float fb, float ft_f, float ft_w, float fr_1, float fr_2, float fd, float fy_c)
        {
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iTotNoPoints = (short)(4 * (short)m_iNumOfArcPoints + 6 + 2);

            m_sShape = sShape;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fr_1 = fr_1;
            m_fr_2 = fr_2;
            m_fd = fd;
            m_fy_c = fy_c;

            //if()
              m_fSlopeTaper = ((m_fh - m_fd - 2*( m_fr_1 + m_fr_2)) / 2.0f) / (m_fb - m_ft_w - m_fr_1 - m_fr_2);
            //else
            //  m_fSlopeTaper = 0.08f; // Default

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_U_MS_0();
        }

        public CCrSc_3_02(short sShape, float fh, float fb, float ft_f, float ft_w, float fr, float fd, float fy_c)
        {
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points

            m_sShape = sShape;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fd = fd;
            m_fy_c = fy_c;
            
            //  m_fSlopeTaper = 0.08f; // Default
            // Number of points per section
            if (m_sShape == 1)       // 1 - Two radii at flanges tips, tapered or parallel flanges (0 auxiliary points), r1 = 0
            {
                m_fr_1 = 0.0f;
                m_fr_2 = fr;
            }
            else if (m_sShape == 2)  // 2 - Two radii at flanges roots, tapered or parallel flanges (2 auxiliary points), r2 = 0
            {
                m_iTotNoPoints = (short)(2 * (short)m_iNumOfArcPoints + 8 + 2);
                m_fr_1 = fr;
                m_fr_2 = 0.0f;
                m_fSlopeTaper = (2*((m_fh - m_fd - 2 * m_fr_1) / 2.0f - m_ft_f)) / (m_fb - m_ft_w - m_fr_1);
            }
            else // Exception
            { }

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data

            if (m_sShape == 1)       // 1 - Two radii at flanges tips, tapered or parallel flanges (0 auxiliary points), r1 = 0
                CalcCrSc_Coord_U_MS_1();
            else if (m_sShape == 2)  // 2 - Two radii at flanges roots, tapered or parallel flanges (2 auxiliary points), r2 = 0
                CalcCrSc_Coord_U_MS_2();
            else // Exception
            { }
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_U_MS_0()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 6;

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fy_c + m_ft_w + m_fr_1;       // y
            m_CrScPoint[0, 1] = m_fh / 2f;                       // z

            // Point No. 2
            m_CrScPoint[1, 0] = - m_fy_c + m_fb- m_fr_2;         // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];               // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_fy_c + m_ft_w;                                                                // y
            m_CrScPoint[2, 1] = m_CrScPoint[0, 1]  - m_fr_2 - ((m_fh - m_fd - 2 * (m_fr_1 + m_fr_2)) / 2.0f);    // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[2, 0];               // y
            m_CrScPoint[3, 1] = -m_CrScPoint[2, 1];              // z

            // Point No. 5
            m_CrScPoint[4, 0] = m_CrScPoint[1, 0];               // y
            m_CrScPoint[4, 1] = -m_CrScPoint[1, 1];              // z

            // Point No. 6
            m_CrScPoint[5,0] = m_CrScPoint[0,0];                 // y
            m_CrScPoint[5,1] = -m_CrScPoint[0,1];                // z

            // Surface points

            // Point No. 7 - 1st Edge point - upper left
            m_CrScPoint[iNumberAux, 0] = -m_fy_c;                // y
            m_CrScPoint[iNumberAux, 1] = m_fh / 2.0f;            // z


            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "1" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + i + 1, 0] = m_CrScPoint[1, 0] + Geom2D.GetPositionX(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + i + 1, 1] = m_CrScPoint[1, 1] + Geom2D.GetPositionY_CW(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 2nd radius - centre "2" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[2, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[2, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 3rd radius - centre "3" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[3, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[3, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 4th radius - centre "4" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 1, 0] = m_CrScPoint[4, 0] + Geom2D.GetPositionX(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 1, 1] = m_CrScPoint[4, 1] + Geom2D.GetPositionY_CW(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // Point No.  - Last edge point - bottom left
            m_CrScPoint[iNumberAux + 2+4 * m_iNumOfArcPoints -1, 0] = -m_fy_c;                  // y
            m_CrScPoint[iNumberAux + 2+4 * m_iNumOfArcPoints -1, 1] = -m_fh / 2.0f;             // z
        }

        void CalcCrSc_Coord_U_MS_1()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            //short iNumberAux = 0;
        }

        void CalcCrSc_Coord_U_MS_2()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 2;

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fy_c + m_ft_w;                 // y                                                             // y
            m_CrScPoint[0, 1] = (m_fd + 2 * m_fr_1) / 2.0f;       // z

            // Point No. 2
            m_CrScPoint[1, 0] = m_CrScPoint[0, 0];                // y
            m_CrScPoint[1, 1] = -m_CrScPoint[0, 1];               // z

            // Surface points

            // Point No. 3
            m_CrScPoint[2, 0] = -m_fy_c;                          // y
            m_CrScPoint[2, 1] = m_fh / 2.0f;                      // z

            // Point No. 4
            m_CrScPoint[3, 0] = -m_fy_c + m_ft_w + m_fr_1;        // y
            m_CrScPoint[3, 1] = m_CrScPoint[2, 1];                // z

            // Point No. 5
            m_CrScPoint[4, 0] = -m_fy_c + m_fb;                    // y
            m_CrScPoint[4, 1] = m_CrScPoint[2, 1];                 // z

            // Point No. 6
            m_CrScPoint[5, 0] = m_CrScPoint[4, 0];                                             // y
            m_CrScPoint[5, 1] = m_CrScPoint[0, 1] + m_fSlopeTaper * (m_fb - m_ft_w - m_fr_1);  // z

            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "0" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + i + 4, 0] = m_CrScPoint[0, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + i + 4, 1] = m_CrScPoint[0, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 2nd radius - centre "1" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 4, 0] = m_CrScPoint[1, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 4, 1] = m_CrScPoint[1, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 4, 0] = m_CrScPoint[5, 0];              // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 4, 1] = -m_CrScPoint[5, 1];             // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 5, 0] = m_CrScPoint[4, 0];              // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 5, 1] = -m_CrScPoint[4, 1];             // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 6, 0] = m_CrScPoint[3, 0];              // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 6, 1] = -m_CrScPoint[3, 1];             // z

            // Point No. XX  - Last edge point - bottom left
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 7, 0] = m_CrScPoint[2, 0];              // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 7, 1] = -m_CrScPoint[2, 1];             // z
        }

		protected override void loadCrScIndices()
		{
			throw new NotImplementedException();
		}
	}
}
