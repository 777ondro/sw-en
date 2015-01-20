﻿using System;
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
            // Rozpracovat vykreslovanie pre rozne typy podpor

            Model3DGroup model_gr = new Model3DGroup();

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(20, 250, 20));
            brush.Opacity = 0.8f;

            // We need to transform CNode to Point3D
            //Point3D pTopEdge = new Point3D(Node.X, Node.Y, Node.Z);

            // Auxialiary - to call VOLUME functions
            CVolume volaux = new CVolume();
            GeometryModel3D GeomModel3Daux = new GeometryModel3D();
            RotateTransform3D RotateTrans3D = new RotateTransform3D();
            TranslateTransform3D Translate3D = new TranslateTransform3D();

            float fa = 0.2f; // Dimension in x / y
            float fh = 0.1f; // Dimension in z

            if (m_bRestrain[(int)ENSupportType.eNST_Uz] == true)
            {
                if (m_bRestrain[(int)ENSupportType.eNST_Ux] == false)
                {
                    GeomModel3Daux = volaux.CreateM_3D_G_Volume_8Edges(fa, 0.01f, 0.01f, new DiffuseMaterial(brush));
                    Translate3D = new TranslateTransform3D(m_Node.X - 0.5f * fa + 0.005f, m_Node.Y - 0.5f * fa + 0.005f, m_Node.Z - fh - 0.05f);
                    GeomModel3Daux.Transform = Translate3D;
                    model_gr.Children.Add(GeomModel3Daux);

                    GeomModel3Daux = volaux.CreateM_3D_G_Volume_8Edges(fa, 0.01f, 0.01f, new DiffuseMaterial(brush));
                    Translate3D = new TranslateTransform3D(m_Node.X - 0.5f * fa + 0.005f, m_Node.Y + 0.5f * fa - 0.005f, m_Node.Z - fh - 0.05f);
                    GeomModel3Daux.Transform = Translate3D;
                    model_gr.Children.Add(GeomModel3Daux);
                }

                if (m_bRestrain[(int)ENSupportType.eNST_Uy] == false)
                {
                    // Rotate around z-axis
                    RotateTrans3D.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90);
                    Transform3DGroup Trans3DGroup_1 = new Transform3DGroup();
                    Transform3DGroup Trans3DGroup_2 = new Transform3DGroup();
                    Trans3DGroup_1.Children.Add(RotateTrans3D);
                    Trans3DGroup_2.Children.Add(RotateTrans3D);

                    GeomModel3Daux = volaux.CreateM_3D_G_Volume_8Edges(fa, 0.01f, 0.01f, new DiffuseMaterial(brush));
                    Translate3D = new TranslateTransform3D(m_Node.X - 0.5f * fa + 0.005f, m_Node.Y - 0.5f * fa + 0.005f, m_Node.Z - fh - 0.05f);
                    Trans3DGroup_1.Children.Add(Translate3D);
                    GeomModel3Daux.Transform = Trans3DGroup_1;
                    model_gr.Children.Add(GeomModel3Daux);

                    GeomModel3Daux = volaux.CreateM_3D_G_Volume_8Edges(fa, 0.01f, 0.01f, new DiffuseMaterial(brush));
                    Translate3D = new TranslateTransform3D(m_Node.X + 0.5f * fa - 0.005f, m_Node.Y - 0.5f * fa + 0.005f, m_Node.Z - fh - 0.05f);
                    Trans3DGroup_2.Children.Add(Translate3D);
                    GeomModel3Daux.Transform = Trans3DGroup_2;
                    model_gr.Children.Add(GeomModel3Daux);
                }

                if (m_bRestrain[(int)ENSupportType.eNST_Rx] == false)
                {
                    model_gr.Children.Add(volaux.CreateM_G_M_3D_Volume_5Edges(new Point3D(m_Node.X, m_Node.Y, m_Node.Z), fa, fh, new DiffuseMaterial(brush)));
                    model_gr.Children.Add(volaux.CreateM_3D_G_Volume_Sphere(new Point3D(m_Node.X, m_Node.Y, m_Node.Z), 0.04f, new DiffuseMaterial(brush)));

                    if (m_bRestrain[(int)ENSupportType.eNST_Rz] == false)
                    {
                        model_gr.Children.Add(volaux.CreateM_G_M_3D_Volume_Cylinder(new Point3D(m_Node.X, m_Node.Y, m_Node.Z - 1.5f * fh), 0.25f * fa, 0.5f * fh, new DiffuseMaterial(brush)));
                    }
                }
                else if (m_bRestrain[(int)ENSupportType.eNST_Rx] == true)
                {
                    model_gr.Children.Add(volaux.CreateM_G_M_3D_Volume_6Edges_CN(new Point3D(m_Node.X, m_Node.Y, m_Node.Z), fa, fh, new DiffuseMaterial(brush)));

                    GeomModel3Daux = volaux.CreateM_G_M_3D_Volume_Cylinder(new Point3D(m_Node.X, m_Node.Y - 0.5f * fa, m_Node.Z), 0.25f * fa, fa, new DiffuseMaterial(brush));
                    RotateTrans3D = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));

                    //Transform3DGroup Trans3DGroup = new Transform3DGroup();
                    //Trans3DGroup.Children.Add(RotateTrans);
                    //n.Transform = Trans3DGroup;
                    GeomModel3Daux.Transform = RotateTrans3D;

                    model_gr.Children.Add(GeomModel3Daux);
                }
                else
                { }
            }

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
