using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace BaseClasses.GraphObj.Objects_3D
{
    public class CurvedLineArrow3D
    {
        public float fTip_X;
        public float fTip_Y;
        public float fTip_Z;

        public float fConeHeight;
        public float fCylinderHeight;
        public float fTotalLength;
        public float fLineRadius;
        public float fRadius;

        public float[,] fAnnulusOutPoints;
        public float[,] fAnnulusInPoints;

        public Point3DCollection ArrowPoints;
        public Color SurfaceColor;

        const int number_of_segments = 72;


        public CurvedLineArrow3D()
        { }

        public CurvedLineArrow3D(float tip_X, float tip_Y, float tip_Z, float lineRadius, Color cColor)
        {
            fTip_X = tip_X;
            fTip_Y = tip_Y;
            fTip_Z = tip_Z;
            fLineRadius = lineRadius;
            SurfaceColor = cColor;

            fConeHeight = 0.2f * lineRadius;
            fRadius = 0.01f * lineRadius;
            fCylinderHeight = lineRadius - fConeHeight;

            AnnulusPoints(tip_X, tip_Y, lineRadius * 0.01f, lineRadius * 0.05f);

            //GetTorus3DGroup();
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
                cPointsCollection.Add(new Point3D(fAnnulusInPoints[i, 0], fAnnulusInPoints[i, 1], fLineRadius));
            }

            cPointsCollection.Add(new Point3D(fTip_X, fTip_Y, fLineRadius)); // Top middle point

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

        public Model3DGroup GetTorus3DGroup()
        {
            ParametricSurface ps = new ParametricSurface(fLineRadius, fRadius, SurfaceColor);

            ps.Umin = 0;
            ps.Umax = /*2 **/ Math.PI; // hlavny uhol
            ps.Vmin = 0;
            ps.Vmax = 2 * Math.PI;
            ps.Nu = 60; // delenie
            ps.Nv = 30;
            ps.CreateSurface();

            return ps.Group3D;
        }
    }
}
