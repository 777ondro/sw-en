using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    public class CExample_3D_901_PF : CExample
    {
        public float fH1_frame;
        float fH2_frame;
        public float fW_frame;
        public float fL1_frame;
        int iFrameNo;
        public float fL_tot;
        public float fDist_Gird;
        public float fDist_Purlin;
        int iMainColumnNo;
        int iRafterNo;
        int iEavesPurlinNo;
        int iGirdNoInOneFrame;
        int iPurlinNoInOneFrame;

        public CExample_3D_901_PF(float fH1_temp, float fW_temp, float fL1_temp, int iFrameNo_temp, float fH2_temp, float fDist_Gird_temp, float fDist_Purlin_temp)
        {
            // Todo asi prepracovat na zoznam tried objektov

            string[,] componentTypesList = new string[9, 3]
            {
                {"Main Column","2x50020","AS"},
                {"Rafter","2x50020",""},
                {"Eaves Purlin","2x50020",""},
                {"Purlin","2x50020",""},
                {"Purlin Brace","2x50020",""},
                {"Gird","2x50020",""},
                {"Gird Brace","2x50020",""},
                {"Roofing","2x50020",""},
                {"Wall Cladding","2x50020",""}
            };

            fH1_frame = fH1_temp;
            fW_frame = fW_temp;
            fL1_frame = fL1_temp;

            iFrameNo = iFrameNo_temp;

            fH2_frame = fH2_temp;
            fL_tot = (iFrameNo - 1) * fL1_frame;

            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            const int iFrameNodesNo = 5;
            const int iEavesPurlinNoInOneFrame = 2;
            iEavesPurlinNo = iEavesPurlinNoInOneFrame * (iFrameNo-1);
            iMainColumnNo = iFrameNo * 2;
            iRafterNo = iFrameNo * 2;

            m_arrNodes = new BaseClasses.CNode[iFrameNodesNo * iFrameNo];
            m_arrMembers = new CMember[iMainColumnNo + iRafterNo + iEavesPurlinNo];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[3];
            m_arrNSupports = new BaseClasses.CNSupport[2 * iFrameNo];
            //m_arrNLoads = new BaseClasses.CNLoad[35];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_arrCrSc[0] = new CCrSc_0_05(0.5f, 0.2f); // Main Column
            m_arrCrSc[1] = new CCrSc_0_05(0.6f, 0.2f); // Rafter
            //m_arrCrSc[2] = new CCrSc_0_05(0.4f, 0.2f); // Eaves Purlin
            m_arrCrSc[2] = new CCrSc_3_51_C_LIP2_FS50020(0.5f, 0.1f, 0.02f, 0.05f, 0.01f);

            m_arrCrSc[0].CSColor = Colors.DarkKhaki;
            m_arrCrSc[1].CSColor = Colors.DarkOrange;

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes
            for (int i = 0; i < iFrameNo; i++)
            {
                m_arrNodes[i * iFrameNodesNo + 0] = new CNode(i * iFrameNodesNo + 1, 000000, i * fL1_frame, 00000, 0);
                m_arrNodes[i * iFrameNodesNo + 1] = new CNode(i * iFrameNodesNo + 2, 000000, i * fL1_frame, fH1_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 2] = new CNode(i * iFrameNodesNo + 3, 0.5f * fW_frame, i * fL1_frame, fH2_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 3] = new CNode(i * iFrameNodesNo + 4, fW_frame, i * fL1_frame, fH1_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 4] = new CNode(i * iFrameNodesNo + 5, fW_frame, i * fL1_frame, 00000, 0);
            }

            // Setridit pole podle ID
            //Array.Sort(m_arrNodes, new CCompare_NodeID());

            // Members Automatic Generation
            // Members List - Members Array

            // Members
            for (int i = 0; i < iFrameNo; i++)
            {
                // Main Column
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 0] = new CMember(i * (iFrameNodesNo - 1) + 1, m_arrNodes[i * iFrameNodesNo + 0], m_arrNodes[i * iFrameNodesNo + 1], m_arrCrSc[0], 0);
                // Rafters
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 1] = new CMember(i * (iFrameNodesNo - 1) + 2, m_arrNodes[i * iFrameNodesNo + 1], m_arrNodes[i * iFrameNodesNo + 2], m_arrCrSc[1], 0);
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 2] = new CMember(i * (iFrameNodesNo - 1) + 3, m_arrNodes[i * iFrameNodesNo + 2], m_arrNodes[i * iFrameNodesNo + 3], m_arrCrSc[1], 0);
                // Main Column
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 3] = new CMember(i * (iFrameNodesNo - 1) + 4, m_arrNodes[i * iFrameNodesNo + 3], m_arrNodes[i * iFrameNodesNo + 4], m_arrCrSc[0], 0);

                // Eaves Purlins
                if (i < (iFrameNo - 1))
                {
                    m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 4] = new CMember((i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 5, m_arrNodes[i * iFrameNodesNo + 1], m_arrNodes[(i + 1) * iFrameNodesNo + 1], m_arrCrSc[2], 0);
                    m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 5] = new CMember((i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 6, m_arrNodes[i * iFrameNodesNo + 3], m_arrNodes[(i + 1) * iFrameNodesNo + 3], m_arrCrSc[2], 0);
                }
            }

            // Setridit pole podle ID
            //Array.Sort(m_arrMembers, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, true, false, false };
            bool[] bSupport2 = { false, false, true, true, false, false };

            // Create Support Objects
            for (int i = 0; i < iFrameNo; i++)
            {
                m_arrNSupports[i * 2 + 0] = new CNSupport(6, i * 2 + 1, m_arrNodes[i * iFrameNodesNo], bSupport1, 0);
                m_arrNSupports[i * 2 + 1] = new CNSupport(6, i * 2 + 2, m_arrNodes[i * iFrameNodesNo + (iFrameNodesNo-1)], bSupport2, 0);
            }

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
