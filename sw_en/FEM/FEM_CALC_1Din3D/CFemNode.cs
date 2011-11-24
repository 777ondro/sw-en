using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATH;
using FEM_CALC_BASE;

namespace FEM_CALC_1Din3D
{
    public class CFemNode : CN
    {
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
            ID = iNNo;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector ArrDisp, CVector VLoad)
        {
            ID = iNNo;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
            m_VDisp = ArrDisp;
           m_DirNodeLoad = VLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            ID = TopoNode.INode_ID;
            Fill_NDisp_Init();
            Fill_NCode_Init();
            Fill_NLoad_Init();
            FVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX] = TopoNode.FCoord_X;
            FVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY] = TopoNode.FCoord_Y;
            FVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ] = TopoNode.FCoord_Z;
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
                if ((ElemArray[i].NodeStart == Node) || (ElemArray[i].NodeEnd == Node))
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
                if ((ElemArray[i].NodeStart == Node) || (ElemArray[i].NodeEnd == Node))
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
            m_VDisp.FVectorItems[(int)e3D_DOF.eUX] = float.PositiveInfinity;
            m_VDisp.FVectorItems[(int)e3D_DOF.eUY] = float.PositiveInfinity;
            m_VDisp.FVectorItems[(int)e3D_DOF.eUZ] = float.PositiveInfinity;
            m_VDisp.FVectorItems[(int)e3D_DOF.eRX] = float.PositiveInfinity;
            m_VDisp.FVectorItems[(int)e3D_DOF.eRY] = float.PositiveInfinity;
            m_VDisp.FVectorItems[(int)e3D_DOF.eRZ] = float.PositiveInfinity;
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
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eFX] = 0f;
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eFY] = 0f;
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eFZ] = 0f;
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eMX] = 0f;
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eMY] = 0f;
            m_DirNodeLoad.FVectorItems[(int)e3D_E_F.eMZ] = 0f;
        }




        ///////////////////////////////////////////////////////////////////

































    }
}
