using System;
using MATH;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_3_07 : CCrSc
    {
        // Rolled / Cold-formed rectangular / square hollow section

        //----------------------------------------------------------------------------
        private short m_sShape;       // Section shape

        // 0 - two radius, same centre point (4 auxialiary points)
        // 1 - two radius, diff centre point (8 auxialiary points)
        // 2 - Outside radius = 0
        // 3 - Inside radius = 0
        // 4 - Both radii = 0

        private float m_fh;                 // Depth - Height / Vyska
        private float m_fb;                 // Width  / Sirka
        private float m_ft;                 // Thickness / Hrubka
        private float m_fr_out;             // Radius outside / Polomer vonkajsi
        private float m_fr_in;              // Radius inside / Polomer vnutorny
        private short m_iNumOfArcSegment;   // Number of Arc Segments
        private short m_iNumOfArcPoints;    // Number of Arc Points
        private short m_iNoPoints;          // Number of Cross-section Points for
        public float[,] m_CrScPointOut;     // Array of Outside Points and values in 2D
        public float[,] m_CrScPointIn;      // Array of Inside Points and values in 2D
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
        public float Ft
        {
            get { return m_ft; }
            set { m_ft = value; }
        }
        public float Fr_out
        {
            get { return m_fr_out; }
            set { m_fr_out = value; }
        }
        public float Fr_in
        {
            get { return m_fr_in; }
            set { m_fr_in = value; }
        }
        public short INoPoints
        {
            get { return m_iNoPoints; }
            set { m_iNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_3_07()  {   }
        public CCrSc_3_07(short sShape, float fh, float fb, float ft, float fr)
        {
            // 0 - two radius, same centre point (4 auxialiary points)
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iNoPoints = (short)(4 * (short)m_iNumOfArcPoints + 4);

            m_sShape = sShape;
            m_fh = fh;
            m_fb = fb;
            m_ft = ft;

            if (m_sShape == 0)
            {
                m_fr_out = fr;
                m_fr_in = m_fr_out - ft;
            }
            else if (m_sShape == 2)
            {
                m_fr_out = fr;
                m_fr_in = 0f;
            }
            else if (m_sShape == 3)
            {
               m_fr_out = 0f;
               m_fr_in = fr;
            }

            // Create Array - allocate memory
            m_CrScPointOut = new float[INoPoints, 2];
            m_CrScPointIn = new float[INoPoints, 2];

            // Fill Array Data

            if (m_sShape == 0)      // Both radii, coincident centres
                CalcCrSc_Coord_0();
            else if (m_sShape == 2) // Outside radius
                CalcCrSc_Coord_2();
            else if (m_sShape == 3) // Inside radius
                CalcCrSc_Coord_3();
            //else
        }

        public CCrSc_3_07(short sShape, float fh, float fb, float ft, float fr_out, float fr_in)
        {
            // 1 - two radius, diff centre point (8 auxialiary points)
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iNoPoints = (short)(4 * (short)m_iNumOfArcPoints + 4);

            m_sShape = sShape;
            m_fh = fh;
            m_fb = fb;
            m_ft = ft;
            m_fr_out = fr_out;
            m_fr_in = fr_in;

            // Create Array - allocate memory
            m_CrScPointOut = new float[INoPoints, 2];
            m_CrScPointIn = new float[INoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord_0();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_0()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            short iNumberAux = 4;

            // Outside points
            // Auxialiary nodes per section

            // Point No. 1
            m_CrScPointOut[0, 0] = -m_fb / 2f + m_fr_out;    // y
            m_CrScPointOut[0, 1] = m_fh / 2f - m_fr_out;     // z

            // Point No. 2
            m_CrScPointOut[1, 0] = -m_CrScPointOut[0, 0];        // y
            m_CrScPointOut[1, 1] = m_CrScPointOut[0, 1];         // z

            // Point No. 3
            m_CrScPointOut[2, 0] = -m_CrScPointOut[0, 0];        // y
            m_CrScPointOut[2, 1] = -m_CrScPointOut[1, 1];        // z

            // Point No. 4
            m_CrScPointOut[3, 0] = m_CrScPointOut[0, 0];         // y
            m_CrScPointOut[3, 1] = m_CrScPointOut[2, 1];         // z

            // Surface points

            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "1" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointOut[iNumberAux + i, 0] = m_CrScPointOut[0, 0] + Geom2D.GetPositionX(m_fr_out, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointOut[iNumberAux + i, 1] = m_CrScPointOut[0, 1] + Geom2D.GetPositionY_CW(m_fr_out, 180 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 2nd radius - centre "2" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 0] = m_CrScPointOut[1, 0] + Geom2D.GetPositionX(m_fr_out, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 1] = m_CrScPointOut[1, 1] + Geom2D.GetPositionY_CW(m_fr_out, 270 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 3rd radius - centre "3" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointOut[iNumberAux + 2 * m_iNumOfArcPoints + i, 0] = m_CrScPointOut[2, 0] + Geom2D.GetPositionX(m_fr_out, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointOut[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = m_CrScPointOut[2, 1] + Geom2D.GetPositionY_CW(m_fr_out, 0 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 4th radius - centre "4" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 0] = m_CrScPointOut[3, 0] + Geom2D.GetPositionX(m_fr_out, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 1] = m_CrScPointOut[3, 1] + Geom2D.GetPositionY_CW(m_fr_out, 90 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Inside points
            // Auxialiary nodes per section

            // Point No. 1
            m_CrScPointIn[0, 0] = -m_fb / 2f + m_fr_in + m_ft;    // y
            m_CrScPointIn[0, 1] = m_fh / 2f - m_fr_in - m_ft;     // z

            // Point No. 2
            m_CrScPointIn[1, 0] = -m_CrScPointIn[0, 0];        // y
            m_CrScPointIn[1, 1] = m_CrScPointIn[0, 1];         // z

            // Point No. 3
            m_CrScPointIn[2, 0] = -m_CrScPointIn[0, 0];        // y
            m_CrScPointIn[2, 1] = -m_CrScPointIn[1, 1];        // z

            // Point No. 4
            m_CrScPointIn[3, 0] = m_CrScPointIn[0, 0];         // y
            m_CrScPointIn[3, 1] = m_CrScPointIn[2, 1];         // z

            // Surface points

            // 1st radius - centre "1" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + i, 0] = m_CrScPointIn[0, 0] + Geom2D.GetPositionX(m_fr_in, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + i, 1] = m_CrScPointIn[0, 1] + Geom2D.GetPositionY_CW(m_fr_in, 180 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 2nd radius - centre "2" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + m_iNumOfArcPoints + i, 0] = m_CrScPointIn[1, 0] + Geom2D.GetPositionX(m_fr_in, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + m_iNumOfArcPoints + i, 1] = m_CrScPointIn[1, 1] + Geom2D.GetPositionY_CW(m_fr_in, 270 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 3rd radius - centre "3" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + 2 * m_iNumOfArcPoints + i, 0] = m_CrScPointIn[2, 0] + Geom2D.GetPositionX(m_fr_in, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = m_CrScPointIn[2, 1] + Geom2D.GetPositionY_CW(m_fr_in, 0 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 4th radius - centre "4" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + 3 * m_iNumOfArcPoints + i, 0] = m_CrScPointIn[3, 0] + Geom2D.GetPositionX(m_fr_in, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + 3 * m_iNumOfArcPoints + i, 1] = m_CrScPointIn[3, 1] + Geom2D.GetPositionY_CW(m_fr_in, 90 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_2()
        {


        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_3()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            short iNumberAux = 4;

            // Number of segments should be even-numbered

            // Outside points
            // Auxialiary nodes per section

            // Point No. 1
            m_CrScPointOut[0, 0] = -m_fb / 2f + m_fr_in + m_ft;    // y
            m_CrScPointOut[0, 1] = m_fh / 2f - m_fr_in - m_ft;     // z

            // Point No. 2
            m_CrScPointOut[1, 0] = -m_CrScPointOut[0, 0];        // y
            m_CrScPointOut[1, 1] = m_CrScPointOut[0, 1];         // z

            // Point No. 3
            m_CrScPointOut[2, 0] = -m_CrScPointOut[0, 0];        // y
            m_CrScPointOut[2, 1] = -m_CrScPointOut[1, 1];        // z

            // Point No. 4
            m_CrScPointOut[3, 0] = m_CrScPointOut[0, 0];         // y
            m_CrScPointOut[3, 1] = m_CrScPointOut[2, 1];         // z

            // Surface points

            float fOutSegmLength = 2* (m_ft + m_fr_in) / m_iNumOfArcSegment;

            // 1st radius - centre "1"
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                if (i <= m_iNumOfArcPoints / 2) // Vertical
                {
                    m_CrScPointOut[iNumberAux + i, 0] = -m_fb / 2f;                                             // y
                    m_CrScPointOut[iNumberAux + i, 1] = m_fh / 2f - (m_iNumOfArcSegment/2 - i) * fOutSegmLength;  // z
                }
                else // Horizontal
                {
                    m_CrScPointOut[iNumberAux + i, 0] = -m_fb / 2f + (i - m_iNumOfArcSegment/2) * fOutSegmLength; // y                                       // y
                    m_CrScPointOut[iNumberAux + i, 1] = m_fh / 2f;                                              // z                
                }
            }

            // 2nd radius - centre "2"
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                if (i <= m_iNumOfArcPoints / 2) // Horizontal
                {
                    m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 0] = m_fb / 2f - (m_iNumOfArcSegment/2 - i) * fOutSegmLength; // y                                       // y
                    m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 1] = m_fh / 2f;                                             // z 
                }
                else // Vertical
                {
                    m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 0] = m_fb / 2f;                                             // y
                    m_CrScPointOut[iNumberAux + m_iNumOfArcPoints + i, 1] = m_fh / 2f - i/2 * fOutSegmLength;                        // z
                }
            }

            // 3rd radius - centre "3"
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                if (i <= m_iNumOfArcPoints / 2) // Vertical
                {
                    m_CrScPointOut[iNumberAux + 2*m_iNumOfArcPoints + i, 0] = m_fb / 2f;                                               // y
                    m_CrScPointOut[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = -m_fh / 2f + (m_iNumOfArcSegment/2 - i) * fOutSegmLength;  // z
                }
                else // Horizontal
                {
                    m_CrScPointOut[iNumberAux + 2 * m_iNumOfArcPoints + i, 0] = m_fb / 2f - i/2 * fOutSegmLength;                         // y                                       // y
                    m_CrScPointOut[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = -m_fh / 2f;                                              // z                
                }
            }

            // 4th radius - centre "4"
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                if (i <= m_iNumOfArcPoints / 2) // Horizontal
                {
                    m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 0] = -m_fb / 2f + (m_iNumOfArcSegment/2 - i) * fOutSegmLength; // y                                       // y
                    m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 1] = -m_fh / 2f;                                             // z 
                }
                else // Vertical
                {
                    m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 0] = -m_fb / 2f;                                             // y
                    m_CrScPointOut[iNumberAux + 3 * m_iNumOfArcPoints + i, 1] = -m_fh / 2f + i/2 * fOutSegmLength;                       // z
                }
            }


            // Inside points
            // Auxialiary nodes per section

            // Point No. 1
            m_CrScPointIn[0, 0] = -m_fb / 2f + m_fr_in + m_ft;    // y
            m_CrScPointIn[0, 1] = m_fh / 2f - m_fr_in - m_ft;     // z

            // Point No. 2
            m_CrScPointIn[1, 0] = -m_CrScPointIn[0, 0];        // y
            m_CrScPointIn[1, 1] = m_CrScPointIn[0, 1];         // z

            // Point No. 3
            m_CrScPointIn[2, 0] = -m_CrScPointIn[0, 0];        // y
            m_CrScPointIn[2, 1] = -m_CrScPointIn[1, 1];        // z

            // Point No. 4
            m_CrScPointIn[3, 0] = m_CrScPointIn[0, 0];         // y
            m_CrScPointIn[3, 1] = m_CrScPointIn[2, 1];         // z

            // Surface points

            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "1" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + i, 0] = m_CrScPointIn[0, 0] + Geom2D.GetPositionX(m_fr_in, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + i, 1] = m_CrScPointIn[0, 1] + Geom2D.GetPositionY_CW(m_fr_in, 180 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 2nd radius - centre "2" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + m_iNumOfArcPoints + i, 0] = m_CrScPointIn[1, 0] + Geom2D.GetPositionX(m_fr_in, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + m_iNumOfArcPoints + i, 1] = m_CrScPointIn[1, 1] + Geom2D.GetPositionY_CW(m_fr_in, 270 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 3rd radius - centre "3" (0-90 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + 2 * m_iNumOfArcPoints + i, 0] = m_CrScPointIn[2, 0] + Geom2D.GetPositionX(m_fr_in, 0 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = m_CrScPointIn[2, 1] + Geom2D.GetPositionY_CW(m_fr_in, 0 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 4th radius - centre "4" (90-180 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPointIn[iNumberAux + 3 * m_iNumOfArcPoints + i, 0] = m_CrScPointIn[3, 0] + Geom2D.GetPositionX(m_fr_in, 90 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPointIn[iNumberAux + 3 * m_iNumOfArcPoints + i, 1] = m_CrScPointIn[3, 1] + Geom2D.GetPositionY_CW(m_fr_in, 90 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }
        }
    }
}
