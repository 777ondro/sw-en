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

            const int iFrameNodes = 5;
            const int iFrameNo = 5;

            m_arrNodes = new BaseClasses.CNode[iFrameNodes * iFrameNo];
            m_arrMembers = new CMember[iFrameNo * (iFrameNodes - 1)];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[1];
            m_arrNSupports = new BaseClasses.CNSupport[2 * iFrameNo];
            //m_arrNLoads = new BaseClasses.CNLoad[35];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_arrCrSc[0] = new CCrSc_3_07(0, 0.5f, 0.2f, 0.00115f);

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            float fH1_frame = 4f;
            float fH2_frame = 6.3f;
            float fW_frame = 10f;
            float fL1_frame = 5f;

            float fL_tot = (iFrameNo - 1) * fL1_frame;


            // Nodes
            m_arrNodes[00] = new CNode(01, 000000, 0000, 00000, 0);
            m_arrNodes[01] = new CNode(02, 000000, 0000, fH1_frame, 0);
            m_arrNodes[02] = new CNode(03, 0.5f * fW_frame, 0000, fH2_frame, 0);
            m_arrNodes[03] = new CNode(04, fW_frame, 0000, fH1_frame, 0);
            m_arrNodes[04] = new CNode(05, fW_frame, 0000, 00000, 0);

            m_arrNodes[05] = new CNode(01, 000000, 1 * fL1_frame, 00000, 0);
            m_arrNodes[06] = new CNode(02, 000000, 1 * fL1_frame, fH1_frame, 0);
            m_arrNodes[07] = new CNode(03, 0.5f * fW_frame, 1 * fL1_frame, fH2_frame, 0);
            m_arrNodes[08] = new CNode(04, fW_frame, 1 * fL1_frame, fH1_frame, 0);
            m_arrNodes[09] = new CNode(05, fW_frame, 1 * fL1_frame, 00000, 0);

            m_arrNodes[10] = new CNode(01, 000000, 2 * fL1_frame, 00000, 0);
            m_arrNodes[11] = new CNode(02, 000000, 2 * fL1_frame, fH1_frame, 0);
            m_arrNodes[12] = new CNode(03, 0.5f * fW_frame, 2 * fL1_frame, fH2_frame, 0);
            m_arrNodes[13] = new CNode(04, fW_frame, 2 * fL1_frame, fH1_frame, 0);
            m_arrNodes[14] = new CNode(05, fW_frame, 2 * fL1_frame, 00000, 0);

            m_arrNodes[15] = new CNode(01, 000000, 3 * fL1_frame, 00000, 0);
            m_arrNodes[16] = new CNode(02, 000000, 3 * fL1_frame, fH1_frame, 0);
            m_arrNodes[17] = new CNode(03, 0.5f * fW_frame, 3 * fL1_frame, fH2_frame, 0);
            m_arrNodes[18] = new CNode(04, fW_frame, 3 * fL1_frame, fH1_frame, 0);
            m_arrNodes[19] = new CNode(05, fW_frame, 3 * fL1_frame, 00000, 0);

            m_arrNodes[20] = new CNode(01, 000000, 4 * fL1_frame, 00000, 0);
            m_arrNodes[21] = new CNode(02, 000000, 4 * fL1_frame, fH1_frame, 0);
            m_arrNodes[22] = new CNode(03, 0.5f * fW_frame, 4 * fL1_frame, fH2_frame, 0);
            m_arrNodes[23] = new CNode(04, fW_frame, 4 * fL1_frame, fH1_frame, 0);
            m_arrNodes[24] = new CNode(05, fW_frame, 4 * fL1_frame, 00000, 0);

            // Setridit pole podle ID
            //Array.Sort(m_arrNodes, new CCompare_NodeID());

            // Members Automatic Generation
            // Members List - Members Array

            // Members
            m_arrMembers[000] = new CMember(001, m_arrNodes[00], m_arrNodes[01], m_arrCrSc[0], 0);
            m_arrMembers[001] = new CMember(002, m_arrNodes[01], m_arrNodes[02], m_arrCrSc[0], 0);
            m_arrMembers[002] = new CMember(003, m_arrNodes[02], m_arrNodes[03], m_arrCrSc[0], 0);
            m_arrMembers[003] = new CMember(004, m_arrNodes[03], m_arrNodes[04], m_arrCrSc[0], 0);

            m_arrMembers[004] = new CMember(001, m_arrNodes[05], m_arrNodes[06], m_arrCrSc[0], 0);
            m_arrMembers[005] = new CMember(002, m_arrNodes[06], m_arrNodes[07], m_arrCrSc[0], 0);
            m_arrMembers[006] = new CMember(003, m_arrNodes[07], m_arrNodes[08], m_arrCrSc[0], 0);
            m_arrMembers[007] = new CMember(004, m_arrNodes[08], m_arrNodes[09], m_arrCrSc[0], 0);

            m_arrMembers[008] = new CMember(001, m_arrNodes[10], m_arrNodes[11], m_arrCrSc[0], 0);
            m_arrMembers[009] = new CMember(002, m_arrNodes[11], m_arrNodes[12], m_arrCrSc[0], 0);
            m_arrMembers[010] = new CMember(003, m_arrNodes[12], m_arrNodes[13], m_arrCrSc[0], 0);
            m_arrMembers[011] = new CMember(004, m_arrNodes[13], m_arrNodes[14], m_arrCrSc[0], 0);

            m_arrMembers[012] = new CMember(001, m_arrNodes[15], m_arrNodes[16], m_arrCrSc[0], 0);
            m_arrMembers[013] = new CMember(002, m_arrNodes[16], m_arrNodes[17], m_arrCrSc[0], 0);
            m_arrMembers[014] = new CMember(003, m_arrNodes[17], m_arrNodes[18], m_arrCrSc[0], 0);
            m_arrMembers[015] = new CMember(004, m_arrNodes[18], m_arrNodes[19], m_arrCrSc[0], 0);

            m_arrMembers[016] = new CMember(001, m_arrNodes[20], m_arrNodes[21], m_arrCrSc[0], 0);
            m_arrMembers[017] = new CMember(002, m_arrNodes[21], m_arrNodes[22], m_arrCrSc[0], 0);
            m_arrMembers[018] = new CMember(003, m_arrNodes[22], m_arrNodes[23], m_arrCrSc[0], 0);
            m_arrMembers[019] = new CMember(004, m_arrNodes[23], m_arrNodes[24], m_arrCrSc[0], 0);

            // Setridit pole podle ID
            //Array.Sort(m_arrMembers, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, true, false, false };
            bool[] bSupport2 = { false, false, true, true, false, false };

            // Create Support Objects
            m_arrNSupports[0] = new CNSupport(6, 1, m_arrNodes[00], bSupport1, 0);
            m_arrNSupports[1] = new CNSupport(6, 2, m_arrNodes[04], bSupport2, 0);

            m_arrNSupports[2] = new CNSupport(6, 1, m_arrNodes[05], bSupport1, 0);
            m_arrNSupports[3] = new CNSupport(6, 2, m_arrNodes[09], bSupport2, 0);

            m_arrNSupports[4] = new CNSupport(6, 1, m_arrNodes[10], bSupport1, 0);
            m_arrNSupports[5] = new CNSupport(6, 2, m_arrNodes[14], bSupport2, 0);

            m_arrNSupports[6] = new CNSupport(6, 1, m_arrNodes[15], bSupport1, 0);
            m_arrNSupports[7] = new CNSupport(6, 2, m_arrNodes[19], bSupport2, 0);

            m_arrNSupports[8] = new CNSupport(6, 1, m_arrNodes[20], bSupport1, 0);
            m_arrNSupports[9] = new CNSupport(6, 2, m_arrNodes[24], bSupport2, 0);

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
