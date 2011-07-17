using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_13 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fMValue; // Moment Value
        private float m_fa;      // Distance from Member Start
        EMLoadDirPCC1 m_eDirPPC; // Direction in Principal Coordinate System of Member / Axis around which oment acts

        //----------------------------------------------------------------------------
        public float FMValue
        {
            get { return m_fMValue; }
            set { m_fMValue = value; }
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
        public CMLoad_13()
        {
        
        
        }
    }
}
