using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;

namespace FEM_CALC_1Din3D
{
    public class CFemNode : CNode
    {
        static int iNodeDOFNo = 6; // int or static int !!!!

        public CVector m_ArrDisp = new CVector(iNodeDOFNo);
        public int[] m_ArrNCodeNo = new int [iNodeDOFNo];      // array of global codes numbers
        public CVector m_ArrDirNodeLoad = new CVector(iNodeDOFNo);  // Direct nodal load

        // Constructor 1
        public CFemNode() 
        {
            // Fill Arrays / Initialize
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
        }

        // Constructor 2
        public CFemNode(int iNNo)
        {
            INode_ID = iNNo;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector ArrDisp, CVector ArrLoad)
        {
            INode_ID = iNNo;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
            m_ArrDisp = ArrDisp;
            m_ArrDirNodeLoad = ArrLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            INode_ID = TopoNode.INode_ID;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
            FCoord_X = TopoNode.FCoord_X;
            FCoord_Y = TopoNode.FCoord_Y;
            FCoord_Z = TopoNode.FCoord_Z;
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




        /////////////////////////////////////////////////////////////////
        // Auxiliary operations

        // Detaulf values of Vectors (Array)


        public void Fill_NDisp_Init()
        {
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUX] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUY] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eUZ] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRX] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRY] = float.PositiveInfinity;
            m_ArrDisp.FVectorItems[(int)e3D_DOF.eRZ] = float.PositiveInfinity;
        }

        public void Fill_NCode_Init()
        {
            m_ArrNCodeNo[(int)e3D_DOF.eUX] = int.MaxValue;
            m_ArrNCodeNo[(int)e3D_DOF.eUY] = int.MaxValue;
            m_ArrNCodeNo[(int)e3D_DOF.eUZ] = int.MaxValue;
            m_ArrNCodeNo[(int)e3D_DOF.eRX] = int.MaxValue;
            m_ArrNCodeNo[(int)e3D_DOF.eRY] = int.MaxValue;
            m_ArrNCodeNo[(int)e3D_DOF.eRZ] = int.MaxValue;
        }

        public void Fill_NLoad_Init()
        {
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eFX] = 0f;
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eFY] = 0f;
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eFZ] = 0f;
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eMX] = 0f;
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eMY] = 0f;
            m_ArrDirNodeLoad.FVectorItems[(int)e3D_E_F.eMZ] = 0f;
        }




        ///////////////////////////////////////////////////////////////////

































    }
}
