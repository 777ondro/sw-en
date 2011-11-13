using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_11 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fF;      // Force Value
        private float m_fa;      // Distance from Member Start
        EMLoadDirPCC1 m_eDirPPC; // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float FF
        {
            get { return m_fF; }
            set { m_fF = value; }
        }
        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_11()
        {
        
        
        }


        public CMLoad_11(float fF, float fa)
        {
            m_fF = fF;
            m_fa = fa;
        }
    }
}
