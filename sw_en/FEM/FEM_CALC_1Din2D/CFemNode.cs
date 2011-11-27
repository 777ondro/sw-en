using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;
using MATH;
using FEM_CALC_BASE;

namespace FEM_CALC_1Din2D
{
    public class CFemNode:CN
    {
        // Constructor 1
        public CFemNode()
        {
        }

        // Constructor 2
        public CFemNode(int iNNo)
        {
            ID = iNNo;
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector VDisp, CVector ArrLoad)
        {
            ID = iNNo;
            m_VDisp = VDisp;
            m_VDirNodeLoad = ArrLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            ID = TopoNode.INode_ID;
            m_fVNodeCoordinates.FVectorItems[(int)e2D_DOF.eUX] = TopoNode.FCoord_X;
            m_fVNodeCoordinates.FVectorItems[(int)e2D_DOF.eUY] = TopoNode.FCoord_Y;
            FTime = TopoNode.FTime;
        }




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CopyTopoNodetoFemNode(CNode TopoNode)
        { 
        // 2D Environment
        ID = TopoNode.INode_ID;
        m_fVNodeCoordinates.FVectorItems[(int)e2D_DOF.eUX] = TopoNode.FCoord_X;
        m_fVNodeCoordinates.FVectorItems[(int)e2D_DOF.eUY] = TopoNode.FCoord_Y;

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

        // Array of int values - returns members indexes not IDs
        public ArrayList GetFoundedMembers_Index(CFemNode Node, CE_1D[] ElemArray)
        {
            ArrayList IndexList = new ArrayList();

            for (int i = 0; i <  ElemArray.Length; i++) // Check for each member in array
            {
                if ((ElemArray[i].NodeStart == Node) || (ElemArray[i].NodeEnd == Node))
                {
                    IndexList.Add(i); // Add Element to Element Index Array
                }
            }

            return IndexList;
        }
    }
}
