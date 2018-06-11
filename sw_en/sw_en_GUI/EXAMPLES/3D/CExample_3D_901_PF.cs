using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    public class CExample_3D_901_PF : CExample
    {
        public CExample_3D_901_PF()
        {
            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrNodes = new BaseClasses.CNode[5];
            m_arrMembers = new CMember[4];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[1];
            m_arrNSupports = new BaseClasses.CNSupport[2];
            //m_arrNLoads = new BaseClasses.CNLoad[35];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_arrCrSc[0] = new CCrSc_3_07(0, 0.5f, 0.2f, 0.00115f);

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes
            m_arrNodes[00] = new CNode(01, 000000, 0000, 00000, 0);
            m_arrNodes[01] = new CNode(02, 000000, 0000, 04000, 0);
            m_arrNodes[02] = new CNode(03, 005000, 0000, 06000, 0);
            m_arrNodes[03] = new CNode(04, 010000, 0000, 04000, 0);
            m_arrNodes[04] = new CNode(05, 010000, 0000, 00000, 0);

            // Convert coordinates to meters
            foreach (CNode node in m_arrNodes)
            {
                node.X /= 1000;
                node.Y /= 1000;
                node.Z /= 1000;
            }

            // Setridit pole podle ID
            //Array.Sort(m_arrNodes, new CCompare_NodeID());

            // Members Automatic Generation
            // Members List - Members Array

            // Members
            m_arrMembers[000] = new CMember(001, m_arrNodes[00], m_arrNodes[01], m_arrCrSc[0], 0);
            m_arrMembers[001] = new CMember(002, m_arrNodes[01], m_arrNodes[02], m_arrCrSc[0], 0);
            m_arrMembers[002] = new CMember(003, m_arrNodes[02], m_arrNodes[03], m_arrCrSc[0], 0);
            m_arrMembers[003] = new CMember(004, m_arrNodes[03], m_arrNodes[04], m_arrCrSc[0], 0);

            // Setridit pole podle ID
            //Array.Sort(m_arrMembers, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, true, false, false };
            bool[] bSupport2 = { false, false, true, true, false, false };

            // Create Support Objects
            m_arrNSupports[0] = new CNSupport(6, 1, m_arrNodes[00], bSupport1, 0);
            m_arrNSupports[1] = new CNSupport(6, 2, m_arrNodes[04], bSupport2, 0);

            // Setridit pole podle ID
            Array.Sort(m_arrNSupports, new CCompare_NSupportID());

            // Member Releases / hinges - fill values

            // Set values
            bool?[] bMembRelase1 = { false, false, false, false, true, false };

            // Create Release / Hinge Objects
            //m_arrMembers[02].CnRelease1 = new CNRelease(6, m_arrMembers[02].NodeStart, bMembRelase1, 0);
        }
    }
}
