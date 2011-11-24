using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;

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


        // Vector of node coordinates in carthesian coordinate system
        CVector m_fVNodeCoordinates;
        
        public CVector FVNodeCoordinates
        {
            get { return m_fVNodeCoordinates; }
            set { m_fVNodeCoordinates = value; }
        }

        float fTime;

        public float FTime
        {
            get { return fTime; }
            set { fTime = value; }
        }

        public CN()
        { 
        
        }
    }
}
