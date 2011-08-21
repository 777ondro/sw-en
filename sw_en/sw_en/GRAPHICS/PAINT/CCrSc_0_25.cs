using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_25 : CCrSc
    {
        // Welded hollow section - doubly symmetrical

        /*
         
        
         
         
        1  ________________  2     ____|/
          |  ____________  |  t_f     /|
          | | 5        6 | |           |
          | |            | |           |
          | |            | |           |
      t_w | |            | |        h  |
          | |      *     | |           |
          | |            | |           |
          | | 8        7 | |           |
          | |____________| |           |
          |________________|       ____|/
        4                    3        /|
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
        public CCrSc_0_25()  {   }
        public CCrSc_0_25(float fh, float fb, float ft_f, float ft_w)
        {
            m_iTotNoPoints = 8;
            m_fh = fh;
            m_fb = fb;
            m_ft_f = ft_f;
            m_ft_w = ft_w;

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
            m_CrScPoint[0,0] = -m_fb / 2f;    // y
            m_CrScPoint[0,1] = m_fh / 2f;     // z

            // Point No. 2
            m_CrScPoint[1,0] = -m_CrScPoint[0,0];    // y
            m_CrScPoint[1,1] = m_CrScPoint[0,1];     // z

            // Point No. 3
            m_CrScPoint[2,0] = -m_CrScPoint[0,0];    // y
            m_CrScPoint[2,1] = -m_CrScPoint[1,1];    // z

            // Point No. 4
            m_CrScPoint[3,0] = m_CrScPoint[0,0];     // y
            m_CrScPoint[3,1] = m_CrScPoint[2,1];     // z

            // Point No. 5
            m_CrScPoint[4,0] = m_CrScPoint[0,0] + m_ft_w;  // y
            m_CrScPoint[4,1] = m_CrScPoint[0, 1] - m_ft_f; // z

            // Point No. 6
            m_CrScPoint[5,0] = -m_CrScPoint[4,0];    // y
            m_CrScPoint[5,1] = m_CrScPoint[4,1];     // z

            // Point No. 7
            m_CrScPoint[6,0] = m_CrScPoint[5,0];     // y
            m_CrScPoint[6,1] = -m_CrScPoint[5,1];    // z

            // Point No. 8
            m_CrScPoint[7,0] = m_CrScPoint[4,0];     // y
            m_CrScPoint[7,1] = -m_CrScPoint[4,1];     // z
        }
    }
}
