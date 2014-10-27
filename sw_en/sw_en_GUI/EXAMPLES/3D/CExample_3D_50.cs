using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_50 : CExample
    {
        public CExample_3D_50()
        {
            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrNodes = new BaseClasses.CNode[2];
            m_arrMembers = new CMember[1];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[1];
            m_arrNSupports = new BaseClasses.CNSupport[2];
            //m_arrNLoads = new BaseClasses.CNLoad[3];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array

            m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.8f, 0.5f); // Solid square section

            // Nodes
            // Nodes List - Nodes Array

            m_arrNodes[0] = new BaseClasses.CNode(1, 0, 0, 0, 0);
            m_arrNodes[1] = new BaseClasses.CNode(2, 5, 0, 0, 0);

            // Sort by ID
            Array.Sort(m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_arrMembers[0] = new BaseClasses.CMember(1, m_arrNodes[0], m_arrNodes[1], m_arrCrSc[0], 0);

            //Sort by ID
            Array.Sort(m_arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values
            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };


            // Create Support Objects
            // Pozn. Jednym z parametrov by malo byt pole ID uzlov v ktorych je zadefinovana tato podpora
            // objekt podpory bude len jeden a dotknute uzly budu vediet ze na ich podpora existuje a ake je konkretne ID jej nastaveni
            m_arrNSupports[0] = new BaseClasses.CNSupport(6, 1, m_arrNodes[0], bSupport1, 0);
            m_arrNSupports[1] = new BaseClasses.CNSupport(6, 2, m_arrNodes[1], bSupport2, 0);

            // Sort by ID
            Array.Sort(m_arrNSupports, new BaseClasses.CCompare_NSupportID());
        }
    }
}
