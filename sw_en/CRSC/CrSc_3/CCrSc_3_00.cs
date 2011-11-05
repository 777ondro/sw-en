using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CRSC
{
    public class CCrSc_3_00 : CCrSc
    {
        // Rolled doubly symmetric I section - parallel or tapered flanges

        //----------------------------------------------------------------------------
        private short m_sShape;       // Section shape

        // Section shapes types
        // 0 - Eight radii, tapered or parallel flanges (12 auxiliary points)
        // 1 - Four radii at flanges tips, tapered or parallel flanges (8 auxiliary points), r1 = 0
        // 2 - Four radii at flanges roots, tapered or parallel flanges (4 auxiliary points), r2 = 0

        private float m_fh;                 // Height / Vyska
        private float m_fb;                 // Width  / Sirka
        private float m_ft_f;               // Flange Thickness / Hrubka pasnice
        private float m_ft_w;               // Web Thickness  / Hrubka steny/stojiny
        private float m_fr_1;               // Radius
        private float m_fr_2;               // Radius - flange edge
        private float m_fd;                 // Web depth - straigth part
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

        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_3_00()  {   }
        public CCrSc_3_00(short sShape, float fh, float fb, float ft_f, float ft_w, float fr_1, float fr_2, float fd)
        {
            m_sShape = sShape;
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iTotNoPoints = (short)(8 * (short)m_iNumOfArcPoints + (8 + 4));

            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fr_1 = fr_1;
            m_fr_2 = fr_2;
            m_fd = fd;

            m_fSlopeTaper = ((m_fh - m_fd - 2*( m_fr_1 + m_fr_2)) / 2.0f) / ((m_fb - m_ft_w - 2*(m_fr_1 + m_fr_2)) / 2.0f);
            
            //  m_fSlopeTaper = 0.08f; // Default

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_I_DS_0();
        }

        public CCrSc_3_00(short sShape,float fh, float fb, float ft_f, float ft_w, float fr, float fd)
        {
            m_sShape = sShape;

            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points

            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fd = fd;

            // Number of points per section
            if (m_sShape == 1)       // 1 - Four radii at flanges tips, tapered or parallel flanges (8 auxiliary points)
            {
                m_iTotNoPoints = (short)(4 * (short)m_iNumOfArcPoints + (4 + 4 + 4));
                m_fr_1 = 0.0f;
                m_fr_2 = fr;
                m_fSlopeTaper = ((m_fh - m_fd - 2 * m_fr_2) / 2.0f) / ((m_fb - m_ft_w - 2 * m_fr_2) / 2.0f);
            }
            else if (m_sShape == 2)  // 2 -  Four radii at flanges roots, tapered or parallel flanges (4 auxiliary points)
            {
                m_iTotNoPoints = (short)(4 * (short)m_iNumOfArcPoints + (4 + 6 + 6));
                m_fr_1 = fr;
                m_fr_2 = 0.0f;
                m_fSlopeTaper = (2 * ((m_fh - m_fd - 2 * m_fr_1)/ 2.0f - m_ft_f)) / ((m_fb - m_ft_w - 2 * m_fr_1) / 2.0f);
            }
            else // Exception
            { }

            //  m_fSlopeTaper = 0.08f; // Default

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data

            if (m_sShape == 1)       // 1 - Four radii at flanges tips, tapered or parallel flanges (8 auxiliary points)
                CalcCrSc_Coord_I_DS_1();
            else if (m_sShape == 2)  // 2 - Four radii at flanges roots, tapered or parallel flanges (4 auxiliary points)
                CalcCrSc_Coord_I_DS_2();
            else // Exception
            { }
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS_0() // 0 - Eight radii, tapered or parallel flanges (12 auxiliary points)
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 8 + 4;

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

            // Point No. 7
            m_CrScPoint[6,0] = m_CrScPoint[3,0];      // y
            m_CrScPoint[6,1] = -m_CrScPoint[3,1];     // z

            // Point No. 8
            m_CrScPoint[7,0] = m_CrScPoint[2,0];      // y
            m_CrScPoint[7,1] = -m_CrScPoint[2,1];     // z

            // Point No. 9
            m_CrScPoint[8,0] = m_CrScPoint[1,0];      // y
            m_CrScPoint[8,1] = -m_CrScPoint[1,1];     // z

            // Point No. 10
            m_CrScPoint[9,0] = m_CrScPoint[0,0];      // y
            m_CrScPoint[9,1] = -m_CrScPoint[0,1];     // z

            // Point No. 11
            m_CrScPoint[10,0] = -m_CrScPoint[5,0];    // y
            m_CrScPoint[10,1] = m_CrScPoint[5,1];     // z

            // Point No. 12
            m_CrScPoint[11, 0] = -m_CrScPoint[4, 0];  // y
            m_CrScPoint[11, 1] = m_CrScPoint[4, 1];   // z

            // Surface points

            // Point No. 13 - 1st Edge point - upper left
            m_CrScPoint[iNumberAux, 0] = -m_fb / 2.0f;     // y
            m_CrScPoint[iNumberAux, 1] = m_fh / 2.0f;      // z

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

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS_1() // 1 - Four radii at flanges tips, tapered or parallel flanges (8 auxiliary points)
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 4 + 4;

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fb / 2f + m_fr_2;    // y
            m_CrScPoint[0, 1] = m_fh / 2f;              // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_ft_w / 2.0f;         // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];           // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[1, 0];      // y
            m_CrScPoint[2, 1] = m_CrScPoint[0, 1];       // z

            // Point No. 4
            m_CrScPoint[3, 0] = -m_CrScPoint[0, 0];      // y
            m_CrScPoint[3, 1] = m_CrScPoint[0, 1];     // z

            // Point No. 5
            m_CrScPoint[4, 0] = m_CrScPoint[3, 0];      // y
            m_CrScPoint[4, 1] = -m_CrScPoint[3, 1];     // z

            // Point No. 6
            m_CrScPoint[5, 0] = m_CrScPoint[2, 0];      // y
            m_CrScPoint[5, 1] = -m_CrScPoint[2, 1];     // z

            // Point No. 7
            m_CrScPoint[6, 0] = m_CrScPoint[1, 0];    // y
            m_CrScPoint[6, 1] = -m_CrScPoint[1, 1];     // z

            // Point No. 8
            m_CrScPoint[7, 0] = m_CrScPoint[0, 0];  // y
            m_CrScPoint[7, 1] = -m_CrScPoint[0, 1];   // z

            // Surface points

            // Point No. 13 - 1st Edge point - upper left
            m_CrScPoint[iNumberAux, 0] = -m_fb / 2.0f;     // y
            m_CrScPoint[iNumberAux, 1] = m_fh / 2.0f;      // z

            int iRadiusAngle = 90; // Radius Angle

            // 2nd radius - centre "3" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + i + 1, 0] = m_CrScPoint[3, 0] + Geom2D.GetPositionX(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + i + 1, 1] = m_CrScPoint[3, 1] + Geom2D.GetPositionY_CW(m_fr_2, 0 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // Point No. XX
            m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 1, 0] = m_ft_w / 2.0f;      // y
            m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 1, 1] = m_CrScPoint[0, 1] - ((m_fh - m_fd - 2 * +m_fr_2) / 2.0f); // z

            // Point No. XX
            m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 2, 0] = m_ft_w / 2.0f;         // y
            m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 2, 1] = -m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 1, 1];     // z

            // 3rd radius - centre "4" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 3, 0] = m_CrScPoint[4, 0] + Geom2D.GetPositionX(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 3, 1] = m_CrScPoint[4, 1] + Geom2D.GetPositionY_CW(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 4th radius - centre "7" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 3, 0] = m_CrScPoint[7, 0] + Geom2D.GetPositionX(m_fr_2, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 3, 1] = m_CrScPoint[7, 1] + Geom2D.GetPositionY_CW(m_fr_2, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 8, 0] = - m_ft_w / 2.0f;         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 8, 1] = m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 2, 1];     // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 9, 0] = - m_ft_w / 2.0f;         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 9, 1] = m_CrScPoint[iNumberAux + m_iNumOfArcPoints + 1, 1];     // z

            // 1st radius - centre "0" (90-180 degrees)
            // Do not create last point of segment - it is already defined
            for (short i = 0; i < m_iNumOfArcPoints - 1; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 10, 0] = m_CrScPoint[0, 0] + Geom2D.GetPositionX(m_fr_2, 90 + i * iRadiusAngle / m_iNumOfArcSegment);      // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 10, 1] = m_CrScPoint[0, 1] + Geom2D.GetPositionY_CW(m_fr_2, 90 + i * iRadiusAngle / m_iNumOfArcSegment);    // z
            }
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS_2() // 2 - Four radii at flanges roots, tapered or parallel flanges (4 auxiliary points)
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 4;

            // Point No. 1
            m_CrScPoint[0, 0] = -m_ft_w / 2f;           // y
            m_CrScPoint[0, 1] = m_fd  /2.0f + m_fr_1;   // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_CrScPoint[0, 0];     // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];      // z

            // Point No. 3
            m_CrScPoint[2, 0] = m_CrScPoint[1, 0];      // y
            m_CrScPoint[2, 1] = -m_CrScPoint[0, 1];     // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[0, 0];      // y
            m_CrScPoint[3, 1] = -m_CrScPoint[0, 1];     // z

            // Surface points

            // Point No. 5
            m_CrScPoint[4, 0] = -m_fb / 2.0f;           // y
            m_CrScPoint[4, 1] = m_fh / 2.0f;           // z

            // Point No. 6
            m_CrScPoint[5, 0] = -m_ft_w / 2.0f - m_fr_1; ;  // y
            m_CrScPoint[5, 1] = m_CrScPoint[4, 1];         // z

            // Point No. 7
            m_CrScPoint[6, 0] = -m_CrScPoint[5, 0];    // y
            m_CrScPoint[6, 1] = m_CrScPoint[5, 1];     // z

            // Point No. 8
            m_CrScPoint[7, 0] = -m_CrScPoint[4, 0];     // y
            m_CrScPoint[7, 1] = m_CrScPoint[4, 1];     // z

            // Point No. 9
            m_CrScPoint[8, 0] = -m_CrScPoint[4, 0];      // y
            m_CrScPoint[8, 1] = m_fd / 2 + m_fr_1 +  (2 * ((m_fh - m_fd - 2 * m_fr_1) / 2.0f - m_ft_f));     // z

            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "1" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + i + 4 + 1, 0] = m_CrScPoint[1, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + i + 4 + 1, 1] = m_CrScPoint[1, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 90 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 2nd radius - centre "2" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 4 + 1, 0] = m_CrScPoint[2, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i + 4 + 1, 1] = m_CrScPoint[2, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 5, 0] = m_CrScPoint[8, 0];         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 5, 1] = -m_CrScPoint[8, 1];        // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 6, 0] = m_CrScPoint[7, 0];         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 6, 1] = -m_CrScPoint[7, 1];        // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 7, 0] = m_CrScPoint[6, 0];         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 7, 1] = -m_CrScPoint[6, 1];        // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 8, 0] = m_CrScPoint[5, 0];         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 8, 1] = -m_CrScPoint[5, 1];        // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 9, 0] = m_CrScPoint[4, 0];         // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 9, 1] = -m_CrScPoint[4, 1];        // z

            // Point No. XX
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 10, 0] = m_CrScPoint[4, 0];       // y
            m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + 10, 1] = -m_CrScPoint[8, 1];        // z

            // 3rd radius - centre "3" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 11, 0] = m_CrScPoint[3, 0] - m_fr_1 + Geom2D.GetPositionX(m_fr_1, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i + 11, 1] = m_CrScPoint[3, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 270 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 4th radius - centre "0" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 11, 0] = m_CrScPoint[0, 0] - m_fr_1 + Geom2D.GetPositionX(m_fr_1, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints + i + 11, 1] = m_CrScPoint[0, 1] - m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 0 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Point No. XX
            m_CrScPoint[iNumberAux + 4 * m_iNumOfArcPoints + 11, 0] = m_CrScPoint[4, 0];       // y
            m_CrScPoint[iNumberAux + 4 * m_iNumOfArcPoints + 11, 1] = m_CrScPoint[8, 1];        // z

        }
    }
}
