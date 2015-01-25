using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using CENEX;
using BaseClasses.GraphObj.Objects_3D;

namespace BaseClasses
{
    [Serializable]
    public class CNLoadSingle : CNLoad
    {
        //----------------------------------------------------------------------------
        private float m_Value;
        private ENLoadType m_nLoadType;

        //----------------------------------------------------------------------------
        public float Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
        public ENLoadType NLoadType
        {
            get { return m_nLoadType; }
            set { m_nLoadType = value; }
        }

        public float m_fOpacity;
        public Color m_Color = new Color(); // Default
        public DiffuseMaterial m_Material = new DiffuseMaterial();

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CNLoadSingle()
        {

        }

        public CNLoadSingle(CNode nNode,
              ENLoadType nLoadType,
              float fValue,
              bool bIsDislayed,
              int fTime)
        {
            Node = nNode;
            NLoadType = nLoadType;
            Value = fValue;
            BIsDisplayed = bIsDislayed;
            FTime = fTime;
        }

        public override Model3DGroup CreateM_3D_G_NLoad()
        {
            Model3DGroup model_gr = new Model3DGroup();

            if (NLoadType == ENLoadType.eNLT_Fx || NLoadType == ENLoadType.eNLT_Mx)
            {
                m_Color = Color.FromRgb(240, 0, 0);

                if (Value < 0.0f)
                    m_Color = Color.FromRgb(200, 0, 0);
            }
            else if (NLoadType == ENLoadType.eNLT_Fy || NLoadType == ENLoadType.eNLT_My)
            {
                m_Color = Color.FromRgb(0, 240, 0);

                if (Value < 0.0f)
                    m_Color = Color.FromRgb(0, 200, 0);
            }
            else //if (NLoadType == ENLoadType.eNLT_Fz || NLoadType == ENLoadType.eNLT_Mz)
            {
                m_Color = Color.FromRgb(0, 0, 240);

                if (Value < 0.0f)
                    m_Color = Color.FromRgb(0, 0, 200);
            }

            m_fOpacity = 0.9f;
            m_Material.Brush = new SolidColorBrush(m_Color);
            m_Material.Brush.Opacity = m_fOpacity;

            if (NLoadType == ENLoadType.eNLT_Fx || NLoadType == ENLoadType.eNLT_Fy || NLoadType == ENLoadType.eNLT_Fz) // Force
            {
                // Tip (cone height id 20% from force value)
                StraightLineArrow3D arrow = new StraightLineArrow3D(Value);
                GeometryModel3D model = new GeometryModel3D();
                MeshGeometry3D mesh = new MeshGeometry3D();

                mesh.Positions = arrow.GetArrowPoints();
                mesh.TriangleIndices = arrow.GetArrowIndices();
                model.Geometry = mesh;
                model.Material = m_Material;
                model_gr.Children.Add(model);  // Add traight arrow
            }
            else // Moment
            {
                // Arc
                CurvedLineArrow3D cArrowArc = new CurvedLineArrow3D(new Point3D(0, 0, 0), Value, m_Color, m_fOpacity);
                model_gr.Children.Add(cArrowArc.GetTorus3DGroup());  // Add curved segment (arc)

                // Tip (cone height is 20% from moment value)
                Arrow3DTip cArrowTip = new Arrow3DTip(0.2f * Value);

                GeometryModel3D modelTip = new GeometryModel3D();
                MeshGeometry3D meshTip = new MeshGeometry3D();

                meshTip.Positions = cArrowTip.GetArrowPoints();
                meshTip.TriangleIndices = cArrowTip.GetArrowIndices();
                modelTip.Geometry = meshTip;
                modelTip.Material = m_Material;

                TranslateTransform3D TranslateArrowTip= new TranslateTransform3D(Value, 0, 0);

                // Translate model points from LCS of Arrow Tip to LCS of load
                modelTip.Transform = TranslateArrowTip;
                model_gr.Children.Add(modelTip); // Add tip model to arrow model group
            }

            // Transform (rotate and translate) load geometry model from LCS to GCS

            RotateTransform3D RotateModel = new RotateTransform3D();

            // Original force model is in z-axis
            // Original moment model is about y-axis

            Vector3D v = new Vector3D();
            double dRotationAngle = 0; // In degrees

            if(NLoadType == ENLoadType.eNLT_Fx)
            {
               v.Y = 1;
               v.X = v.Z = 0;

               dRotationAngle = -90;

               if (Value < 0.0f) // If negative value change direction
               {
                   dRotationAngle = 90;
               }
            }
            else if(NLoadType == ENLoadType.eNLT_Fy)
            {
                v.X = 1;
                v.Y = v.Z = 0;

                dRotationAngle = 90;

                if (Value < 0.0f) // If negative value change direction
                {
                    dRotationAngle = -90;
                }
            }
            else if (NLoadType == ENLoadType.eNLT_Fz)
            {
                v.X = 1;
                v.Y = v.Z = 0;

                dRotationAngle = 180;

                if (Value < 0.0f) // If negative value change direction
                {
                  v.X = v.Y = v.Z = 0; // No Rotation
                  dRotationAngle = 0;
                }
            }
            else if(NLoadType == ENLoadType.eNLT_Mx)
            {
                v.Z = 1;
                v.X = v.Y = 0;

                dRotationAngle = 90;

                if (Value < 0.0f) // If negative value change direction
                {
                    dRotationAngle = -90;
                }
            }
            else if (NLoadType == ENLoadType.eNLT_My)
            {
                v.X = v.Y = v.Z = 0; // No Rotation

                if (Value < 0.0f) // If negative value change direction
                {
                    v.Z = 1;
                    v.X = v.Z = 0;

                    dRotationAngle = 180;
                }
            }
            else //if(NLoadType == ENLoadType.eNLT_Mz)
            {
                v.X = 1;
                v.Y = v.Z = 0;

                dRotationAngle = 90;

                if (Value < 0.0f) // If negative value change direction
                {
                    dRotationAngle = -90;
                }
            }

            RotateModel.Rotation = new AxisAngleRotation3D(v, dRotationAngle);
            TranslateTransform3D TranslateModel = new TranslateTransform3D(Node.X, Node.Y, Node.Z);

            Transform3DGroup Rottransgroup = new Transform3DGroup();
            Rottransgroup.Children.Add(RotateModel);
            Rottransgroup.Children.Add(TranslateModel);

            // Translate model from LCS to GCS
            model_gr.Transform = Rottransgroup;

            return model_gr;
        }
    }
}
