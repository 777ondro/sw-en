﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace BaseClasses.GraphObj.Objects_3D
{
    public class StraightLineArrow3D
    {
        public float fTip_X;
        public float fTip_Y;
        public float fTip_Z;

        public float fConeHeight;
        public float fCylinderHeight;
        public float fTotalHeight;

        public float[,] fAnnulusOutPoints;
        public float[,] fAnnulusInPoints;

        public Point3DCollection ArrowPoints;

        const int number_of_segments = 72;

        public StraightLineArrow3D()
        { }

        public StraightLineArrow3D(float tip_X, float tip_Y, float tip_Z, float totalHeight)
        {
            fTip_X = tip_X;
            fTip_Y = tip_Y;
            fTip_Z = tip_Z;
            fTotalHeight = totalHeight;

            fConeHeight = 0.2f * fTotalHeight;
            fCylinderHeight = fTotalHeight - fConeHeight;

            AnnulusPoints(tip_X, tip_Y, totalHeight * 0.01f, totalHeight * 0.05f);
        }

        float[,] GetCircleCoordinates(float x0, float y0, float fr)
        {
            float[,] fCirclePoints = new float[number_of_segments, 2];

            for (int i = 0; i < number_of_segments; i++)
            {
                float theta = 2.0f * (float)Math.PI * i / number_of_segments;
                fCirclePoints[i, 0] = x0 + fr * (float)Math.Cos(theta);
                fCirclePoints[i, 1] = y0 + fr * (float)Math.Sin(theta);
            }

            return fCirclePoints;
        }

        void AnnulusPoints(float x0, float y0, float fr_in, float fr_out)
        {
            fAnnulusOutPoints = new float[number_of_segments, 2];
            fAnnulusInPoints = new float[number_of_segments, 2];

            fAnnulusOutPoints = GetCircleCoordinates(x0, y0, fr_out);
            fAnnulusInPoints = GetCircleCoordinates(x0, y0, fr_in);
        }

        public Point3DCollection GetArrowPoints()
        {
            Point3DCollection cPointsCollection = new Point3DCollection(1 + 3 * number_of_segments + 1);

            cPointsCollection.Add(new Point3D(fTip_X, fTip_Y, fTip_Z)); // Tip

            for (int i = 0; i < number_of_segments; i++)
            {
                cPointsCollection.Add(new Point3D(fAnnulusOutPoints[i, 0], fAnnulusOutPoints[i, 1], fConeHeight));
            }

            for (int i = 0; i < number_of_segments; i++)
            {
                cPointsCollection.Add(new Point3D(fAnnulusInPoints[i, 0], fAnnulusInPoints[i, 1], fConeHeight));
            }

            for (int i = 0; i < number_of_segments; i++)
            {
                cPointsCollection.Add(new Point3D(fAnnulusInPoints[i, 0], fAnnulusInPoints[i, 1], fTotalHeight));
            }

            cPointsCollection.Add(new Point3D(fTip_X, fTip_Y, fTotalHeight)); // Top middle point

            return cPointsCollection;
        }

        public Int32Collection GetArrowIndices()
        {
            Int32Collection cArrowIndices = new Int32Collection();

            for (int i = 0; i < number_of_segments; i++)
            {
                if (i < number_of_segments - 1)
                {
                    cArrowIndices.Add(0);
                    cArrowIndices.Add(i + 2);
                    cArrowIndices.Add(i + 1);
                }
                else // last
                {
                    cArrowIndices.Add(0);
                    cArrowIndices.Add(1);
                    cArrowIndices.Add(i + 1);
                }
            }

            // annulus
            for (int i = 0; i < number_of_segments; i++)
            {
                if (i < number_of_segments - 1)
                {
                    CreateRectangle_CCW(cArrowIndices, i + 1, i + 2, i + 2 + number_of_segments, i + 1 + number_of_segments);
                }
                else // last
                {
                    CreateRectangle_CCW(cArrowIndices, i + 1, 1, 1 + number_of_segments, i + 1 + number_of_segments);
                }
            }

            // Rozna orientacia normal !!!
            // shell
            for (int i = 0; i < number_of_segments; i++)
            {
                if (i < number_of_segments - 1)
                {
                    CreateRectangle_CW(cArrowIndices, i + 1 + number_of_segments, i + 1 + 2 * number_of_segments, i + 2 + 2 * number_of_segments, i + 2 + number_of_segments);
                }
                else // last
                {
                    CreateRectangle_CW(cArrowIndices, 2 * number_of_segments, 3 * number_of_segments, i + 2 + number_of_segments, i + 2);
                }
            }

            // Rozna orientacia normal !!!
            // Top surface
            for (int i = 0; i < number_of_segments; i++)
            {
                if (i < number_of_segments - 1)
                {
                    cArrowIndices.Add(3 * number_of_segments + 1);
                    cArrowIndices.Add(i + 1 + 2 * number_of_segments);
                    cArrowIndices.Add(i + 2 + 2 * number_of_segments);
                }
                else // last
                {
                    cArrowIndices.Add(3 * number_of_segments + 1);
                    cArrowIndices.Add(3 * number_of_segments);
                    cArrowIndices.Add(2 * number_of_segments + 1);
                }
            }

            return cArrowIndices;
        }

        public void CreateRectangle_CCW(Int32Collection ArrowIndices, int i0, int i1, int i2, int i3)
        {
            ArrowIndices.Add(i0);
            ArrowIndices.Add(i1);
            ArrowIndices.Add(i2);

            ArrowIndices.Add(i0);
            ArrowIndices.Add(i2);
            ArrowIndices.Add(i3);
        }

        public void CreateRectangle_CW(Int32Collection ArrowIndices, int i0, int i1, int i2, int i3)
        {
            ArrowIndices.Add(i0);
            ArrowIndices.Add(i2);
            ArrowIndices.Add(i1);

            ArrowIndices.Add(i0);
            ArrowIndices.Add(i3);
            ArrowIndices.Add(i2);
        }
    }
}
