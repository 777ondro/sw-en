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
        public ArrayList m_iMemberCollection; // List / Collection of FEM members connected to the node

        // Constructor 1
        public CFemNode()
        {
            Fill_Node_Init();
        }

        // Constructor 2
        public CFemNode(int iNNo)
        {
            INode_ID = iNNo;

            Fill_Node_Init();
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector VDisp, float[] ArrLoad)
        {
            INode_ID = iNNo;
            m_VDisp = VDisp;
            m_ArrDirNodeLoad = ArrLoad;

            Fill_Node_Init();
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            INode_ID = TopoNode.INode_ID;
            FCoord_X = TopoNode.FCoord_X;
            FCoord_Y = TopoNode.FCoord_Y;
            FCoord_Z = TopoNode.FCoord_Z;
            FTime = TopoNode.FTime;

            Fill_Node_Init();
        }




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Nodal loads in GCS
        public void Fill_ArrDirNodeLoad_Init()
        {
           m_ArrDirNodeLoad[(int)e2D_E_F.eFX] = 0f;
           m_ArrDirNodeLoad[(int)e2D_E_F.eFY] = 0f;
           m_ArrDirNodeLoad[(int)e2D_E_F.eMZ] = 0f;
        }

        // Nodal displacement
        public void Fill_VDisp_Init()
        {
         m_VDisp.FVectorItems[(int)e2D_DOF.eUX] = 0f;
         m_VDisp.FVectorItems[(int)e2D_DOF.eUY] = 0f;
         m_VDisp.FVectorItems[(int)e2D_DOF.eRZ] = 0f;
        }

        public void Fill_ArrNodeDOF_Init()
        {
            // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            m_ArrNodeDOF[(int)e2D_DOF.eUX] = false;
            m_ArrNodeDOF[(int)e2D_DOF.eUY] = false;
            m_ArrNodeDOF[(int)e2D_DOF.eRZ] = false;
        }

        public void Fill_Node_Init()
        {
            Fill_ArrDirNodeLoad_Init();
            Fill_VDisp_Init();
            Fill_ArrNodeDOF_Init();
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
