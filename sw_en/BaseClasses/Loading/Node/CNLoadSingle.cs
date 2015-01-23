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

            if (NLoadType == ENLoadType.eNLT_Fx || NLoadType == ENLoadType.eNLT_Fy || NLoadType == ENLoadType.eNLT_Fz)
            {
                // Tip (cone height id 20% from force value)
                StraightLineArrow3D arrow = new StraightLineArrow3D(Node.X, Node.Y, Node.Z, Value);
                GeometryModel3D model = new GeometryModel3D();
                MeshGeometry3D mesh = new MeshGeometry3D();

                mesh.Positions = arrow.GetArrowPoints();
                mesh.TriangleIndices = arrow.GetArrowIndices();
                model.Geometry = mesh;

                m_Color = Color.FromRgb(200, 100, 0);
                m_fOpacity = 0.9f;
                m_Material.Brush = new SolidColorBrush(m_Color);
                m_Material.Brush.Opacity = m_fOpacity;
                model.Material = m_Material;

                model_gr.Children.Add(model);  // Straight
            }
            else
            {
                m_Color = Color.FromRgb(200, 100, 0);
                m_fOpacity = 0.9f;
                m_Material.Brush = new SolidColorBrush(m_Color);
                m_Material.Brush.Opacity = m_fOpacity;

                // Arc
                CurvedLineArrow3D cArrowArc = new CurvedLineArrow3D(new Point3D(Node.X, Node.Y, Node.Z), Value, m_Color, m_fOpacity);
                model_gr.Children.Add(cArrowArc.GetTorus3DGroup());  // Add curved segment (arc)

                // Tip (cone height id 20% from moment value)
                Arrow3DTip cArrowTip = new Arrow3DTip(Node.X + Value, Node.Y, Node.Z - 0.2f * Value, 0.2f * Value);

                GeometryModel3D model = new GeometryModel3D();
                MeshGeometry3D mesh = new MeshGeometry3D();

                mesh.Positions = cArrowTip.GetArrowPoints();
                mesh.TriangleIndices = cArrowTip.GetArrowIndices();
                model.Geometry = mesh;
                model.Material = m_Material;

                model_gr.Children.Add(model); // Add tip
            }

            return model_gr;
        }
    }
}
