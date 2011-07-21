using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_51z : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_ft_0_u; // Temperature Value for PCC / Upper+Z / Right+Y  (positive direction) // Teplota hore alebo vpravo na prvku / priecnom reze prierezom
        private float m_ft_0_b; // Temperature Value for PCC / Upper-Z / Right-Y  (negative direction) // Teplota dole alebo vlavo na prvku / priecnom reze prierezom
        EMLoadDirPCC1 m_eDirPPC;  // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float Ft_0_u
        {
            get { return m_ft_0_u; }
            set { m_ft_0_u = value; }
        }

        public float Ft_0_b
        {
            get { return m_ft_0_b; }
            set { m_ft_0_b = value; }
        }
        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_51z()
        {
        
        
        }
    }
}
