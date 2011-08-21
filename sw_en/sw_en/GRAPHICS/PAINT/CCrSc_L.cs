using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_L : CCrSc
    {
        // Welded Angle section

        /*
         
        
         
         
        1  _  2              ____|/
          | |                   /|
          | |                    |
          | |                    |
          | |                    |
  t_a=t_w | |                 h  |
          | |                    |
          | |                    |
          | | 3  *  t_b=t_f      |
          | |___________  4      |
          |_____________|    ____|/
        6                 5     /|
                  b
          |/____________|/
         /|            /|
      
         y_c - from left
         z_c - from bottom
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */


        //----------------------------------------------------------------------------
        private float m_fh;   // Height / Vyska / Leg A
        private float m_fb;   // Width  / Sirka / Leg B
        private float m_ft_f; // Flange Thickness / Hrubka pasnice / Leg B
        private float m_ft_w; // Web Thickness  / Hrubka steny/stojiny / Leg A
        private float m_fy_c; // Centroid coordinate / Suradnica tažiska / Absolute value
        private float m_fz_c; // Centroid coordinate / Suradnica tažiska / Absolute value
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
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
        public float Fy_c
        {
            get { return m_fy_c; }
            set { m_fy_c = value; }
        }
        public float Fz_c
        {
            get { return m_fz_c; }
            set { m_fz_c = value; }
        }
        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }
        /*
        public float[,] CrScPoint
        {
            get { return m_CrScPoint; }
            set { m_CrScPoint = value; }
        }
        */

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_L()  {   }
        public CCrSc_L(float fh, float fb, float ft_f, float ft_w, float fy_c, float fz_c)
        {
            m_iTotNoPoints = 8;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;
            m_fy_c = Math.Abs(fy_c); // Absolute value
            m_fz_c = Math.Abs(fz_c); // Absolute value

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fy_c;           // y
            m_CrScPoint[0, 1] = m_fh - m_fz_c;     // z

            // Point No. 2
            m_CrScPoint[1, 0] = m_CrScPoint[0, 0] + m_ft_w;    // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];             // z

            // Point No. 3
            m_CrScPoint[2, 0] = m_CrScPoint[1, 0];    // y
            m_CrScPoint[2, 1] = -m_fz_c + m_ft_f;     // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[0, 0] + m_fb;     // y
            m_CrScPoint[3, 1] = m_CrScPoint[2, 1];            // z

            // Point No. 5
            m_CrScPoint[4, 0] = m_CrScPoint[3, 0];  // y
            m_CrScPoint[4, 1] = -m_fz_c;            // z

            // Point No. 6
            m_CrScPoint[5, 0] = m_CrScPoint[0, 0];     // y
            m_CrScPoint[5, 1] = m_CrScPoint[4, 1];     // z
        }
    }
}
