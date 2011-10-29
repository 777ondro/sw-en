﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CENEX
{
	[Serializable]
    public class CMat_00
    {
        // Predok pre jednotlive materialy

        // Default material - steel
        public short m_sMatType = 3;
        public float m_fE = 2.1e5f;        // Unit [Pa]
        public float m_fNu = 0.3f;         // Unit [-]
        public float m_fG;                 // Unit [Pa]
        public float m_fAlpha_T = 1.2e-5f; // Unit [1/Celsius degree] 
        public float m_fRho = 7850f;       // Unit [kg/m^3]

        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        // Constructor
        public CMat_00()
        {
            m_fG = m_fE / (2f * (1f + m_fNu));
        }

        // User defined material
        public CMat_00(float fE, float fNu, float fAlpha_T, float fRho)
        {
            m_fE = fE;
            m_fG = m_fE / (2f * (1f + m_fNu));
            m_fNu = fNu;
            m_fAlpha_T = fAlpha_T;
            m_fRho = fRho;
        }

        public CMat_00(short sMatType, float fE, float fNu, float fAlpha_T, float fRho)
        {
            m_sMatType = sMatType;
            m_fE = fE;
            m_fG = m_fE / (2f * (1f + m_fNu));
            m_fNu = fNu;
            m_fAlpha_T = fAlpha_T;
            m_fRho = fRho;
        }
    }
}
