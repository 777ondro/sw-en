using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;

namespace FEM_CALC_1Din3D
{
    public struct SNodeCoord
    {
        public float s_fCoord_X;
        public float s_fCoord_Y;
        public float s_fCoord_Z;
    };

    public struct SNodeDisp
    {
        public float s_fUX;
        public float s_fUY;
        public float s_fUZ;
        public float s_fRX;
        public float s_fRY;
        public float s_fRZ;
    };

    public struct SNodeCodeNo
    {
        public int s_iUX_CodeNo;
        public int s_iUY_CodeNo;
        public int s_iUZ_CodeNo;
        public int s_iRX_CodeNo;
        public int s_iRY_CodeNo;
        public int s_iRZ_CodeNo;
    };

    public struct SNodeLoad
    {
        public float s_fFX;
        public float s_fFY;
        public float s_fFZ;
        public float s_fMX;
        public float s_fMY;
        public float s_fMZ;
    };

    public class CFemNode : CNode
    {
        Constants c = new Constants();

        //public int INode_ID;              // Node ID (start with 1
        //public SNodeCoord m_sCoord;     // node coordinates
        //public SNodeDisp m_sDisp;       // vector of displacement
        //public SNodeCodeNo m_sNCodeNo;  // vector of global code numbers
        //public SNodeLoad m_sLoad;       // vector of internal forces

        static int iNodeDOFNo = 6; // int ot static int !!!!

        public float[] m_ArrDisp = new float [iNodeDOFNo];
        public int[] m_ArrNCodeNo = new int [iNodeDOFNo];      // array of global codes numbers
        public float[] m_ArrDirNodeLoad = new float [iNodeDOFNo];  // Direct nodal load

        // Constructor 1
        public CFemNode() 
        {
            // Fill Structures / Initialize
            Fill_NDisp_InitStr();
            Fill_NCode_InitStr();
            Fill_NLoad_InitStr();

            // Fill Arrays / Initialize
            Fill_NDisp_ArrwithStr();
            Fill_NCode_ArrwithStr();
            Fill_NLoad_ArrwithStr();

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

        // Temporary Structure vs Array

        // Fill Array with structure values

        public void Fill_NDisp_ArrwithStr()
        {
            //m_ArrDisp[c.UX] = m_sDisp.s_fUX;
            //m_ArrDisp[c.UY] = m_sDisp.s_fUY;
            //m_ArrDisp[c.UZ] = m_sDisp.s_fUZ;
            //m_ArrDisp[c.RX] = m_sDisp.s_fRX;
            //m_ArrDisp[c.RY] = m_sDisp.s_fRY;
            //m_ArrDisp[c.RZ] = m_sDisp.s_fRZ;
        }

        public void Fill_NCode_ArrwithStr()
        {
            //m_ArrNCodeNo[c.UX] = m_sNCodeNo.s_iUX_CodeNo;
            //m_ArrNCodeNo[c.UY] = m_sNCodeNo.s_iUY_CodeNo;
            //m_ArrNCodeNo[c.UZ] = m_sNCodeNo.s_iUZ_CodeNo;
            //m_ArrNCodeNo[c.RX] = m_sNCodeNo.s_iRX_CodeNo;
            //m_ArrNCodeNo[c.RY] = m_sNCodeNo.s_iRY_CodeNo;
            //m_ArrNCodeNo[c.RZ] = m_sNCodeNo.s_iRZ_CodeNo;
        }

        public void Fill_NLoad_ArrwithStr()
        {
            //m_ArrDirNodeLoad[c.FX] = m_sLoad.s_fFX;
            //m_ArrDirNodeLoad[c.FY] = m_sLoad.s_fFY;
            //m_ArrDirNodeLoad[c.FZ] = m_sLoad.s_fFZ;
            //m_ArrDirNodeLoad[c.MX] = m_sLoad.s_fMX;
            //m_ArrDirNodeLoad[c.MY] = m_sLoad.s_fMY;
            //m_ArrDirNodeLoad[c.MZ] = m_sLoad.s_fMZ;
        }

        public void Fill_NDisp_InitStr()
        {
            //m_sDisp.s_fUX = float.PositiveInfinity;
            //m_sDisp.s_fUY = float.PositiveInfinity;
            //m_sDisp.s_fUZ = float.PositiveInfinity;
            //m_sDisp.s_fRX = float.PositiveInfinity;
            //m_sDisp.s_fRY = float.PositiveInfinity;
            //m_sDisp.s_fRZ = float.PositiveInfinity;
        }

        public void Fill_NCode_InitStr()
        {
            //m_sNCodeNo.s_iUX_CodeNo = int.MaxValue;
            //m_sNCodeNo.s_iUY_CodeNo = int.MaxValue;
            //m_sNCodeNo.s_iUZ_CodeNo = int.MaxValue;
            //m_sNCodeNo.s_iRX_CodeNo = int.MaxValue;
            //m_sNCodeNo.s_iRY_CodeNo = int.MaxValue;
            //m_sNCodeNo.s_iRZ_CodeNo = int.MaxValue;
        }

        public void Fill_NLoad_InitStr()
        {
            //m_sLoad.s_fFX = 0f;
            //m_sLoad.s_fFY = 0f;
            //m_sLoad.s_fFZ = 0f;
            //m_sLoad.s_fMX = 0f;
            //m_sLoad.s_fMY = 0f;
            //m_sLoad.s_fMZ = 0f;
        }




        ///////////////////////////////////////////////////////////////////

































    }
}
