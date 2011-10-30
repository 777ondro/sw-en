using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CRSC
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_61 : CCrSc
    {
        // Doubly symmetric Cruciform

        /*
         
        
          9   1    3   4       
             _      _          
             \ \ 2 / /         
              \ \ / /    t     
               \ * /           
              8 | |  5         
                | |    b       
                |_|            
              7      6         
                                    
      
        
         Centroid [0,0]
         
        z 
        /|\
         | 
         |
         |_____________\  y
                       /
         */


        //----------------------------------------------------------------------------
        private float m_fb;   // Width  / Sirka
        private float m_ft;   // Thickness / Hrubka 
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
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
        public short ITotNoPoints
        {
            get { return m_iTotNoPoints; }
            set { m_iTotNoPoints = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_61()  {   }
        public CCrSc_0_61(float fb, float ft)
        {
            m_iTotNoPoints = 9;
            m_fb = fb;
            m_ft = ft;
 
            // Create Array - allocate memory
            m_CrScPoint = new float [m_iTotNoPoints,2];
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {

            // Polar Coordinates of Points 1,3,4,6,7,9
            // Auxiliary Angle
            float fAlpha_Aux = (float)Math.Atan(m_ft / 2 / (m_fb + m_ft / 3));
            // Calculate Radius
            float fr = (m_ft / 2f) / (float)Math.Sin(fAlpha_Aux);

            // Calculate coordinates of 2, 5, 8 - Equilateral Triangle
            float[,] fArrTemp = new float[3, 2];
            fArrTemp = Geom2D.GetTrianEqLatPointCoord1(m_ft);

            // Transform Radians to Degrees - input to GetPositionX/Y functions
            fAlpha_Aux = 180f / MathF.fPI * fAlpha_Aux;
            // Transform Degrees to Radians
            // fAlpha_Aux = MathF.fPI / 180f * fAlpha_Aux;

            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Point No. 1
            m_CrScPoint[0, 0] = Geom2D.GetPositionX(fr, 210f + fAlpha_Aux);    // y
            m_CrScPoint[0, 1] = Geom2D.GetPositionY_CW(fr, 210f + fAlpha_Aux);    // z

            // Point No. 2
            m_CrScPoint[1, 0] = fArrTemp[0, 0];     // y
            m_CrScPoint[1, 1] = fArrTemp[0, 1];     // z

            // Point No. 3
            m_CrScPoint[2, 0] = Geom2D.GetPositionX(fr, 330f - fAlpha_Aux);    // y
            m_CrScPoint[2, 1] = Geom2D.GetPositionY_CW(fr, 330f - fAlpha_Aux);    // z

            // Point No. 4
            m_CrScPoint[3, 0] = Geom2D.GetPositionX(fr, 330f + fAlpha_Aux);    // y
            m_CrScPoint[3, 1] = Geom2D.GetPositionY_CW(fr, 330f + fAlpha_Aux);    // z

            // Point No. 5
            m_CrScPoint[4, 0] = fArrTemp[1, 0];      // y
            m_CrScPoint[4, 1] = fArrTemp[1, 1];      // z

            // Point No. 6
            m_CrScPoint[5, 0] = Geom2D.GetPositionX(fr, 90f - fAlpha_Aux);    // y
            m_CrScPoint[5, 1] = Geom2D.GetPositionY_CW(fr, 90f - fAlpha_Aux);    // z

            // Point No. 7
            m_CrScPoint[6, 0] = Geom2D.GetPositionX(fr, 90f + fAlpha_Aux);    // y
            m_CrScPoint[6, 1] = Geom2D.GetPositionY_CW(fr, 90f + fAlpha_Aux);    // z

            // Point No. 8
            m_CrScPoint[7, 0] = fArrTemp[2, 0];      // y
            m_CrScPoint[7, 1] = fArrTemp[2, 1];      // z

            // Point No. 9
            m_CrScPoint[8, 0] = Geom2D.GetPositionX(fr, 210f - fAlpha_Aux);    // y
            m_CrScPoint[8, 1] = Geom2D.GetPositionY_CW(fr, 210f - fAlpha_Aux);    // z
        }
    }
}
