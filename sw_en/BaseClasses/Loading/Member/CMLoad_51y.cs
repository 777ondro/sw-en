﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class CMLoad_51y : CMLoad
    {
        //----------------------------------------------------------------------------
        private float m_ft_0_r; // Temperature Value for PCC / Upper+Z / Right+Y  (positive direction) // Teplota hore alebo vpravo na prvku / priecnom reze prierezom
        private float m_ft_0_l; // Temperature Value for PCC / Upper-Z / Right-Y  (negative direction) // Teplota dole alebo vlavo na prvku / priecnom reze prierezom
        EMLoadDirPCC1 m_eDirPPC;  // Force Direction in Principal Coordinate System of Member

        //----------------------------------------------------------------------------
        public float Ft_0_r
        {
            get { return m_ft_0_r; }
            set { m_ft_0_r = value; }
        }

        public float Ft_0_l
        {
            get { return m_ft_0_l; }
            set { m_ft_0_l = value; }
        }

        public EMLoadDirPCC1 EDirPPC
        {
            get { return m_eDirPPC; }
            set { m_eDirPPC = value; }
        }

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CMLoad_51y()
        {
        
        
        }
    }
}