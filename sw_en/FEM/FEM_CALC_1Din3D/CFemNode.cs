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
        }

        // Constructor 2
        public CFemNode(int iNNo)
        {
            ID = iNNo;
        }

        // Constructor 3
        public CFemNode(int iNNo, CVector ArrDisp, CVector VLoad)
        {
            ID = iNNo;
            m_VDisp = ArrDisp;
            m_VDirNodeLoad = VLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            ID = TopoNode.ID;
            m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUX] = TopoNode.FCoord_X;
            m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUY] = TopoNode.FCoord_Y;
            m_fVNodeCoordinates.FVectorItems[(int)e3D_DOF.eUZ] = TopoNode.FCoord_Z;
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
































    }
}
