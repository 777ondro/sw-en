using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    // Test cross-section class
    // Temporary Class - includes array of drawing points of cross-section in its coordinate system (LCS-for 2D yz)
    public class CCrSc_0_22 : CCrSc
    {
        // Tube / Rura

        //----------------------------------------------------------------------------
        private float m_fd;   // Diameter/ Priemer
        private float m_ft;   // Thickness/ Hrubka
        private int m_iNoPoints; // Number of Cross-section Points for Drawing in One Circle
        public  float[,] m_CrScPointOut; // Array of Outside Points and values in 2D
        public float[,] m_CrScPointIn; // Array of Inside Points and values in 2D
        //----------------------------------------------------------------------------

        public float Fd
        {
            get { return m_fd; }
            set { m_fd = value; }
        }

        public float Ft
        {
            get { return m_ft; }
            set { m_ft = value; }
        }

        public int INoPoints
        {
            get { return m_iNoPoints; }
            set { m_iNoPoints = value; }
        }

        float m_fr_out;
        float m_fr_in;

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CCrSc_0_22()  {   }
        public CCrSc_0_22(float fd, float ft, int iNoPoints)
        {
            m_iNoPoints = iNoPoints; // vykreslujeme ako n-uholnik, pocet bodov n
            m_fd = fd;
            m_ft = ft;

            float fd_in = m_fd - 2*m_ft;
           
            // Radii
            m_fr_out = m_fd/2f;
            m_fr_in = fd_in / 2f;

            if (iNoPoints < 2 || m_fr_in == m_fr_out)
                return;


            // Create Array - allocate memory
            m_CrScPointOut = new float [m_iNoPoints,2];
            m_CrScPointIn =  new float[m_iNoPoints, 2];

            // Fill Array Data
            CalcCrSc_Coord();
        }

        //----------------------------------------------------------------------------
        void CalcCrSc_Coord()
        {
            // Fill Point Array Data in LCS (Local Coordinate System of Cross-Section, horizontal y, vertical - z)

            // Outside Points Coordinates
            for (int i = 0; i < INoPoints; i++)
            {
                m_CrScPointOut[i, 0] = GetPositionX(m_fr_out, i * 360 / (INoPoints));  // y
                m_CrScPointOut[i, 1] = GetPositionY(m_fr_out, i * 360 / (INoPoints));  // z
            }

            // Inside Points
            for (int i = 0; i < INoPoints; i++)
            {
                m_CrScPointIn[i, 0] = GetPositionX(m_fr_in, i * 360 / (INoPoints));   // y
                m_CrScPointIn[i, 1] = GetPositionY(m_fr_in, i * 360 / (INoPoints));   // z
            }
        }

        // Transformation of coordinates
        private float GetPositionX(float radius,float theta)
        {
            return radius * (float)Math.Cos(theta * Math.PI / 180);
        }

        private float GetPositionY(float radius, float theta)
        {
            // Clock-wise (for counterclock-wise change sign for vertical coordinate)
            return -radius * (float)Math.Sin(theta * Math.PI / 180);
        }


/*
public Cylinder()
{
//AddCylinder();
// Create a cylinder:
CreateCylinder(new Point3D(0, 0, 0),
0, 1.2, 2, 20, Colors.LightBlue, true);
// Create another cylinder:
CreateCylinder(new Point3D(0, 0, -4),
0.8, 1.2, 0.5, 20, Colors.LightCoral, true);
// Create another cylinder:
CreateCylinder(new Point3D(-3, 0, 0),
1, 1.2, 0.5, 40, Colors.Red, false);
}



        private Point3D GetPosition(double radius,
double theta, double y)
{
Point3D pt = new Point3D();
double sn = Math.Sin(theta * Math.PI / 180);
double cn = Math.Cos(theta * Math.PI / 180);
pt.X = radius * cn;
pt.Y = y;
pt.Z = -radius * sn;
return pt;
}
private void CreateCylinder(Point3D center,
double rin, double rout, double height, int n,
Color color, bool isWireframe)
{
if (n < 2 || rin == rout)
return;
double radius = rin;
if (rin > rout)
{
rin = rout;
rout = radius;
}
double h = height / 2;
Model3DGroup cylinder = new Model3DGroup();
Point3D[,] pts = new Point3D[n, 4];
for (int i = 0; i < n; i++)
{
pts[i, 0] = GetPosition(rout, i * 360 / (n - 1), h);
pts[i, 1] = GetPosition(rout, i * 360 / (n - 1), -h);
pts[i, 2] = GetPosition(rin, i * 360 / (n - 1), -h);
pts[i, 3] = GetPosition(rin, i * 360 / (n - 1), h);
}
for (int i = 0; i < n; i++)
{
for (int j = 0; j < 4; j++)
pts[i, j] += (Vector3D)center;
}
Point3D[] p = new Point3D[8];
for (int i = 0; i < n - 1; i++)
{
p[0] = pts[i, 0];
p[1] = pts[i, 1];
p[2] = pts[i, 2];
p[3] = pts[i, 3];
p[4] = pts[i + 1, 0];
p[5] = pts[i + 1, 1];
p[6] = pts[i + 1, 2];
p[7] = pts[i + 1, 3];
// Top surface:
Utility.CreateTriangleFace(p[0], p[4], p[3],
color, isWireframe, myViewport);
Utility.CreateTriangleFace(p[4], p[7], p[3],
color, isWireframe, myViewport);
// Bottom surface:
Utility.CreateTriangleFace(p[1], p[5], p[2],
color, isWireframe, myViewport);
Utility.CreateTriangleFace(p[5], p[6], p[2],
color, isWireframe, myViewport);
// Outer surface:
Utility.CreateTriangleFace(p[0], p[1], p[4],
color, isWireframe, myViewport);
Utility.CreateTriangleFace(p[1], p[5], p[4],
color, isWireframe, myViewport);
// Outer surface:
Utility.CreateTriangleFace(p[2], p[7], p[6],
color, isWireframe, myViewport);
    Utility.CreateTriangleFace(p[2], p[3], p[7],
color, isWireframe, myViewport);
}
 */
}
}




