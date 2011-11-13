using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_21 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fq; // Force Value
        EMLoadDirPCC1 m_eDirPPC; // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float Fq
        {
            get { return m_fq; }
            set { m_fq = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_21()
        {
        
        
        }

        public CMLoad_21(float fqValue, EMLoadDirPCC1 eDir)
        {
            m_fq = fqValue;
            m_eDirPPC = eDir;
        }
    }
}
