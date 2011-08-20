using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MATH
{
    public static class Geom2D
    {





        // 2D Shapes Coordinates

        // Triangle



        // Circle


        // Arc









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
        public static float[,] GetEllipsePoints(float fa, float fb, float fangle, int isteps)
        {
            //if (isteps == null)
            //    isteps = 36;

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












    }
}
