﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

namespace CRSC
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_28 : CCrSc
    {
        // Empty (Hollow) Octagon / Duty Osemuholnik

        //----------------------------------------------------------------------------
        private float m_fa_out;   // OutSide Length / Dlzka vonkajsej strany
        private float m_fa_in;    // InSide Length / Dlzka vnutornej strany
        private float m_fd1_out;   // Circumscribed Circle Diameter / Outside Polygon is Inscribed in Circle / Priemer opisanej kruznice vonkajsieho polygonu
        private float m_fd1_in;    // Circumscribed Circle Diameter / Inside Polygon is Inscribed in Circle / Priemer opisanej kruznice vnutorneho polygonu
        private float m_fd2_out;   // Inscribed Circle Diameter / Circle circumscribed Outside Polygon / Priemer vpisanej kruznice vonkajsieho polygonu
        private float m_fd2_in;    // Inscribed Circle Diameter / Circle circumscribed Inside Polygon / Priemer vpisanej kruznice vnutorneho polygonu 
        private float m_ft;       // Thickness/ Hrubka
        private short m_iNoPoints; // Number of Cross-section Points for Drawing in One Circle
        public  float[,] m_CrScPointOut; // Array of Outside Points and values in 2D
        public float[,] m_CrScPointIn; // Array of Inside Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fa_out
        {
            get { return m_fa_out; }
            set { m_fa_out = value; }
        }

        public float Fa_in
        {
            get { return m_fa_in; }
            set { m_fa_in = value; }
        }

        public float Fd1_out
        {
            get { return m_fd1_out; }
            set { m_fd1_out = value; }
        }

        public float Fd1_in
        {
            get { return m_fd1_in; }
            set { m_fd1_in = value; }
        }
        
        public float Fd2_out
        {
            get { return m_fd2_out; }
            set { m_fd2_out = value; }
        }

        public float Fd2_in
        {
            get { return m_fd2_in; }
            set { m_fd2_in = value; }
        }

        public float Ft
        {
            get { return m_ft; }
            set { m_ft = value; }
        }

        public short INoPoints
        {
            get { return m_iNoPoints; }
            set { m_iNoPoints = value; }
        }

        float m_fr1_out;
        float m_fr1_in;
        float m_fr2_out;
        float m_fr2_in;

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_28()  {   }
        public CCrSc_0_28(float fa, float ft)
        {
            m_iNoPoints = 8; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fa_out = fa;
            m_ft = ft;

            // Calculate Diameter of Circumscribed Circle of Outside Polygon
            m_fd1_out = 2 * Geom2D.GetRadiusfromSideLength(m_fa_out, m_iNoPoints);
            // Calculate Diameter of Inscribed Circle of Outside Polygon
            m_fd2_out = 2 * Geom2D.GetIntRadiusfromSideLength(m_fa_out, m_iNoPoints);

            m_fr1_out = m_fd1_out / 2f;
            m_fr2_out = m_fd2_out / 2f;
            
            // Calculate Side Length of Inside Polygon
            m_fa_in = Geom2D.GetRegularPolygonInternalSideLength(m_fa_out, m_fd2_out / 2, m_fd2_out / 2 - m_ft);
            // Calculate Diameter of Circumscribed Circle of Inside Polygon
            m_fd1_in = 2 * Geom2D.GetRadiusfromSideLength(m_fa_in, m_iNoPoints);
            // Calculate Diameter of Inscribed Circle of Inside Polygon
            m_fd2_in = m_fd2_out - 2 *  m_ft;

            m_fr1_in = m_fd1_in / 2f;
            m_fr2_in = m_fd2_in / 2f;

            if (m_fr1_out == m_fr1_in)
                return;

            // Create Array - allocate memory
            m_CrScPointOut = new float[m_iNoPoints, 2];
            m_CrScPointIn  = new float[m_iNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates
            m_CrScPointOut = Geom2D.GetOctagonPointCoord(m_fa_out);

            // Inside Points
            m_CrScPointIn = Geom2D.GetOctagonPointCoord(m_fa_in);
        }
    }
}




