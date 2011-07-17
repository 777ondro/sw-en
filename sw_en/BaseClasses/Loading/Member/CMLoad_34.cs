using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_34 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fqValue; // Force Value
        private float m_fa;      // Distance of Load from Member Start
        private float m_fs;      // Distance of Load / Length along which load acts
        EMLoadDirPCC1 m_eDirPPC; // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float FqValue
        {
            get { return m_fqValue; }
            set { m_fqValue = value; }
        }
        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }
        public float Fs
        {
            get { return m_fs; }
            set { m_fs = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_34()
        {
        
        
        }
    }
}
