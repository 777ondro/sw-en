using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;
using System.Collections;

namespace FEM_CALC_BASE
{
    public class CN
    {
        int m_ID; // Unique FEM node ID

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        const int m_iNodeDOFNumber = 3; // 3 in 2D, 6 in 3D

        // Vector of node coordinates in carthesian coordinate system
        public CVector m_fVNodeCoordinates = new CVector(m_iNodeDOFNumber);

        float fTime;

        public float FTime
        {
            get { return fTime; }
            set { fTime = value; }
        }


        public CVector m_VDisp = new CVector(m_iNodeDOFNumber);
        public int[] m_ArrNCodeNo = new int[m_iNodeDOFNumber];        // Array of global codes numbers
        public CVector m_DirNodeLoad = new CVector(m_iNodeDOFNumber);  // Direct external nodal load vector
        public bool[] m_ArrNodeDOF = new bool [m_iNodeDOFNumber];        // Nodal Supports - Node DOF restraints
        public ArrayList m_iMemberCollection = new ArrayList(); // List / Collection of FEM members connected to the node

        public CN()
        { 
        
        }
    }
}
