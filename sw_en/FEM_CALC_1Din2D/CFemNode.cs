using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;

namespace FEM_CALC_1Din2D
{
    public class CFemNode:CNode
    {

        static int iNodeDOFNo = 6; // int or static int !!!!

        public float[] m_ArrDisp = new float[iNodeDOFNo];
        public int[] m_ArrNCodeNo = new int[iNodeDOFNo];      // array of global codes numbers
        public float[] m_ArrDirNodeLoad = new float[iNodeDOFNo];  // Direct nodal load

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
        public CFemNode(int iNNo, float[] ArrDisp, float[] ArrLoad)
        {
            INode_ID = iNNo;
            m_ArrDisp = ArrDisp;
            m_ArrDirNodeLoad = ArrLoad;
        }

        // Constructor 4 - FEM node is copy of topological node
        public CFemNode(CNode TopoNode)
        {
            INode_ID = TopoNode.INode_ID;
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
    }
}
