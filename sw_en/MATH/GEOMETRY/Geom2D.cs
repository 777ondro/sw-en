using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MATH
{
    public static class Geom2D
    {
        public static float[,] m_ArrfPointsCoord2D;

        // Get Basic 2D Shapes Coordinates
        #region Circle
        // Circle
        // Transformation of coordinates
        static float GetPositionX(float radius, float theta)
        {
            return radius * (float)Math.Cos(theta * Math.PI / 180);
        }

        static float GetPositionY(float radius, float theta)
        {
            // Clock-wise (for counterclock-wise change sign for vertical coordinate)
            return -radius * (float)Math.Sin(theta * Math.PI / 180);
        }

        // Points Coordinates
        public static float[,] GetCirclePointCoord(float fr, int iNumber)
        {
            m_ArrfPointsCoord2D = new float[iNumber, 2];

            for (int i = 0; i < iNumber; i++)
            {
                m_ArrfPointsCoord2D[i, 0] = GetPositionX(fr, i * 360 / iNumber);  // y
                m_ArrfPointsCoord2D[i, 1] = GetPositionY(fr, i * 360 / iNumber);  // z
            }

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Arc
        // Arc
        public static float[,] GetArcPointCoord(float fr, int fStartAngle, int fEndAngle, int iNumber)
        {
            m_ArrfPointsCoord2D = new float[iNumber, 2];

            for (int i = 0; i < fEndAngle - fStartAngle; i += (fEndAngle - fStartAngle) / iNumber)
            {
                m_ArrfPointsCoord2D[i, 0] = GetPositionX(fr, fStartAngle + i * (fEndAngle - fStartAngle) / iNumber);  // y
                m_ArrfPointsCoord2D[i, 1] = GetPositionY(fr, fStartAngle + i * (fEndAngle - fStartAngle) / iNumber);  // z
            }

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Ellipse
        // Ellipse
        // Get Points Coordinates
        /*
        * This functions returns an array containing 36 points to draw an
        * ellipse.
        *
        * @param fx {float} X coordinate
        * @param fy {float} Y coordinate
        * @param fa {float} Semimajor axis
        * @param fb {float} Semiminor axis
        * @param fangle {float} Angle of the ellipse in Degrees

        * Read more: http://www.answers.com/topic/ellipse#ixzz1UN0OIaGS
        */
        public static float[,] GetEllipsePointCoord(float fa, float fb, float fAngle, int isteps)
        {
            //if (isteps == null)
            //    isteps = 36;

            float[,] farr_points = new float[isteps, 2];

            // Angle is given by Degree Value
            float fBeta = -fAngle * (MathF.fPI / 180f); //(Math.PI/180) converts Degree Value into Radians
            float fsinbeta = (float)Math.Sin(fBeta);
            float fcosbeta = (float)Math.Cos(fBeta);

            int iNodeTemp = 0; // Temporary Number of Current Point

            for (int i = 0; i < 360; i += 360 / isteps)
            {
                float falpha = i * (MathF.fPI / 180);
                float fsinalpha = (float)Math.Sin(falpha);
                float fcosalpha = (float)Math.Cos(falpha);

                // Clock-wise (for counterclock-wise change sign for vertical coordinate)
                farr_points[iNodeTemp, 0] = fa * fcosalpha * fcosbeta - fb * fsinalpha * fsinbeta;      // Point x-coordinate
                farr_points[iNodeTemp, 1] = -(fa * fcosalpha * fsinbeta + fb * fsinalpha * fcosbeta);      // Point y-coordinate

                iNodeTemp++;
            }
            return farr_points;
        }

        public static float[,] GetEllipsePoints(float fx, float fy, float fa, float fb, float fangle, int isteps)
        {
            //if (isteps == null)
            //    isteps = 36;
            //if (fangle == null)
            //    fangle = 0f;

            float[,] farr_points = new float[isteps, 2];

            // Angle is given by Degree Value
            float fBeta = -fangle * (MathF.fPI / 180f); //(Math.PI/180) converts Degree Value into Radians
            float fsinbeta = (float)Math.Sin(fBeta);
            float fcosbeta = (float)Math.Cos(fBeta);

            int iNodeTemp = 0; // Temporary Number of Current Point

            for (int i = 0; i < 360; i += 360 / isteps)
            {
                float falpha = i * (MathF.fPI / 180);
                float fsinalpha = (float)Math.Sin(falpha);
                float fcosalpha = (float)Math.Cos(falpha);

                // Clock-wise (for counterclock-wise change sign for vertical coordinate)
                farr_points[iNodeTemp, 0] = fx + (fa * fcosalpha * fcosbeta - fb * fsinalpha * fsinbeta);      // Point x-coordinate
                farr_points[iNodeTemp, 1] = fy - (fa * fcosalpha * fsinbeta + fb * fsinalpha * fcosbeta);      // Point y-coordinate

                iNodeTemp++;
            }
            return farr_points;
        }
        #endregion
        #region Triangle
        // Triangle
        public static float[,] GetTrianEqLatPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[3, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_ArrfPointsCoord2D[0, 0] = 0f;                                   // y
            m_ArrfPointsCoord2D[0, 1] = 2f / 3f * (fa / 2f) * MathF.fSqrt3;   // z

            // Point No. 2
            m_ArrfPointsCoord2D[1, 0] = fa / 2f;                              // y
            m_ArrfPointsCoord2D[1, 1] = -1f / 3f * (fa / 2f) * MathF.fSqrt3;  // z

            // Point No. 3
            m_ArrfPointsCoord2D[2, 0] = -m_ArrfPointsCoord2D[1, 0];           // y
            m_ArrfPointsCoord2D[2, 1] = m_ArrfPointsCoord2D[1, 1];            // z

            return m_ArrfPointsCoord2D;
        }
        // Isosceles
        public static float[,] GetTrianIsosCelPointCoord(float fh, float fb)
        {
            m_ArrfPointsCoord2D = new float[3, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_ArrfPointsCoord2D[0, 0] = 0f;               // y
            m_ArrfPointsCoord2D[0, 1] = 2f / 3f * fh;     // z

            // Point No. 2
            m_ArrfPointsCoord2D[1, 0] = fb / 2f;          // y
            m_ArrfPointsCoord2D[1, 1] = -1f / 3f * fh;    // z

            // Point No. 3
            m_ArrfPointsCoord2D[2, 0] = -m_ArrfPointsCoord2D[1, 0];  // y
            m_ArrfPointsCoord2D[2, 1] = m_ArrfPointsCoord2D[1, 1];   // z

            return m_ArrfPointsCoord2D;
        }
        // Right triangle (right-angled triangle, rectangled triangle)
        public static float[,] GetTrianRightAngPointCoord(float fh, float fb)
        {
            m_ArrfPointsCoord2D = new float[3, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_ArrfPointsCoord2D[0, 0] = -fb / 3f;        // y
            m_ArrfPointsCoord2D[0, 1] = 2f / 3f * fh;     // z

            // Point No. 2
            m_ArrfPointsCoord2D[1, 0] = 2f / 3f * fb;     // y
            m_ArrfPointsCoord2D[1, 1] = -1f / 3f * fh;    // z

            // Point No. 3
            m_ArrfPointsCoord2D[2, 0] = m_ArrfPointsCoord2D[0, 0];  // y
            m_ArrfPointsCoord2D[2, 1] = m_ArrfPointsCoord2D[1, 1];  // z

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Square
        // Square (4)
        public static float[,] GetSquarePointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[4, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_ArrfPointsCoord2D[0, 0] = -fa/2f;      // y
            m_ArrfPointsCoord2D[0, 1] = fa / 2f;     // z

            // Point No. 2
            m_ArrfPointsCoord2D[1, 0] = -m_ArrfPointsCoord2D[0, 0];   // y
            m_ArrfPointsCoord2D[1, 1] = m_ArrfPointsCoord2D[0, 1];    // z

            // Point No. 3
            m_ArrfPointsCoord2D[2, 0] = -m_ArrfPointsCoord2D[0, 0];  // y
            m_ArrfPointsCoord2D[2, 1] = -m_ArrfPointsCoord2D[0, 1];  // z

            // Point No. 4
            m_ArrfPointsCoord2D[3, 0] = m_ArrfPointsCoord2D[0, 0];   // y
            m_ArrfPointsCoord2D[3, 1] = -m_ArrfPointsCoord2D[0, 1];  // z

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Rectangle
        // Rectangle (4)
        public static float[,] GetRectanglePointCoord(float fh, float fb)
        {
            m_ArrfPointsCoord2D = new float[4, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_ArrfPointsCoord2D[0, 0] = -fb / 2f;      // y
            m_ArrfPointsCoord2D[0, 1] = fh / 2f;       // z

            // Point No. 2
            m_ArrfPointsCoord2D[1, 0] = -m_ArrfPointsCoord2D[0, 0];   // y
            m_ArrfPointsCoord2D[1, 1] = m_ArrfPointsCoord2D[0, 1];    // z

            // Point No. 3
            m_ArrfPointsCoord2D[2, 0] = -m_ArrfPointsCoord2D[0, 0];  // y
            m_ArrfPointsCoord2D[2, 1] = -m_ArrfPointsCoord2D[0, 1];  // z

            // Point No. 4
            m_ArrfPointsCoord2D[3, 0] = m_ArrfPointsCoord2D[0, 0];   // y
            m_ArrfPointsCoord2D[3, 1] = -m_ArrfPointsCoord2D[0, 1];  // z

            return m_ArrfPointsCoord2D;
        }
        #endregion
        // Regular Polygonal Shapes

        private static float GetRadiusfromSideLength(float fa, short iNumEdges)
        {
            return fa / 2f * 1f / ((float)Math.Sin(360 / (2 * iNumEdges)));
        }

        #region Pentagon
        // Pentagon (5)
        public static float[,] GetPentagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[5, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 5), 5);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Hexagon
        // Hexafgon (6)
        public static float[,] GetHexagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[6, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 6), 6);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Heptagon
        // Heptagon (7)
        public static float[,] GetHeptagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[7, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 7), 7);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Octagon
        // Octagon  (8)
        public static float[,] GetOctagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[8, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 8), 8);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Nonagon
        // Nonagon  (9)
        public static float[,] GetNonagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[9, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 9), 9);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Decagon
        // Decagon  (10)
        public static float[,] GetDecagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[10, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 10), 10);

            return m_ArrfPointsCoord2D;
        }
        #endregion        
        #region Hendecagon
        // Hendecagon(11)
        public static float[,] GetHendecagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[11, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 11),11);

            return m_ArrfPointsCoord2D;
        }
        #endregion        
        #region Dodecagon
        // Dodecagon (12)
        public static float[,] GetPentagonPointCoord(float fa)
        {
            m_ArrfPointsCoord2D = new float[12, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, 12), 12);

            return m_ArrfPointsCoord2D;
        }
        #endregion
        #region Polygon
        // Nonagon  (n)
        public static float[,] GetNonagonPointCoord(float fa, short iNumEdges)
        {
            m_ArrfPointsCoord2D = new float[iNumEdges, 2];

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            m_ArrfPointsCoord2D = GetCirclePointCoord(GetRadiusfromSideLength(fa, iNumEdges), iNumEdges);

            return m_ArrfPointsCoord2D;
        }
        #endregion








    }
}
