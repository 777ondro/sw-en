using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using BaseClasses;
using MATH;
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
        public float fRoofPitch_rad;
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

            fDist_Gird = fDist_Gird_temp;
            fDist_Purlin = fDist_Purlin_temp;

            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            fRoofPitch_rad = (float)Math.Atan((fH2_frame - fH1_frame) / (0.5f * fW_frame));

            const int iFrameNodesNo = 5;
            const int iEavesPurlinNoInOneFrame = 2;
            iEavesPurlinNo = iEavesPurlinNoInOneFrame * (iFrameNo-1);
            iMainColumnNo = iFrameNo * 2;
            iRafterNo = iFrameNo * 2;

            const float fBottomGirdPosition = 0.3f;
            int iOneColumnGridNo = 0;
            iGirdNoInOneFrame = 0;

            bool bGenerateGirts = true;
            if(bGenerateGirts)
            {
                iOneColumnGridNo = (int)((fH1_frame - fBottomGirdPosition) / fDist_Gird) + 1;
                iGirdNoInOneFrame = 2 * iOneColumnGridNo;
            }

            float fFirstPurlinPosition = fDist_Purlin;
            float fRafterLength = MathF.Sqrt(MathF.Pow2(fH2_frame - fH1_frame) + MathF.Pow2(0.5f * fW_frame));

            int iOneRafterPurlinNo = 0;
            iPurlinNoInOneFrame = 0;

            bool bGeneratePurlins = true;
            if (bGeneratePurlins)
            {
                iOneRafterPurlinNo = (int)((fRafterLength - fFirstPurlinPosition) / fDist_Purlin) + 1;
                iPurlinNoInOneFrame = 2 * iOneRafterPurlinNo;
            }

            m_arrNodes = new BaseClasses.CNode[iFrameNodesNo * iFrameNo + iFrameNo * iGirdNoInOneFrame + iFrameNo * iPurlinNoInOneFrame];
            m_arrMembers = new CMember[iMainColumnNo + iRafterNo + iEavesPurlinNo + (iFrameNo - 1) * iGirdNoInOneFrame + (iFrameNo - 1) * iPurlinNoInOneFrame];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[5];
            m_arrNSupports = new BaseClasses.CNSupport[2 * iFrameNo];
            //m_arrNLoads = new BaseClasses.CNLoad[35];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            //m_arrCrSc[0] = new CCrSc_0_05(0.5f, 0.2f); // Main Column
            m_arrCrSc[0] = new CCrSc_3_51_BOX_TEMP(0.5f, 0.2f, 0.002f);
            //m_arrCrSc[1] = new CCrSc_0_05(0.6f, 0.2f); // Rafter
            m_arrCrSc[1] = new CCrSc_3_51_BOX_TEMP(0.4f, 0.2f, 0.00115f);
            //m_arrCrSc[2] = new CCrSc_0_05(0.4f, 0.2f); // Eaves Purlin
            m_arrCrSc[2] = new CCrSc_3_51_C_LIP2_FS50020(0.5f, 0.1f, 0.02f, 0.05f, 0.01f);
            m_arrCrSc[3] = new CCrSc_3_51_C_LIP2_FS50020(0.3f, 0.08f, 0.02f, 0.05f, 0.01f);
            m_arrCrSc[4] = new CCrSc_3_51_C_LIP2_FS50020(0.35f, 0.10f, 0.015f, 0.065f, 0.001f);

            m_arrCrSc[0].CSColor = Colors.DarkKhaki;
            m_arrCrSc[1].CSColor = Colors.DarkOrange;
            m_arrCrSc[2].CSColor = Colors.DarkCyan;
            m_arrCrSc[3].CSColor = Colors.DimGray;
            m_arrCrSc[4].CSColor = Colors.DarkSalmon;

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes - Frames
            for (int i = 0; i < iFrameNo; i++)
            {
                m_arrNodes[i * iFrameNodesNo + 0] = new CNode(i * iFrameNodesNo + 1, 000000, i * fL1_frame, 00000, 0);
                m_arrNodes[i * iFrameNodesNo + 1] = new CNode(i * iFrameNodesNo + 2, 000000, i * fL1_frame, fH1_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 2] = new CNode(i * iFrameNodesNo + 3, 0.5f * fW_frame, i * fL1_frame, fH2_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 3] = new CNode(i * iFrameNodesNo + 4, fW_frame, i * fL1_frame, fH1_frame, 0);
                m_arrNodes[i * iFrameNodesNo + 4] = new CNode(i * iFrameNodesNo + 5, fW_frame, i * fL1_frame, 00000, 0);
            }

            // Members
            for (int i = 0; i < iFrameNo; i++)
            {
                // Main Column
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 0] = new CMember(i * (iFrameNodesNo - 1) + 1, m_arrNodes[i * iFrameNodesNo + 0], m_arrNodes[i * iFrameNodesNo + 1], m_arrCrSc[0], -0.1f, -0.1f, 0f, 0);
                // Rafters
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 1] = new CMember(i * (iFrameNodesNo - 1) + 2, m_arrNodes[i * iFrameNodesNo + 1], m_arrNodes[i * iFrameNodesNo + 2], m_arrCrSc[1], -0.1f, -0.1f, 0f, 0);
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 2] = new CMember(i * (iFrameNodesNo - 1) + 3, m_arrNodes[i * iFrameNodesNo + 2], m_arrNodes[i * iFrameNodesNo + 3], m_arrCrSc[1], -0.1f, -0.1f, 0f, 0);
                // Main Column
                m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 3] = new CMember(i * (iFrameNodesNo - 1) + 4, m_arrNodes[i * iFrameNodesNo + 3], m_arrNodes[i * iFrameNodesNo + 4], m_arrCrSc[0], -0.1f, -0.1f, 0f, 0);

                // Eaves Purlins
                if (i < (iFrameNo - 1))
                {
                    m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 4] = new CMember((i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 5, m_arrNodes[i * iFrameNodesNo + 1], m_arrNodes[(i + 1) * iFrameNodesNo + 1], m_arrCrSc[2], -0.1f, -0.1f, 0f, 0);
                    m_arrMembers[(i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 5] = new CMember((i * iEavesPurlinNoInOneFrame) + i * (iFrameNodesNo - 1) + 6, m_arrNodes[i * iFrameNodesNo + 3], m_arrNodes[(i + 1) * iFrameNodesNo + 3], m_arrCrSc[2], -0.1f, -0.1f, 0f, 0);
                }
            }

            // Nodes - Girds
            int i_temp_numberofNodes = iFrameNodesNo * iFrameNo;
            if (bGenerateGirts)
            {
                for (int i = 0; i < iFrameNo; i++)
                {
                    for (int j = 0; j < iOneColumnGridNo; j++)
                    {
                        m_arrNodes[i_temp_numberofNodes + i * iGirdNoInOneFrame + j] = new CNode(i_temp_numberofNodes + i * iGirdNoInOneFrame + j + 1, 000000, i * fL1_frame, fBottomGirdPosition + j * fDist_Gird, 0);
                    }

                    for (int j = 0; j < iOneColumnGridNo; j++)
                    {
                        m_arrNodes[i_temp_numberofNodes + i * iGirdNoInOneFrame + iOneColumnGridNo + j] = new CNode(i_temp_numberofNodes + i * iGirdNoInOneFrame + iOneColumnGridNo + j + 1, fW_frame, i * fL1_frame, fBottomGirdPosition + j * fDist_Gird, 0);
                    }
                }
            }

            // Members - Girds
            int i_temp_numberofMembers = iMainColumnNo + iRafterNo + iEavesPurlinNoInOneFrame * (iFrameNo - 1);
            if (bGenerateGirts)
            {
                for (int i = 0; i < (iFrameNo - 1); i++)
                {
                    for (int j = 0; j < iOneColumnGridNo; j++)
                    {
                        m_arrMembers[i_temp_numberofMembers + i * iGirdNoInOneFrame + j] = new CMember(i_temp_numberofMembers + i * iGirdNoInOneFrame + j + 1, m_arrNodes[i_temp_numberofNodes + i * iGirdNoInOneFrame + j], m_arrNodes[i_temp_numberofNodes + (i + 1) * iGirdNoInOneFrame + j], m_arrCrSc[3], -0.1f, -0.1f, 0f, 0);
                    }

                    for (int j = 0; j < iOneColumnGridNo; j++)
                    {
                        m_arrMembers[i_temp_numberofMembers + i * iGirdNoInOneFrame + iOneColumnGridNo + j] = new CMember(i_temp_numberofMembers + i * iGirdNoInOneFrame + iOneColumnGridNo + j + 1, m_arrNodes[i_temp_numberofNodes + i * iGirdNoInOneFrame + iOneColumnGridNo + j], m_arrNodes[i_temp_numberofNodes + (i + 1) * iGirdNoInOneFrame + iOneColumnGridNo + j], m_arrCrSc[3], -0.1f, -0.1f, 0f, 0);
                    }
                }
            }

            // Nodes - Purlins
            i_temp_numberofNodes += bGenerateGirts ? iGirdNoInOneFrame * iFrameNo : 0;
            if (bGeneratePurlins)
            {
                for (int i = 0; i < iFrameNo; i++)
                {
                    for (int j = 0; j < iOneRafterPurlinNo; j++)
                    {
                        float x_glob, z_glob;
                        CalcPurlinNodeCoord(fFirstPurlinPosition + j * fDist_Purlin, out x_glob, out z_glob);

                        m_arrNodes[i_temp_numberofNodes + i * iPurlinNoInOneFrame + j] = new CNode(i_temp_numberofNodes + i * iPurlinNoInOneFrame + j + 1, x_glob, i * fL1_frame, z_glob, 0);
                    }

                    for (int j = 0; j < iOneRafterPurlinNo; j++)
                    {
                        float x_glob, z_glob;
                        CalcPurlinNodeCoord(fFirstPurlinPosition + j * fDist_Purlin, out x_glob, out z_glob);

                        m_arrNodes[i_temp_numberofNodes + i * iPurlinNoInOneFrame + iOneRafterPurlinNo + j] = new CNode(i_temp_numberofNodes + i * iPurlinNoInOneFrame + iOneRafterPurlinNo + j + 1, fW_frame - x_glob, i * fL1_frame, z_glob, 0);
                    }
                }
            }

            // Members - Purlins
            i_temp_numberofMembers += bGenerateGirts ? iGirdNoInOneFrame * (iFrameNo - 1) : 0;
            if (bGeneratePurlins)
            {
                for (int i = 0; i < (iFrameNo - 1); i++)
                {
                    for (int j = 0; j < iOneRafterPurlinNo; j++)
                    {
                        m_arrMembers[i_temp_numberofMembers + i * iPurlinNoInOneFrame + j] = new CMember(i_temp_numberofMembers + i * iPurlinNoInOneFrame + j + 1, m_arrNodes[i_temp_numberofNodes + i * iPurlinNoInOneFrame + j], m_arrNodes[i_temp_numberofNodes + (i + 1) * iPurlinNoInOneFrame + j], m_arrCrSc[4], -0.1f, -0.1f, 0f, 0);
                    }

                    for (int j = 0; j < iOneRafterPurlinNo; j++)
                    {
                        m_arrMembers[i_temp_numberofMembers + i * iPurlinNoInOneFrame + iOneRafterPurlinNo + j] = new CMember(i_temp_numberofMembers + i * iPurlinNoInOneFrame + iOneRafterPurlinNo + j + 1, m_arrNodes[i_temp_numberofNodes + i * iPurlinNoInOneFrame + iOneRafterPurlinNo + j], m_arrNodes[i_temp_numberofNodes + (i + 1) * iPurlinNoInOneFrame + iOneRafterPurlinNo + j], m_arrCrSc[4], -0.1f, -0.1f, 0f, 0);
                    }
                }
            }

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

        public void CalcPurlinNodeCoord(float x_rel, out float x_global, out float z_global)
        {
            x_global = (float)Math.Cos(fRoofPitch_rad) * x_rel;
            z_global = fH1_frame + (float)Math.Sin(fRoofPitch_rad) * x_rel;
        }
    }
}
