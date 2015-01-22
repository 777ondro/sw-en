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
            m_arrNReleases = new BaseClasses.CNRelease[2];
            m_arrNLoads = new BaseClasses.CNLoad[2];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array

            m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.02f, 0.01f); // Solid square section

            // Nodes
            // Nodes List - Nodes Array

            m_arrNodes[0] = new BaseClasses.CNode(1, 0, 0, 0, 0);
            m_arrNodes[1] = new BaseClasses.CNode(2, 5, 0, 0, 0);

            // Sort by ID
            //Array.Sort(m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_arrMembers[0] = new BaseClasses.CMember(1, m_arrNodes[0], m_arrNodes[1], m_arrCrSc[0], 0);

            //Sort by ID
            //Array.Sort(m_arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values
            // Set values
            //bool[] bSupport1 = { true, true, true, false, false, false };
            //bool[] bSupport2 = { false, true, true, false, false, false };

            //bool[] bSupport1 = { false, false, false, true, true, true };
            //bool[] bSupport2 = { true, true, true, true, true,true };

            bool[] bSupport1 = { true, false, true, true, true, true };
            bool[] bSupport2 = { false, true, true, true, false, false };

            // Create Support Objects
            // Pozn. Jednym z parametrov by malo byt pole ID uzlov v ktorych je zadefinovana tato podpora
            // objekt podpory bude len jeden a dotknute uzly budu vediet ze na ich podpora existuje a ake je konkretne ID jej nastaveni
            m_arrNSupports[0] = new BaseClasses.CNSupport(6, 1, m_arrNodes[0], bSupport1, 0);
            m_arrNSupports[1] = new BaseClasses.CNSupport(6, 2, m_arrNodes[1], bSupport2, 0);

            // Sort by ID
            //Array.Sort(m_arrNSupports, new BaseClasses.CCompare_NSupportID());

            // Member Releases

            bool?[] bRelease1 = { true, false, true, true, true, true };
            bool?[] bRelease2 = { false, false, false, false, false, false };
            m_arrNReleases[0] = new BaseClasses.CNRelease(6, m_arrMembers[0].NodeStart, m_arrMembers[0], bRelease1, 0);
            m_arrNReleases[1] = new BaseClasses.CNRelease(6, m_arrMembers[0].NodeEnd, m_arrMembers[0], bRelease2, 0);

            // Nodal Loads
            m_arrNLoads[0] = new BaseClasses.CNLoadSingle(m_arrNodes[0], ENLoadType.eNLT_Fx, 2.5f, true, 0);
            m_arrNLoads[1] = new BaseClasses.CNLoadSingle(m_arrNodes[1], ENLoadType.eNLT_Mx, 2f, true, 0);
        }
    }
}
