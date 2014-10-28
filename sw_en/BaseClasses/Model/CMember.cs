﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using MATH;
using MATERIAL;
using CRSC;


namespace BaseClasses
{
    [Serializable]
    public class CMember : CEntity3D
    {
        private CNode    m_NodeStart;

        public CNode NodeStart
        {
          get { return m_NodeStart; }
          set { m_NodeStart = value; }
        }
        private CNode    m_NodeEnd;

        public CNode NodeEnd
        {
          get { return m_NodeEnd; }
          set { m_NodeEnd = value; }
        }

        private CNRelease m_cnRelease1;

        public CNRelease CnRelease1
        {
          get { return m_cnRelease1; }
          set { m_cnRelease1 = value; }
        }
        private CNRelease m_cnRelease2;

        public CNRelease CnRelease2
        {
          get { return m_cnRelease2; }
          set { m_cnRelease2 = value; }
        }

        private CCrSc m_CrScStart;

        public CCrSc CrScStart
        {
            get { return m_CrScStart; }
            set { m_CrScStart = value; }
        }

        private CCrSc m_CrScEnd;

        public CCrSc CrScEnd
        {
            get { return m_CrScEnd; }
            set { m_CrScEnd = value; }
        }

        private float m_fLength;

        public float FLength
        {
          get { return m_fLength; }
          set { m_fLength = value; }
        }

        public double m_dTheta_x;

        public double DTheta_x
        {
            get { return m_dTheta_x; }
            set { m_dTheta_x = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------

        // Constructor 1
        public CMember()
        {
            m_NodeStart = new CNode();
            m_NodeEnd = new CNode();
            m_cnRelease1 = null;
            m_cnRelease2 = null;
        }
        // Constructor 2
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            int fTime)
        {
            ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            FTime = fTime;

            Fill_Basic();
        }

        // Constructor 3
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            bool haveRelease1,
            bool haveRelease2,
            int fTime)
        {
            ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            if (haveRelease1)
                m_cnRelease1 = new CNRelease(iNode1);
            if (haveRelease2)
                m_cnRelease2 = new CNRelease(iNode2);
            FTime = fTime;

            Fill_Basic();
        }
        // Constructor 4
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            CCrSc objCrSc1,
            int fTime
            )
        {
            ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_CrScStart = objCrSc1;
            FTime = fTime;

            Fill_Basic();
        }

        // Constructor 5
        public CMember(
            int iLine_ID,
            CNode iNode1,
            CNode iNode2,
            CCrSc objCrSc1,
            CCrSc objCrSc2,
            int fTime
            )
        {
            ID = iLine_ID;
            m_NodeStart = iNode1;
            m_NodeEnd = iNode2;
            m_cnRelease1 = null;
            m_cnRelease2 = null;
            m_CrScStart = objCrSc1;
            m_CrScEnd = objCrSc2;
            FTime = fTime;

            Fill_Basic();
        }


        //Fill basic data
        public void Fill_Basic()
        {
            // Temporary !!!!!!!!!!!!!!!!!!!!!! Member Length for 3F 
            FLength = (float)Math.Sqrt((float)Math.Pow(m_NodeEnd.X - m_NodeStart.X, 2f) + (float)Math.Pow(m_NodeEnd.Y - m_NodeStart.Y, 2f) + (float)Math.Pow(m_NodeEnd.Z - m_NodeStart.Z, 2f));

        }

        public Model3DGroup getM_3D_G_Member(EGCS eGCS, SolidColorBrush brush)
        {
            // We need to transform CNode to Point3D
            Point3D mpA = new Point3D(NodeStart.X, NodeStart.Y, NodeStart.Z); // Start point - class Point3D
            Point3D mpB = new Point3D(NodeEnd.X, NodeEnd.Y, NodeEnd.Z); // End point - class Point3D
            // Angle of rotation about local x-axis
            DTheta_x = 0; // Temporary

            Model3DGroup modelGroup = new Model3DGroup(); // Whole member
            GeometryModel3D model = new GeometryModel3D(); // Member parts (forehead, backhead, shell, surface
            MeshGeometry3D mesh = getMeshMemberGeometry3DFromCrSc(eGCS, CrScStart, CrScEnd, mpA, mpB, DTheta_x); // Mesh one member

            model.Geometry = mesh;

            model.Material = new DiffuseMaterial(brush);  // Set MemberModel Material

            modelGroup.Children.Add((Model3D)model);

            return modelGroup;
        }

        private MeshGeometry3D getMeshMemberGeometry3DFromCrSc(EGCS eGCS, CCrSc obj_CrScA, CCrSc obj_CrScB, Point3D mpA, Point3D mpB, double dTheta_x)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = new Point3DCollection();

            // Main Nodes of Member
            Point3D m_pA = mpA;
            Point3D m_pB = mpB;

            // Angle of rotation about local x-axis
            double m_dTheta_x = dTheta_x;

            // Priemet do osi GCS - rozdiel suradnic v GCS
            double m_dDelta_X = m_pB.X - m_pA.X;
            double m_dDelta_Y = m_pB.Y - m_pA.Y;
            double m_dDelta_Z = m_pB.Z - m_pA.Z;

            // Realna dlzka prvku // Length of member - straigth segment of member
            // Prečo je záporná ???
            // double m_dLength = -Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));
            double m_dLength = Math.Sqrt(Math.Pow(m_dDelta_X, 2) + Math.Pow(m_dDelta_Y, 2) + Math.Pow(m_dDelta_Z, 2));

            // Number of Points per section
            short iNoCrScPoints2D;
            // Points 2D Coordinate Array
            if (obj_CrScA.IsShapeSolid) // Solid I,U,Z,HL,L, ..............
            {
                iNoCrScPoints2D = obj_CrScA.ITotNoPoints; // Depends on Section Type
                // Fill Mesh Positions for Start and End Section of Element - Defines Edge Points of Element

                if (obj_CrScA.CrScPointsOut != null) // Check that data are available
                {
                    for (int j = 0; j < iNoCrScPoints2D; j++)
                    {
                        // X - start, Y, Z
                        mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
                    }
                    for (int j = 0; j < iNoCrScPoints2D; j++)
                    {
                        // X - end, Y, Z
                        if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
                        else
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
                    }
                }
                else
                {
                    // Exception
                }
            }
            else if (obj_CrScA.INoPointsOut == obj_CrScA.INoPointsIn) // Closed cross-section with same number out ouside and insdide definiton points
            {
                // Tubes , Polygonal Hollow Sections
                iNoCrScPoints2D = (short)(2 * obj_CrScA.INoPointsOut); // Twice number of one surface

                // Tube, regular hollow sections
                // TU

                // Start
                if (obj_CrScA.CrScPointsOut != null) // Check that data are available
                {
                    // OutSide Radius Points
                    for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
                    {
                        // X - start, Y, Z
                        mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1]));
                    }
                }
                else
                {
                    // Exception
                }

                if (obj_CrScA.CrScPointsIn != null) // Check that data are available
                {
                    // Inside Radius Points
                    for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
                    {
                        // X - start, Y, Z
                        mesh.Positions.Add(new Point3D(0, obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1]));
                    }
                }
                else
                {
                    // Exception
                }

                // End
                if (obj_CrScA.CrScPointsOut != null) // Check that data are available
                {
                    // OutSide Radius Points
                    for (int j = 0; j < obj_CrScA.INoPointsOut; j++)
                    {
                        // X - end, Y, Z
                        if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsOut[j, 0], obj_CrScA.CrScPointsOut[j, 1])); // Constant size member
                        else
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsOut[j, 0], obj_CrScB.CrScPointsOut[j, 1])); // Tapered member
                    }
                }
                else
                {
                    // Exception
                }

                if (obj_CrScA.CrScPointsIn != null) // Check that data are available
                {
                    // Inside Radius Points
                    for (int j = 0; j < obj_CrScA.INoPointsIn; j++)
                    {
                        // X - end, Y, Z
                        if (obj_CrScB == null /*|| zistit ci su objekty rovnakeho typu - triedy */)  // Check that data of second cross-section are available
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScA.CrScPointsIn[j, 0], obj_CrScA.CrScPointsIn[j, 1])); // Constant size member
                        else
                            mesh.Positions.Add(new Point3D(m_dLength, obj_CrScB.CrScPointsIn[j, 0], obj_CrScB.CrScPointsIn[j, 1])); // Tapered member
                    }
                }
                else
                {
                    // Exception
                }
            }
            else
            {
                // Exception
                // Closed cross-section with different number out ouside and insdide definiton points

                iNoCrScPoints2D = 0; // Temp
            }

            // Dislay data in the output window

            string sOutput = "Before transformation \n\n"; // create temporary string

            for (int i = 0; i < 2 * iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
            {
                Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

                sOutput += "Node ID: " + i.ToString();
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.X.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.Y.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.Z.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns

                sOutput += "\n"; // New row
            }

            if (BIsDebugging)
                System.Console.Write(sOutput); // Write in console window

            // Transform coordinates
            TransformMember_LCStoGCS(eGCS, m_pA, m_pB, m_dDelta_X, m_dDelta_Y, m_dDelta_Z, m_dTheta_x, mesh.Positions);

            // Mesh Triangles - various cross-sections shapes defined
            mesh.TriangleIndices = obj_CrScA.TriangleIndices;


            // Dislay data in the output window

            sOutput = null;
            sOutput = "After transformation \n\n"; // create temporary string

            for (int i = 0; i < 2 * iNoCrScPoints2D; i++) // for all mesh positions (start and end of member, number of edge points of whole member = 2 * number in one section)
            {
                Point3D p3D = mesh.Positions[i]; // Get mesh element/item (returns Point3D)

                sOutput += "Node ID: " + i.ToString();
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.X.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.Y.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns
                sOutput += p3D.Z.ToString("0.0000");
                sOutput += "\t"; // New Tab between columns

                sOutput += "\n"; // New row
            }

            if (BIsDebugging)
                System.Console.Write(sOutput); // Write in console window

            // Change mesh triangle indices
            // Change orientation of normals

            //if (eGCS == EGCS.eGCSLeftHanded)
            //{
            int iSecond = 1;
            int iThird = 2;

            int iTIcount = mesh.TriangleIndices.Count;
            for (int i = 0; i < iTIcount / 3; i++)
            {
                int iTI_2 = mesh.TriangleIndices[iSecond];
                int iTI_3 = mesh.TriangleIndices[iThird];

                mesh.TriangleIndices[iThird] = iTI_2;
                mesh.TriangleIndices[iSecond] = iTI_3;

                iSecond += 3;
                iThird += 3;
            }
            //}


            return mesh;
        }

        public Point3DCollection TransformMember_LCStoGCS(EGCS eGCS, Point3D pA, Point3D pB, double dDeltaX, double dDeltaY, double dDeltaZ, double dTheta_x, Point3DCollection pointsCollection)
        {
            // Returns transformed coordinates of member nodes

            // Angles
            double dAlphaX = 0, dBetaY = 0, dGammaZ = 0;

            // Priemet do rovin GCS - dlzka priemetu do roviny
            double dLength_XY = 0,
                   dLength_YZ = 0,
                   dLength_XZ = 0;

            if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaY, 0.0))
                dLength_XY = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaY, 2));

            if (!MathF.d_equal(dDeltaY, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
                dLength_YZ = Math.Sqrt(Math.Pow(dDeltaY, 2) + Math.Pow(dDeltaZ, 2));

            if (!MathF.d_equal(dDeltaX, 0.0) || !MathF.d_equal(dDeltaZ, 0.0))
                dLength_XZ = Math.Sqrt(Math.Pow(dDeltaX, 2) + Math.Pow(dDeltaZ, 2));

            if (BIsDebugging)
            {
                // Temporary console output
                System.Console.Write("\n" + "Lengths - projection of element into global coordinate system:\n");
                System.Console.Write("Length - global X-axis:\t" + dDeltaX.ToString("0.000") + "\n"); // Write length in X-axis
                System.Console.Write("Length - global Y-axis:\t" + dDeltaY.ToString("0.000") + "\n"); // Write length in Y-axis
                System.Console.Write("Length - global Z-axis:\t" + dDeltaZ.ToString("0.000") + "\n\n"); // Write length in Z-axis
            }
            // Uhly pootocenia LCS okolo osi GCS
            // Angles
            dAlphaX = Geom2D.GetAlpha2D_CW(dDeltaY, dDeltaZ);
            dBetaY = Geom2D.GetAlpha2D_CW_2(dDeltaX, dDeltaZ); // !!! Pre pootocenie okolo Y su pouzite ine kvadranty !!!
            dGammaZ = Geom2D.GetAlpha2D_CW(dDeltaX, dDeltaY);

            // Auxialiary angles for members graphics
            double dBetaY_aux = Geom2D.GetAlpha2D_CW_3(dDeltaX, dDeltaZ, Math.Sqrt(Math.Pow(dLength_XY, 2) + Math.Pow(dDeltaZ, 2)));
            double dGammaZ_aux = dGammaZ;
            if (Math.PI / 2 < dBetaY && dBetaY < 1.5 * Math.PI)
            {
                if (dGammaZ < Math.PI)
                    dGammaZ_aux = dGammaZ + Math.PI;
                else
                    dGammaZ_aux = dGammaZ - Math.PI;
            }

            if (BIsDebugging)
            {
                // Temporary console output
                System.Console.Write("\n" + "Rotation angles:\n");
                System.Console.Write("Rotation about global X-axis:\t" + dAlphaX.ToString("0.000") + "rad\t " + (dAlphaX * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about X-axis
                System.Console.Write("Rotation about global Y-axis:\t" + dBetaY.ToString("0.000") + "rad\t " + (dBetaY * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Y-axis
                System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ.ToString("0.000") + "rad\t " + (dGammaZ * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write rotation about Z-axis
                System.Console.Write("\n" + "Auxiliary rotation angles - graphics:\n");
                System.Console.Write("Rotation about global Y-axis:\t" + dBetaY_aux.ToString("0.000") + "rad\t " + (dBetaY_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n"); // Write auxiliary rotation about Y-axis
                System.Console.Write("Rotation about global Z-axis:\t" + dGammaZ_aux.ToString("0.000") + "rad\t " + (dGammaZ_aux * 180.0f / MathF.fPI).ToString("0.0") + "deg \n\n"); // Write auxiliary rotation about Z-axis
            }

            for (int i = 0; i < pointsCollection.Count; i++)
            {
                pointsCollection[i] = RotatePoint(eGCS, pA, pointsCollection[i], dBetaY_aux, dGammaZ_aux, dTheta_x, dDeltaX, dDeltaY, dDeltaZ);
            }

            return pointsCollection;
        }

        protected Point3D RotatePoint(EGCS eGCS, Point3D pA, Point3D p, double betaY, double gamaZ, double theta_x, double dDeltaX, double dDeltaY, double dDeltaZ)
        {
            Point3D p3Drotated = new Point3D();

            //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu

            // Left Handed
            // * Where (alphaX) represents the rotation about the X axis, (betaY) represents the rotation about the Y axis, and (gamaZ) represents the rotation about the Z axis
            /*
            X Rotation *

            1     0                0                  0
            0     cos (alphaX)    -sin (alphaX)       0
            0     sin (alphaX)     cos (alphaX)       0
            0     0                0                  1
            */

            /*
            Y Rotation *

            cos (betaY)    0     sin (betaY)    0
            0              1     0              0
            -sin (betaY)   0     cos (betaY)    0
            0              0     0              1
            */

            /*
            Z Rotation *

            cos (gamaZ)     -sin (gamaZ)     0      0
            sin (gamaZ)     cos (gamaZ)      0      0
            0                 0              1      0
            0                 0              0      1
            */

            ////////////////////////////////////////////////////////////////////////////////////////////
            // Right Handed

            /*
            X Rotation *

            1           0             0               0
            0     cos (alphaX)     sin (alphaX)       0
            0     -sin (alphaX)    cos (alphaX)       0
            0           0             0               1
            */

            /*
            Y Rotation *

            cos (betaY)    0     -sin (betaY)   0
            0              1     0              0
            sin (betaY)    0     cos (betaY)    0
            0              0     0              1
            */

            /*
            Z Rotation *

            cos (gamaZ)     sin (gamaZ)      0      0
            -sin (gamaZ)    cos (gamaZ)      0      0
            0                 0              1      0
            0                 0              0      1
            */

            Point3D pTemp1 = new Point3D();
            Point3D pTemp2 = new Point3D();

            if (eGCS == EGCS.eGCSLeftHanded)
            {
                // Left-handed

                // Rotate about Y-axis
                pTemp1.X = (Math.Cos(betaY) * p.X) + (Math.Sin(betaY) * p.Z);
                pTemp1.Y = p.Y;
                pTemp1.Z = (-Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

                // Rotate about Z-axis
                pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) - (Math.Sin(gamaZ) * pTemp1.Y);
                pTemp2.Y = (Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
                pTemp2.Z = pTemp1.Z;

                // Translate
                pTemp1.X = pA.X + pTemp2.X;
                pTemp1.Y = pA.Y + pTemp2.Y;
                pTemp1.Z = pA.Z + pTemp2.Z;

                // Set output point data
                p3Drotated.X = pTemp1.X;
                p3Drotated.Y = pTemp1.Y;
                p3Drotated.Z = pTemp1.Z;

                // Rotate about local x-axis
                if (!MathF.d_equal(theta_x, 0.0))
                {
                    double c = Math.Cos(theta_x);
                    double s = Math.Sin(theta_x);
                    double t = 1 - c;

                    p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.Z;
                    p3Drotated.Y = (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Z;
                    p3Drotated.Z = (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
                }
            }
            else
            {
                // Right-handed

                // Rotate about Y-axis
                pTemp1.X = (Math.Cos(betaY) * p.X) - (Math.Sin(betaY) * p.Z);
                pTemp1.Y = p.Y;
                pTemp1.Z = (Math.Sin(betaY) * p.X) + (Math.Cos(betaY) * p.Z);

                // Rotate about Z-axis
                pTemp2.X = (Math.Cos(gamaZ) * pTemp1.X) + (Math.Sin(gamaZ) * pTemp1.Y);
                pTemp2.Y = (-Math.Sin(gamaZ) * pTemp1.X) + (Math.Cos(gamaZ) * pTemp1.Y);
                pTemp2.Z = pTemp1.Z;

                // Translate
                pTemp1.X = pA.X + pTemp2.X;
                pTemp1.Y = pA.Y + pTemp2.Y;
                pTemp1.Z = pA.Z + pTemp2.Z;

                // Set output point data
                p3Drotated.X = pTemp1.X;
                p3Drotated.Y = pTemp1.Y;
                p3Drotated.Z = pTemp1.Z;

                // Rotate about local x-axis
                if (!MathF.d_equal(theta_x, 0.0))
                {
                    double c = Math.Cos(theta_x);
                    double s = Math.Sin(theta_x);
                    double t = 1 - c;

                    p3Drotated.X = (t * Math.Pow(dDeltaX, 2) + c) * pTemp1.X + (t * dDeltaX * dDeltaY + s * dDeltaZ) * pTemp1.Y + (t * dDeltaX * dDeltaZ - s * dDeltaY) * pTemp1.Z;
                    p3Drotated.Y = (t * dDeltaX * dDeltaY - s * dDeltaZ) * pTemp1.X + (t * Math.Pow(dDeltaY, 2) + c) * pTemp1.Y + (t * dDeltaY * dDeltaZ + s * dDeltaX) * pTemp1.Z;
                    p3Drotated.Z = (t * dDeltaX * dDeltaZ + s * dDeltaY) * pTemp1.X + (t * dDeltaY * dDeltaZ - s * dDeltaX) * pTemp1.Y + (t * Math.Pow(dDeltaZ, 2) + c) * pTemp1.Z;
                }

            }

            return p3Drotated;
        }
        protected Point3D RotatePoint_POKUSY(EGCS eGCS, Point3D pA, Point3D p, double alphaX, double betaY, double gamaZ, double dDeltaX, double dDeltaY, double dDeltaZ)
        {
            Point3D p3Drotated = new Point3D();

            /* Commented 25.5.2013
            // Left Handed

            p3Drotated.X = pA.X + p.X;
            p3Drotated.Y = pA.Y + (p.Y * Math.Cos(alphaX) - p.Z * Math.Sin(alphaX));
            p3Drotated.Z = pA.Z + (p.Y * Math.Sin(alphaX) + p.Z * Math.Cos(alphaX));

            p3Drotated.X = pA.X + (p.X * Math.Cos(betaY) + p.Z * Math.Sin(betaY));
            p3Drotated.Y = pA.Y + p.Y;
            p3Drotated.Z = pA.Z + (-p.X * Math.Sin(betaY) + p.Z * Math.Cos(betaY));

            p3Drotated.X = pA.X + (p.X * Math.Cos(gamaZ) - p.Y * Math.Sin(gamaZ));
            p3Drotated.Y = pA.Y + (p.X * Math.Sin(gamaZ) + p.Y * Math.Cos(gamaZ));
            p3Drotated.Z = pA.Z + p.Z;
            */

            // Left Handed
            //http://sk.wikipedia.org/wiki/Trojrozmern%C3%A1_projekcia#D.C3.A1ta_nevyhnutn.C3.A9_pre_projekciu
            // * Where (alphaX) represents the rotation about the X axis, (betaY) represents the rotation about the Y axis, and (gamaZ) represents the rotation about the Z axis
            /*
            X Rotation *

            1     0                0                  0
            0     cos (alphaX)    -sin (alphaX)       0
            0     sin (alphaX)     cos (alphaX)       0
            0     0                0                  1
            */

            /*
            Y Rotation *

            cos (betaY)    0     sin (betaY)    0
            0              1     0              0
            -sin (betaY)   0     cos (betaY)    0
            0              0     0              1
            */

            /*
            Z Rotation *

            cos (gamaZ)     -sin (gamaZ)     0      0
            sin (gamaZ)     cos (gamaZ)      0      0
            0                 0              1      0
            0                 0              0      1
            */

            ////////////////////////////////////////////////////////////////////////////////////////////
            // Right Handed

            /*
            X Rotation *

            1           0             0               0
            0     cos (alphaX)     sin (alphaX)       0
            0     -sin (alphaX)    cos (alphaX)       0
            0           0             0               1
            */

            /*
            Y Rotation *

            cos (betaY)    0     -sin (betaY)   0
            0              1     0              0
            sin (betaY)    0     cos (betaY)    0
            0              0     0              1
            */

            /*
            Z Rotation *

            cos (gamaZ)     sin (gamaZ)      0      0
            -sin (gamaZ)    cos (gamaZ)      0      0
            0                 0              1      0
            0                 0              0      1
            */

            /*
            Point3D pTemp1 = new Point3D();
            Point3D pTemp2 = new Point3D();

            if (eGCS == EGCS.eGCSLeftHanded)
            {
                // Left handed

                pTemp1.X = p.X;
                pTemp1.Y = (Math.Cos(alphaX) * p.Y) - (Math.Sin(alphaX) * p.Z);
                pTemp1.Z = (Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

                pTemp2.X = (Math.Cos(betaY) * pTemp1.X) + (Math.Sin(betaY) * pTemp1.Z);
                pTemp2.Y = pTemp1.Y;
                pTemp2.Z = (-Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

                p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) - (Math.Sin(gamaZ) * pTemp2.Y));
                p3Drotated.Y = pA.Y + ((Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
                p3Drotated.Z = pA.Z + pTemp2.Z;
            }
            else
            {
                // Right handed

                pTemp1.X = p.X;
                pTemp1.Y = (Math.Cos(alphaX) * p.Y) + (Math.Sin(alphaX) * p.Z);
                pTemp1.Z = (-Math.Sin(alphaX) * p.Y) + (Math.Cos(alphaX) * p.Z);

                pTemp2.X = (Math.Cos(betaY) * pTemp1.X) - (Math.Sin(betaY) * pTemp1.Z);
                pTemp2.Y = pTemp1.Y;
                pTemp2.Z = (Math.Sin(betaY) * pTemp1.X) + (Math.Cos(betaY) * pTemp1.Z);

                p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * pTemp2.X) + (Math.Sin(gamaZ) * pTemp2.Y));
                p3Drotated.Y = pA.Y + ((-Math.Sin(gamaZ) * pTemp2.X) + (Math.Cos(gamaZ) * pTemp2.Y));
                p3Drotated.Z = pA.Z + pTemp2.Z;
            }
            */

            // In case that member is parallel to global axis should be rotated only once
            if (dDeltaX < 0 && MathF.d_equal(dDeltaY, 0.0) && MathF.d_equal(dDeltaZ, 0.0))      // Parallel to X-axis with negative orientation
                betaY = 0; // Do not rotate about Y-axis
            else if (MathF.d_equal(dDeltaX, 0.0) && dDeltaY < 0 && MathF.d_equal(dDeltaZ, 0.0)) // Parallel to Y-axis with negative orientation
                alphaX = 0; // Do not rotate about X-axis
            else if (MathF.d_equal(dDeltaX, 0.0) && MathF.d_equal(dDeltaY, 0.0) && dDeltaZ < 0) // Parallel to Z-axis with negative orientation
                betaY = 0; // Do not rotate about Y-axis
            else
            {
                // No action - General position of member in space
            }

            // Cumulative 3D rotation and translation
            // Temp - pokus 1
            // Rotate around x, y, z
            double ax = Math.Cos(betaY) * Math.Cos(gamaZ);
            double ay = (Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ));
            double az = (Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ));

            p3Drotated.X = ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);

            double bx = Math.Cos(betaY) * Math.Sin(gamaZ);
            double by = (Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));
            double bz = (-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ));

            p3Drotated.Y = ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);

            double cx = -Math.Sin(betaY);
            double cy = Math.Cos(betaY) * Math.Sin(alphaX);
            double cz = Math.Cos(alphaX) * Math.Cos(betaY);

            p3Drotated.Z = ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);


            // Temp - pokus 2

            if (gamaZ < Math.PI)
                gamaZ += Math.PI;
            else if (gamaZ > Math.PI)
                gamaZ -= Math.PI;


            // X Rotation
            ax = 1 * p.X + 0 * p.Y + 0 * p.Z;
            ay = 0 * p.X + Math.Cos(alphaX) * p.Y - Math.Sin(alphaX) * p.Z;
            az = Math.Sin(alphaX) * p.X + Math.Cos(alphaX) * p.Y + 0 * p.Z;

            // Y Rotation
            bx = Math.Cos(betaY) * ax + 0 * ay + Math.Sin(betaY) * az;
            by = 0 * ax + 1 * ay + 0 * az;
            bz = -Math.Sin(betaY) * ax + 0 * ay + Math.Cos(betaY) * az;

            // Z Rotation
            cx = Math.Cos(gamaZ) * bx - Math.Sin(gamaZ) * by + 0 * bz;
            cy = Math.Sin(gamaZ) * bx + Math.Cos(gamaZ) * by + 0 * bz;
            cz = 0 * bx + 0 * by + 1 * bz;

            p3Drotated.X = pA.X + cx;
            p3Drotated.Y = pA.Y + cy;
            p3Drotated.Z = pA.Z + cz;

            p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + ((Math.Cos(gamaZ) * Math.Sin(alphaX) * Math.Sin(betaY)) - (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.Y + ((Math.Cos(alphaX) * Math.Cos(gamaZ) * Math.Sin(betaY)) + (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.Z);
            p3Drotated.Y = pA.Y + ((Math.Cos(betaY) * Math.Sin(gamaZ)) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) + (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + ((-Math.Cos(gamaZ) * Math.Sin(alphaX)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Z);
            p3Drotated.Z = pA.Z + ((-Math.Sin(betaY)) * p.X + (Math.Cos(betaY) * Math.Sin(alphaX)) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);

            // Rotate around z, y, x
            /*
            p3Drotated.X = pA.X + ((Math.Cos(betaY) * Math.Cos(gamaZ)) * p.X + (-Math.Cos(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Sin(betaY)) * p.Z);
            p3Drotated.Y = pA.Y + (((Math.Cos(alphaX) * Math.Sin(gamaZ)) + (Math.Sin(alphaX)*Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Cos(alphaX) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(betaY) * Math.Sin(gamaZ))) * p.Y + (-Math.Sin(alphaX) * Math.Cos(betaY)) * p.Z);
            p3Drotated.Z = pA.Z + (((Math.Sin(alphaX) * Math.Sin(gamaZ)) - (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Cos(gamaZ))) * p.X + ((Math.Sin(alphaX) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(betaY) * Math.Sin (gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Cos(betaY)) * p.Z);
            */

            // Right-handed cumulative ???
            // http://scipp.ucsc.edu/~haber/ph216/rotation_12.pdf
            /*
            p3Drotated.X = pA.X + (((Math.Cos(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) - (Math.Sin(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Cos(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) - (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(alphaX) * Math.Sin(betaY)) * p.Z);
            p3Drotated.Y = pA.Y + (((Math.Sin(alphaX) * Math.Cos(betaY) * Math.Cos(gamaZ)) + (Math.Cos(alphaX) * Math.Sin(gamaZ))) * p.X + (-Math.Sin(alphaX) * Math.Cos(betaY) * Math.Sin(gamaZ) + (Math.Cos(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Sin(alphaX) * Math.Sin(betaY)) * p.Z);
            p3Drotated.Z = pA.Z + ((-Math.Sin(betaY) * Math.Cos(gamaZ)) * p.X + (Math.Sin(betaY) * Math.Sin(gamaZ)) * p.Y + (Math.Cos(betaY)) * p.Z);
            */

            //
            // http://inside.mines.edu/~gmurray/ArbitraryAxisRotation/
            /*
            p3Drotated.X = pA.X + ((Math.Cos(gamaZ) * Math.Cos(betaY)) * p.X + (-Math.Sin(gamaZ) * Math.Cos(betaY)) * p.Y + Math.Sin(betaY) * p.Z);
            p3Drotated.Y = pA.Y + (((Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX)) + (Math.Sin(gamaZ) * Math.Cos(alphaX))) * p.X + ((Math.Cos(gamaZ) * Math.Cos(alphaX)) - (Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Sin(alphaX))) * p.Y + (-Math.Cos(betaY) * Math.Sin(alphaX)) * p.Z);
            p3Drotated.Z = pA.Z + (((Math.Sin(gamaZ) * Math.Sin(alphaX)) - (Math.Cos(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX))) * p.X + ((Math.Sin(gamaZ) * Math.Sin(betaY) * Math.Cos(alphaX)) + (Math.Sin(alphaX) * Math.Cos(gamaZ))) * p.Y + (Math.Cos(betaY) * Math.Cos(alphaX)) * p.Z);
            */


            return p3Drotated;


            // Mozno by som mal zapracovat toto
            //http://mathworld.wolfram.com/EulerAngles.html
        }

    } // End of Class CMember


    public class CCompare_MemberID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CMember)x).ID - ((CMember)y).ID;
        }
    }
}
