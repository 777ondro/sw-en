using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATH;

namespace CRSC
{
    public class CCrSc_3_03 : CCrSc
    {
        // Rolled monosymmetric L section (angle with equal legs)

        //----------------------------------------------------------------------------
        private float m_fb;                 // Width  / Sirka
        private float m_ft;                 // Leg Thickness / Hrubka ramena
        private float m_fr_1;               // Radius
        private float m_fr_2;               // Radius - flange edge
        private float m_fy_c;               // Centroid coordinate / Suradnica tažiska / Absolute value
        private short m_iNumOfArcSegment;   // Number of Arc Segments
        private short m_iNumOfArcPoints;    // Number of Arc Points
        private short m_iTotNoPoints;       // Total Number of Cross-section Points for Drawing
        public float[,] m_CrScPoint;        // Array of Points and values in 2D
        //----------------------------------------------------------------------------

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
        public CCrSc_3_03()  {   }
        public CCrSc_3_03(float fb, float ft, float fr_1, float fr_2, float fy_c)
        {
            m_iNumOfArcSegment = 8;
            m_iNumOfArcPoints = (short)(m_iNumOfArcSegment + 1); // Each arc is defined by number of segments + 1 point points
            m_iTotNoPoints = (short)(3 * (short)m_iNumOfArcPoints + 3 + 1);

            m_fb = fb;
            m_ft = ft;
            m_fr_1 = fr_1;
            m_fr_2 = fr_2;
            m_fy_c = fy_c;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_L_MS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_L_MS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Auxialiary nodes

            short iNumberAux = 3;

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fy_c;                         // y
            m_CrScPoint[0, 1] = m_fb - m_fy_c- m_fr_2;           // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_fy_c + m_ft;                 // y
            m_CrScPoint[1, 1] = -m_fy_c + m_ft;                 // z

            // Point No. 3
            m_CrScPoint[2, 0] = m_CrScPoint[0, 1];              // y
            m_CrScPoint[2, 1] = m_CrScPoint[0, 0];              // z

            // Surface points

            int iRadiusAngle = 90; // Radius Angle

            // 1st radius - centre "1" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux+i, 0] = m_CrScPoint[0, 0] + Geom2D.GetPositionX(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux+i, 1] = m_CrScPoint[0, 1] + Geom2D.GetPositionY_CW(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);  // z
            }

            // 2nd radius - centre "2" (180-270 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i, 0] = m_CrScPoint[1, 0] + m_fr_1 + Geom2D.GetPositionX(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + m_iNumOfArcPoints + i, 1] = m_CrScPoint[1, 1] + m_fr_1 + Geom2D.GetPositionY_CCW(m_fr_1, 180 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // 3rd radius - centre "3" (270-360 degrees)
            for (short i = 0; i < m_iNumOfArcPoints; i++)
            {
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i, 0] = m_CrScPoint[2, 0] + Geom2D.GetPositionX(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment);     // y
                m_CrScPoint[iNumberAux + 2 * m_iNumOfArcPoints + i, 1] = m_CrScPoint[2, 1] + Geom2D.GetPositionY_CW(m_fr_2, 270 + i * iRadiusAngle / m_iNumOfArcSegment); // z
            }

            // Point No.  - Last edge point - bottom left
            m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints, 0] = -m_fy_c;              // y
            m_CrScPoint[iNumberAux + 3 * m_iNumOfArcPoints, 1] = -m_fy_c;              // z
        }

		protected override void loadCrScIndices()
		{
			throw new NotImplementedException();
		}
	}
}
