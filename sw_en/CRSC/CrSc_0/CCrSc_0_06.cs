﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CRSC
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_06 : CCrSc
    {
        // Solid Penthagon

        //----------------------------------------------------------------------------
        private float m_fa;   // Side
        private float m_fd;   // Circumscribed Circle Diameter / Polygon is Inscribed in Circle
        private short m_iTotNoPoints; // Total Number of Cross-section Points for Drawing
        public  float[,] m_CrScPoint; // Array of Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
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
        public CCrSc_0_06()  {   }
        public CCrSc_0_06(float fa)
        {
            m_iTotNoPoints = 5+1; // Total Number of Points in Section (1 point for Centroid)
            m_fa = fa;
  
            // Calculate Diameter of Circumscribed Circle
            m_fd = Geom2D.GetRadiusfromSideLength(m_fa, 5);

            // Create Array - allocate memory
            m_CrScPoint = new float[m_iTotNoPoints, 2];
            
            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Edge Points Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)
            m_CrScPoint = Geom2D.GetPentagonPointCoord(m_fa);

            // Centroid
            m_CrScPoint[ITotNoPoints - 1, 0] = 0f;
            m_CrScPoint[ITotNoPoints - 1, 1] = 0f;
        }
    }
}
