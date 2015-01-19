using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using BaseClasses.GraphObj.Objects_3D;
using BaseClasses.GraphObj;

namespace BaseClasses
{
    [Serializable]
    public class CNSupport : CEntity3D
    {
        //----------------------------------------------------------------------------
        public int[] m_iNodeCollection; // List / Collection of nodes IDs where support is defined [First member index is 0]
        private CNode m_Node;
        private ENSupportType m_NSupportType;

        // Restraints - list of node degreess of freedom
        // false - 0 - free DOF
        // true - 1 - restrained (rigid)

        public int m_eNDOF;
        public bool[] m_bRestrain; // Array of boolean values, UX, UY, UZ, RX, RY, RZ

        //----------------------------------------------------------------------------
        public CNode Node
        {
            get { return m_Node; }
            set { m_Node = value; }
        }
        public ENSupportType NSupportType
        {
            get { return m_NSupportType; }
            set { m_NSupportType = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CNSupport(int eNDOF)
        {
            m_bRestrain = new bool[(int)eNDOF];
        }

        public CNSupport(int eNDOF, int iSupport_ID, CNode Node, bool[] bRestrain, int fTime)
        {
            m_eNDOF = eNDOF;
            m_bRestrain = new bool[(int)eNDOF];
            ID = iSupport_ID;
            m_Node = Node;
            m_bRestrain = bRestrain;
            BIsDisplayed = true;
            FTime = fTime;
        }

        public CNSupport(int eNDOF, int iSupport_ID,CNode Node, bool[] bRestrain, bool bIsDisplayed, int fTime)
        {
            m_eNDOF = eNDOF;
            m_bRestrain = new bool[(int)eNDOF];
            ID = iSupport_ID;
            m_Node = Node;
            m_bRestrain = bRestrain;
            BIsDisplayed = bIsDisplayed;
            FTime = fTime;
        }

        public Model3DGroup CreateM_3D_G_NSupport()
        {
            Model3DGroup model_gr = new Model3DGroup();

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(20, 250, 20));
            // We need to transform CNode to Point3D
            Point3D pTopEdge = new Point3D(Node.X, Node.Y, Node.Z);

            float fa = 0.2f;
            float fh = 0.1f;

            CVolume volPyramide = new CVolume();

            model_gr.Children.Add(volPyramide.CreateM_G_M_3D_Volume_5Edges(new Point3D(m_Node.X, m_Node.Y, m_Node.Z), fa, fh, new DiffuseMaterial(brush)));

            CVolume volSpherePyramide = new CVolume();

            model_gr.Children.Add(volPyramide.CreateM_3D_G_Volume_Sphere(new Point3D(m_Node.X, m_Node.Y, m_Node.Z), 0.04f, new DiffuseMaterial(brush)));

            CVolume volPrism = new CVolume();

            model_gr.Children.Add(volPrism.CreateM_3D_G_Volume_8Edges(new Point3D(m_Node.X - 0.5f * fa, m_Node.Y - 0.5f * fa, m_Node.Z - fh - 0.05f), fa, 0.01f, 0.01f, new DiffuseMaterial(brush), new DiffuseMaterial(brush)));
            model_gr.Children.Add(volPrism.CreateM_3D_G_Volume_8Edges(new Point3D(m_Node.X - 0.5f * fa, m_Node.Y + 0.5f * fa, m_Node.Z - fh - 0.05f), fa, 0.01f, 0.01f, new DiffuseMaterial(brush), new DiffuseMaterial(brush)));

            return model_gr;
        }

    } // End of CNSupport class

    public class CCompare_NSupportID : IComparer
    {
        // x<y - zaporne cislo; x=y - nula; x>y - kladne cislo
        public int Compare(object x, object y)
        {
            return ((CNSupport)x).ID - ((CNSupport)y).ID;
        }
    }
}
