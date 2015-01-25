﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MATH;

namespace BaseClasses.GraphObj.Objects_3D
{
    // Cylinder in local x-axis
    class Cylinder
    {
        public float m_fDim1_r;
        public float m_fDim2_L;
        DiffuseMaterial m_mat;

        public Cylinder()
        { }
        public Cylinder(float fDim1_r, float fDim2_L, DiffuseMaterial mat)
        {
            m_fDim1_r = fDim1_r;
            m_fDim2_L = fDim2_L;
            m_mat = mat;
        }

        //--------------------------------------------------------------------------------------------
        // Generate 3D volume geometry of cylinder
        //--------------------------------------------------------------------------------------------
        public GeometryModel3D CreateM_G_M_3D_Volume_Cylinder(Point3D solidControlEdge, float fDim1_r, float fDim2_L, DiffuseMaterial mat)
        {
            MeshGeometry3D meshGeom3D = new MeshGeometry3D(); // Create geometry mesh

            short iTotNoPoints = 73; // 1 auxialiary node in centroid / stredovy bod

            if (fDim1_r <= 0f)
            {
                // Exception
                //return;
            }

            // Create Array - allocate memory
            float[,] PointsOut = new float[iTotNoPoints, 2];

            // Outside Points Coordinates
            PointsOut = Geom2D.GetCirclePointCoord(fDim1_r, iTotNoPoints);

            // Centroid
            PointsOut[iTotNoPoints - 1, 0] = 0f;
            PointsOut[iTotNoPoints - 1, 1] = 0f;

            meshGeom3D.Positions = new Point3DCollection();

            // Bottom  h = 0
            for (int i = 0; i < iTotNoPoints; i++)
            {
                Point3D p = new Point3D(0,  PointsOut[i, 0],  PointsOut[i, 1]);
                meshGeom3D.Positions.Add(p);
            }

            // Top L = xxx
            for (int i = 0; i < iTotNoPoints; i++)
            {
                Point3D p = new Point3D(fDim2_L, PointsOut[i, 0], PointsOut[i, 1]);
                meshGeom3D.Positions.Add(p);
            }

            Int32Collection TriangleIndices = new Int32Collection();

            // Front Side / Forehead
            for (int i = 0; i < iTotNoPoints - 1; i++)
            {
                if (i < iTotNoPoints - 2)
                {
                    TriangleIndices.Add(i + 1);
                    TriangleIndices.Add(iTotNoPoints - 1);
                    TriangleIndices.Add(i);
                }
                else // Last Element
                {
                    TriangleIndices.Add(0);
                    TriangleIndices.Add(iTotNoPoints - 1);
                    TriangleIndices.Add(i);
                }
            }

            // Back Side
            for (int i = 0; i < iTotNoPoints - 1; i++)
            {
                if (i < iTotNoPoints - 2)
                {
                    TriangleIndices.Add(iTotNoPoints + iTotNoPoints - 1);
                    TriangleIndices.Add(iTotNoPoints + i + 1);
                    TriangleIndices.Add(iTotNoPoints + i);
                }
                else // Last Element
                {
                    TriangleIndices.Add(iTotNoPoints + iTotNoPoints - 1);
                    TriangleIndices.Add(iTotNoPoints);
                    TriangleIndices.Add(iTotNoPoints + i);
                }
            }

            // Shell Surface OutSide
            for (int i = 0; i < iTotNoPoints - 1; i++)
            {
                if (i < iTotNoPoints - 2)
                    AddRectangleIndices_CCW_1234(TriangleIndices, i, iTotNoPoints + i, iTotNoPoints + i + 1, i + 1);
                else
                    AddRectangleIndices_CCW_1234(TriangleIndices, i, iTotNoPoints + i, iTotNoPoints, 0); // Last Element

            }

            meshGeom3D.TriangleIndices = TriangleIndices;

            GeometryModel3D geomModel3D = new GeometryModel3D();

            geomModel3D.Geometry = meshGeom3D; // Set mesh to model

            geomModel3D.Material = mat;

            TranslateTransform3D translate = new TranslateTransform3D(solidControlEdge.X, solidControlEdge.Y, solidControlEdge.Z);

            geomModel3D.Transform = translate;

            return geomModel3D;
        }

        // Draw Rectangle / Add rectangle indices - countrer-clockwise CCW numbering of input points 1,2,3,4 (see scheme)
        // Add in order 1,4,3,2
        protected void AddRectangleIndices_CCW_1234(Int32Collection Indices,
              int point1, int point2,
              int point3, int point4)
        {
            // Main input numbering is clockwise, add indices counter-clockwise

            // 1  _______  2
            //   |_______| 
            // 4           3

            // Triangles Numbering is Clockwise
            // Top Right
            Indices.Add(point1);
            Indices.Add(point2);
            Indices.Add(point3);

            // Bottom Left
            Indices.Add(point1);
            Indices.Add(point3);
            Indices.Add(point4);
        }
    }
}
