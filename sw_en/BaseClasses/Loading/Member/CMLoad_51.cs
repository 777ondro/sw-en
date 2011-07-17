using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_51 : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_fFValue1; // Temperature Value for PCC / Upper+Z / Right+Y  (positive direction) // Teplota hore alebo vpravo na prvku / priecnom reze prierezom
        private float m_fFValue2; // Temperature Value for PCC / Upper-Z / Right-Y  (negative direction) // Teplota dole alebo vlavo na prvku / priecnom reze prierezom
        EMLoadDirPCC1 m_eDirPPC;  // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float FFValue1
        {
            get { return m_fFValue1; }
            set { m_fFValue1 = value; }
        }

        public float FFValue2
        {
            get { return m_fFValue2; }
            set { m_fFValue2 = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_51()
        {
        
        
        }
    }
}
