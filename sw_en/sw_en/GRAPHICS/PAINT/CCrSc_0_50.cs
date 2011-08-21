using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_50 : CCrSc
    {
        // Welded doubly symmetric I section

        /*
         
        
         
         
        1  ________________  2       ____|/
          |_______  _______|    t_f     /|
       12    11  | | 4       3           |
                 | |                     |
                 | |                     |
                 | |  t_w             h  |
                 |*|                     |
                 | |                     |
             10  | | 5       6           |
        9  ______| |_______              |
          |________________|         ____|/
        8                    7          /|
                  b
          |/_______________|/
         /|               /|
      
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */


        //----------------------------------------------------------------------------
        private float m_fh;   // Height / Vyska
        private float m_fb;   // Width  / Sirka
        private float m_ft_f; // Flange Thickness / Hrubka pasnice
        private float m_ft_w; // Web Thickness  / Hrubka steny/stojiny
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
        public CCrSc_0_50()  {   }
        public CCrSc_0_50(float fh, float fb, float ft_f, float ft_w)
        {
            m_iTotNoPoints = 12;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;

            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord_I_DS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_DS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0,0] = -m_fb / 2f;    // y
            m_CrScPoint[0,1] = m_fh / 2f;     // z

            // Point No. 2
            m_CrScPoint[1,0] = -m_CrScPoint[0,0];    // y
            m_CrScPoint[1,1] = m_CrScPoint[0,1];     // z

            // Point No. 3
            m_CrScPoint[2,0] = -m_CrScPoint[0,0];             // y
            m_CrScPoint[2,1] = m_CrScPoint[0,1] - m_ft_f;     // z

            // Point No. 4
            m_CrScPoint[3,0] = m_CrScPoint[2,0] - (m_fb-m_ft_w)/2f;    // y
            m_CrScPoint[3,1] = m_CrScPoint[2,1];                       // z

            // Point No. 5
            m_CrScPoint[4,0] = m_CrScPoint[3,0];      // y
            m_CrScPoint[4,1] = -m_CrScPoint[3,1];     // z

            // Point No. 6
            m_CrScPoint[5,0] = -m_CrScPoint[0,0];     // y
            m_CrScPoint[5,1] = -m_CrScPoint[3,1];     // z

            // Point No. 7
            m_CrScPoint[6,0] = -m_CrScPoint[0,0];     // y
            m_CrScPoint[6,1] = -m_CrScPoint[0,1];     // z

            // Point No. 8
            m_CrScPoint[7,0] = m_CrScPoint[0,0];      // y
            m_CrScPoint[7,1] = -m_CrScPoint[0,1];     // z

            // Point No. 9
            m_CrScPoint[8,0] = m_CrScPoint[0,0];     // y
            m_CrScPoint[8,1] = -m_CrScPoint[3,1];     // z

            // Point No. 10
            m_CrScPoint[9,0] = -m_CrScPoint[4,0];     // y
            m_CrScPoint[9,1] = -m_CrScPoint[3,1];     // z

            // Point No. 11
            m_CrScPoint[10,0] = -m_CrScPoint[4,0];    // y
            m_CrScPoint[10,1] = m_CrScPoint[2,1];     // z

            // Point No. 12
            m_CrScPoint[11,0] = m_CrScPoint[0,0];    // y
            m_CrScPoint[11,1] = m_CrScPoint[2,1];     // z
        }

        // Welded monosymmetric I section

        /*
         
                 b_fu
          |/_______________|/
         /|               /| 
         
         
        1  ________________  2            ____|/
          |_______  _______|    t_fu         /|
       12    11  | | 4       3                |
                 | |                          |
                 | |                          |
                 | |  t_w                  h  |
                 | |                          |
                 |*|                  ____|/  |
             10  | | 5       6           /|   |
     9  _________| |_________         z_c |   |
       |_____________________|  t_fb  ____|/__|/
      8                    7             /|  /|
                  b_fb
       |/____________________|/
      /|                    /|
     
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */

        private float m_fb_fu;   // Width of Upper Flange / Sirka hornej pasnice
        private float m_ft_fu;   // Upper Flange Thickness / Hrubka hornej pasnice
        private float m_fb_fb;   // Width of Bottom Flange / Sirka spodnej pasnice
        private float m_ft_fb;   // Bottom Flange Thickness / Hrubka spodnej pasnice
        private float m_fz_c;    // Centroid coordinate / Suradnica tažiska / Absolute value

        public CCrSc_0_50(float fh, float fb_fu, float fb_fb, float ft_fu,float ft_fb, float ft_w, float fz_c)
        {
            m_iTotNoPoints = 12;
            m_fh = fh;
            m_fb_fu = fb_fu;
            m_fb_fb = fb_fb;
            m_ft_fu = ft_fu;
            m_ft_fb = ft_fb;
            m_ft_w = ft_w;
            m_fz_c = Math.Abs(fz_c);

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            // Fill Array Data
            CalcCrSc_Coord_I_MS();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord_I_MS()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = -m_fb_fu / 2f;    // y
            m_CrScPoint[0, 1] = m_fh - m_fz_c;    // z

            // Point No. 2
            m_CrScPoint[1, 0] = -m_CrScPoint[0, 0];    // y
            m_CrScPoint[1, 1] = m_CrScPoint[0, 1];     // z

            // Point No. 3
            m_CrScPoint[2, 0] = -m_CrScPoint[0, 0];              // y
            m_CrScPoint[2, 1] = m_CrScPoint[0, 1] - m_ft_fu;     // z

            // Point No. 4
            m_CrScPoint[3, 0] = m_CrScPoint[2, 0] - (m_fb_fu - m_ft_w) / 2f;    // y
            m_CrScPoint[3, 1] = m_CrScPoint[2, 1];                              // z

            // Point No. 5
            m_CrScPoint[4, 0] = m_CrScPoint[3, 0];      // y
            m_CrScPoint[4, 1] = -m_fz_c + m_ft_fb;      // z

            // Point No. 6
            m_CrScPoint[5, 0] = m_fb_fb / 2f;           // y
            m_CrScPoint[5, 1] = m_CrScPoint[4, 1];      // z

            // Point No. 7
            m_CrScPoint[6, 0] = m_CrScPoint[5, 0];      // y
            m_CrScPoint[6, 1] = -m_fz_c;                // z

            // Point No. 8
            m_CrScPoint[7, 0] = -m_CrScPoint[6, 0];     // y
            m_CrScPoint[7, 1] = m_CrScPoint[6, 1];      // z

            // Point No. 9
            m_CrScPoint[8, 0] = -m_CrScPoint[5, 0];     // y
            m_CrScPoint[8, 1] = m_CrScPoint[5, 1];      // z

            // Point No. 10
            m_CrScPoint[9, 0] = -m_CrScPoint[4, 0];     // y
            m_CrScPoint[9, 1] = m_CrScPoint[4, 1];      // z

            // Point No. 11
            m_CrScPoint[10, 0] = -m_CrScPoint[3, 0];    // y
            m_CrScPoint[10, 1] = m_CrScPoint[3, 1];     // z

            // Point No. 12
            m_CrScPoint[11, 0] = -m_CrScPoint[2, 0];    // y
            m_CrScPoint[11, 1] = m_CrScPoint[2, 1];     // z
        }








    }
}
