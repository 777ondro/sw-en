using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;
using MATH;

namespace FEM_CALC_1Din2D
{
    public class CFemNode:CNode
    {
        public CVector m_VDisp = new CVector(Constants.i2D_DOFNo);
        public int[] m_ArrNCodeNo = new int[Constants.i2D_DOFNo];          // Array of global codes numbers
        public float[] m_ArrDirNodeLoad = new float[Constants.i2D_DOFNo];  // Direct nodal load
        public bool[] m_ArrNodeDOF = new bool[Constants.i2D_DOFNo];        // Nodal Supports - Node DOF restraints

        // Constructor 1
        public CFemNode()
        {

        }

        // Constructor 2
        public CFemNode(int iNNo)
        {
            INode_ID = iNNo;
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector VDisp, float[] ArrLoad)
        {
            INode_ID = iNNo;
            m_VDisp = VDisp;
            m_ArrDirNodeLoad = ArrLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNSupport [] arrNSupports, CNode TopoNode)
        {
            INode_ID = TopoNode.INode_ID;
            FCoord_X = TopoNode.FCoord_X;
            FCoord_Y = TopoNode.FCoord_Y;
            FCoord_Z = TopoNode.FCoord_Z;
            FTime = TopoNode.FTime;

            // Get nodal support
            // Search if node is in list of supported nodes for each nodal support
            for (int i = 0; i < arrNSupports.Length; i++) // Check all nodal supports
            {
                for (int j = 0; j < arrNSupports[i].m_iNodeCollection.Length; j++) // Check list of nodes (Nodes IDs collection)
                {
                    if (INode_ID == arrNSupports[i].m_iNodeCollection[j])
                    {
                        m_ArrNodeDOF[(int)e2D_DOF.eUX] = arrNSupports[i].m_bRestrain[(int)e2D_DOF.eUX]; // !!! 2D Environment enum
                        m_ArrNodeDOF[(int)e2D_DOF.eUY] = arrNSupports[i].m_bRestrain[(int)e2D_DOF.eUY]; // !!! 2D Environment enum
                        m_ArrNodeDOF[(int)e2D_DOF.eRZ] = arrNSupports[i].m_bRestrain[(int)e2D_DOF.eRZ]; // !!! 2D Environment enum
                    }
                }
            }
        }


        public void CopyTopoNodetoFemNode(CNode TopoNode)
        { 
        // 2D Environment
        INode_ID = TopoNode.INode_ID;
        FCoord_X = TopoNode.FCoord_X;
        FCoord_Y = TopoNode.FCoord_Y;

        FTime = TopoNode.FTime;
        }


        // Function returns list of FEM 1D elements which includes given node
        // Do we need to store whole elements object (array of elements indexes should be enough) !!!

        public ArrayList GetFoundedMembers(CFemNode Node, CE_1D[] ElemArray, int iElemNo)
        {
            int j = 0;
            ArrayList ElemTempList = new ArrayList();

            for (int i = 0; i < iElemNo; i++)
            {
                if ((ElemArray[i].m_NodeStart == Node) || (ElemArray[i].m_NodeEnd == Node))
                {
                    ElemTempList.Add(i); // Add Element to Element List
                    j++;
                }
            }

            return ElemTempList;
        }

        // Array of int values
        public ArrayList GetFoundedMembers_Index(CFemNode Node, CE_1D[] ElemArray, int iElemNo)
        {
            int j = 0;
            ArrayList IndexList = new ArrayList();

            for (int i = 0; i < iElemNo; i++)
            {
                if ((ElemArray[i].m_NodeStart == Node) || (ElemArray[i].m_NodeEnd == Node))
                {
                    IndexList.Add(i); // Add Element to Element Index Array
                    j++;
                }
            }

            return IndexList;
        }
    }
}
