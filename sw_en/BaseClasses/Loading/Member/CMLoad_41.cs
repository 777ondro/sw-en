using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_41 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fq; // Force Value
        private float m_fa;      // Slope projection
        private float m_fb;      // Distance of horiyontal plateau of Load / User Input
        EMLoadDirPCC1 m_eDirPPC; // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float Fq
        {
            get { return m_fq; }
            set { m_fq = value; }
        }

        public float Fa
        {
            get { return m_fa; }
            set { m_fa = value; }
        }
        public float Fb
        {
            get { return m_fb; }
            set { m_fb = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_41()
        {
        
        
        }
    }
}
