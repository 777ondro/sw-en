using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_12 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fF; // Force Value
        EMLoadDirPCC1 m_eDirPPC; // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float FF
        {
            get { return m_fF; }
            set { m_fF = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_12()
        {
        
        
        }
    }
}
