using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_21 : CExample
    {
        public CExample_3D_21()
        {
            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrNodes = new BaseClasses.CNode[50];
            m_arrMembers = new CMember[25];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[25];
            m_arrNSupports = new BaseClasses.CNSupport[3];
            //m_arrNLoads = new BaseClasses.CNLoad[3];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_arrCrSc[00] = new CCrSc_0_00(0.1f, 20); // Solid Half Circle / Semicircle shape
            m_arrCrSc[01] = new CCrSc_0_01(0.1f, 20); // Solid Quater Cirlce - chyba nezobrazuje sa jedna strana
            m_arrCrSc[02] = new CCrSc_0_02(0.1f, 20); // Rolled round bar
            m_arrCrSc[03] = new CCrSc_0_03(0.2f, 0.1f, 21); // Solid Ellipse
            m_arrCrSc[04] = new CCrSc_0_04(0.3f, 0.5f); // Triangular Prism / Equilateral
            m_arrCrSc[05] = new CCrSc_0_05(0.1f, 0.05f); // Solid square section
            m_arrCrSc[06] = new CCrSc_0_06(0.1f); // Solid Penthagon
            m_arrCrSc[07] = new CCrSc_0_07(0.1f); // Solid Hexagon
            m_arrCrSc[08] = new CCrSc_0_08(0.1f); // Solid Octagon
            m_arrCrSc[09] = new CCrSc_0_09(0.1f); // Solid Dodecagon
            m_arrCrSc[10] = new CCrSc_0_20(0.2f, 0.010f, 25); // Semicircle Curve
            m_arrCrSc[11] = new CCrSc_0_22(0.2f, 0.05f, 12); // Circular Hollow Section (Tube, Pipe)
            m_arrCrSc[12] = new CCrSc_0_23(0.2f, 0.1f, 0.020f, 24); // Elliptical Hollow Section
            m_arrCrSc[13] = new CCrSc_0_24(0.2f, 0.05f); // Triangular Prism / Equilateral with Opening
            m_arrCrSc[14] = new CCrSc_0_25(0.2f, 0.15f, 0.01f, 0.008f); // Welded hollow section - doubly symmetrical
            m_arrCrSc[15] = new CCrSc_0_26(0.2f, 0.05f); // Empty (Hollow) Penthagon
            m_arrCrSc[16] = new CCrSc_0_27(0.2f, 0.05f); // Empty (Hollow) Hexagon
            m_arrCrSc[17] = new CCrSc_0_28(0.2f, 0.05f); // Empty (Hollow) Octagon
            m_arrCrSc[18] = new CCrSc_0_50(0.2f, 0.1f, 0.015f, 0.006f); // Doubly symmetric I section
            m_arrCrSc[19] = new CCrSc_0_52(0.2f, 0.1f, 0.015f, 0.006f, -0.05f); // Monosymmetric U/C section
            m_arrCrSc[20] = new CCrSc_0_54(0.2f, 0.1f, 0.015f, 0.010f, 0.050f, 0.010f); // Welded Angle section
            m_arrCrSc[21] = new CCrSc_0_56(0.2f, 0.1f, 0.015f, 0.010f, 0.15f); // Welded monosymmetric T section
            m_arrCrSc[22] = new CCrSc_0_58(0.2f, 0.1f, 0.015f, 0.010f); // Welded centrally symmetric Z section
            m_arrCrSc[23] = new CCrSc_0_60(0.2f, 0.1f, 0.015f); // Doubly symmetric Cruciform
            m_arrCrSc[24] = new CCrSc_0_61(0.2f, 0.010f); // Y-section

            //m_arrCrSc[0] = new CCrSc_3_07(1,0.2f, 0.05f, 0.005f, 0.005f, 0.003f); // rectangular hollow section

            // Nodes
            // Nodes List - Nodes Array

            m_arrNodes[0]  = new BaseClasses.CNode(1, 0.0f, 0.0f, 0.0f, 0);
            m_arrNodes[1]  = new BaseClasses.CNode(2, 5.0f, 0.0f, 0.0f, 0);
            m_arrNodes[2]  = new BaseClasses.CNode(3, 0.0f, 1.0f, 0.0f, 0);
            m_arrNodes[3]  = new BaseClasses.CNode(4, 5.0f, 1.0f, 0.0f, 0);
            m_arrNodes[4]  = new BaseClasses.CNode(5, 0.0f, 2.0f, 0.0f, 0);
            m_arrNodes[5]  = new BaseClasses.CNode(6, 5.0f, 2.0f, 0.0f, 0);
            m_arrNodes[6]  = new BaseClasses.CNode(7, 0.0f, 3.0f, 0.0f, 0);
            m_arrNodes[7]  = new BaseClasses.CNode(8, 5.0f, 3.0f, 0.0f, 0);
            m_arrNodes[8]  = new BaseClasses.CNode(9, 0.0f, 4.0f, 0.0f, 0);
            m_arrNodes[9]  = new BaseClasses.CNode(10, 5.0f, 4.0f, 0.0f, 0);
            m_arrNodes[10] = new BaseClasses.CNode(11, 0.0f, 5.0f, 0.0f, 0);
            m_arrNodes[11] = new BaseClasses.CNode(12, 5.0f, 5.0f, 0.0f, 0);
            m_arrNodes[12] = new BaseClasses.CNode(13, 0.0f, 6.0f, 0.0f, 0);
            m_arrNodes[13] = new BaseClasses.CNode(14, 5.0f, 6.0f, 0.0f, 0);
            m_arrNodes[14] = new BaseClasses.CNode(15, 0.0f, 7.0f, 0.0f, 0);
            m_arrNodes[15] = new BaseClasses.CNode(16, 5.0f, 7.0f, 0.0f, 0);
            m_arrNodes[16] = new BaseClasses.CNode(17, 0.0f, 8.0f, 0.0f, 0);
            m_arrNodes[17] = new BaseClasses.CNode(18, 5.0f, 8.0f, 0.0f, 0);
            m_arrNodes[18] = new BaseClasses.CNode(19, 0.0f, 9.0f, 0.0f, 0);
            m_arrNodes[19] = new BaseClasses.CNode(20, 5.0f, 9.0f, 0.0f, 0);
            m_arrNodes[20] = new BaseClasses.CNode(21, 0.0f, 10.0f, 0.0f, 0);
            m_arrNodes[21] = new BaseClasses.CNode(22, 5.0f, 10.0f, 0.0f, 0);
            m_arrNodes[22] = new BaseClasses.CNode(23, 0.0f, 11.0f, 0.0f, 0);
            m_arrNodes[23] = new BaseClasses.CNode(24, 5.0f, 11.0f, 0.0f, 0);
            m_arrNodes[24] = new BaseClasses.CNode(25, 0.0f, 12.0f, 0.0f, 0);
            m_arrNodes[25] = new BaseClasses.CNode(26, 5.0f, 12.0f, 0.0f, 0);
            m_arrNodes[26] = new BaseClasses.CNode(27, 0.0f, 13.0f, 0.0f, 0);
            m_arrNodes[27] = new BaseClasses.CNode(28, 5.0f, 13.0f, 0.0f, 0);
            m_arrNodes[28] = new BaseClasses.CNode(29, 0.0f, 14.0f, 0.0f, 0);
            m_arrNodes[29] = new BaseClasses.CNode(30, 5.0f, 14.0f, 0.0f, 0);
            m_arrNodes[30] = new BaseClasses.CNode(31, 0.0f, 15.0f, 0.0f, 0);
            m_arrNodes[31] = new BaseClasses.CNode(32, 5.0f, 15.0f, 0.0f, 0);
            m_arrNodes[32] = new BaseClasses.CNode(33, 0.0f, 16.0f, 0.0f, 0);
            m_arrNodes[33] = new BaseClasses.CNode(34, 5.0f, 16.0f, 0.0f, 0);
            m_arrNodes[34] = new BaseClasses.CNode(35, 0.0f, 17.0f, 0.0f, 0);
            m_arrNodes[35] = new BaseClasses.CNode(36, 5.0f, 17.0f, 0.0f, 0);
            m_arrNodes[36] = new BaseClasses.CNode(37, 0.0f, 18.0f, 0.0f, 0);
            m_arrNodes[37] = new BaseClasses.CNode(38, 5.0f, 18.0f, 0.0f, 0);
            m_arrNodes[38] = new BaseClasses.CNode(39, 0.0f, 19.0f, 0.0f, 0);
            m_arrNodes[39] = new BaseClasses.CNode(40, 5.0f, 19.0f, 0.0f, 0);
            m_arrNodes[40] = new BaseClasses.CNode(41, 0.0f, 20.0f, 0.0f, 0);
            m_arrNodes[41] = new BaseClasses.CNode(42, 5.0f, 20.0f, 0.0f, 0);
            m_arrNodes[42] = new BaseClasses.CNode(43, 0.0f, 21.0f, 0.0f, 0);
            m_arrNodes[43] = new BaseClasses.CNode(44, 5.0f, 21.0f, 0.0f, 0);
            m_arrNodes[44] = new BaseClasses.CNode(45, 0.0f, 22.0f, 0.0f, 0);
            m_arrNodes[45] = new BaseClasses.CNode(46, 5.0f, 22.0f, 0.0f, 0);
            m_arrNodes[46] = new BaseClasses.CNode(47, 0.0f, 23.0f, 0.0f, 0);
            m_arrNodes[47] = new BaseClasses.CNode(48, 5.0f, 23.0f, 0.0f, 0);
            m_arrNodes[48] = new BaseClasses.CNode(49, 0.0f, 24.0f, 0.0f, 0);
            m_arrNodes[49] = new BaseClasses.CNode(50, 5.0f, 24.0f, 0.0f, 0);

            // Sort by ID
            Array.Sort(m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_arrMembers[0] = new BaseClasses.CMember(1, m_arrNodes[0], m_arrNodes[1], m_arrCrSc[0], 0);
            m_arrMembers[1] = new BaseClasses.CMember(2, m_arrNodes[2], m_arrNodes[3], m_arrCrSc[1], 0);
            m_arrMembers[2] = new BaseClasses.CMember(3, m_arrNodes[4], m_arrNodes[5], m_arrCrSc[2], 0);
            m_arrMembers[3] = new BaseClasses.CMember(4, m_arrNodes[6], m_arrNodes[7], m_arrCrSc[3], 0);
            m_arrMembers[4] = new BaseClasses.CMember(5, m_arrNodes[8], m_arrNodes[9], m_arrCrSc[4], 0);
            m_arrMembers[5] = new BaseClasses.CMember(6, m_arrNodes[10], m_arrNodes[11], m_arrCrSc[5], 0);
            m_arrMembers[6] = new BaseClasses.CMember(7, m_arrNodes[12], m_arrNodes[13], m_arrCrSc[6], 0);
            m_arrMembers[7] = new BaseClasses.CMember(8, m_arrNodes[14], m_arrNodes[15], m_arrCrSc[7], 0);
            m_arrMembers[8] = new BaseClasses.CMember(9, m_arrNodes[16], m_arrNodes[17], m_arrCrSc[8], 0);
            m_arrMembers[9] = new BaseClasses.CMember(10, m_arrNodes[18], m_arrNodes[19], m_arrCrSc[9], 0);
            m_arrMembers[10] = new BaseClasses.CMember(11, m_arrNodes[20], m_arrNodes[21], m_arrCrSc[10], 0);
            m_arrMembers[11] = new BaseClasses.CMember(12, m_arrNodes[22], m_arrNodes[23], m_arrCrSc[11], 0);
            m_arrMembers[12] = new BaseClasses.CMember(13, m_arrNodes[24], m_arrNodes[25], m_arrCrSc[12], 0);
            m_arrMembers[13] = new BaseClasses.CMember(14, m_arrNodes[26], m_arrNodes[27], m_arrCrSc[13], 0);
            m_arrMembers[14] = new BaseClasses.CMember(15, m_arrNodes[28], m_arrNodes[29], m_arrCrSc[14], 0);
            m_arrMembers[15] = new BaseClasses.CMember(16, m_arrNodes[30], m_arrNodes[31], m_arrCrSc[15], 0);
            m_arrMembers[16] = new BaseClasses.CMember(17, m_arrNodes[32], m_arrNodes[33], m_arrCrSc[16], 0);
            m_arrMembers[17] = new BaseClasses.CMember(18, m_arrNodes[34], m_arrNodes[35], m_arrCrSc[17], 0);
            m_arrMembers[18] = new BaseClasses.CMember(19, m_arrNodes[36], m_arrNodes[37], m_arrCrSc[18], 0);
            m_arrMembers[19] = new BaseClasses.CMember(20, m_arrNodes[38], m_arrNodes[39], m_arrCrSc[19], 0);
            m_arrMembers[20] = new BaseClasses.CMember(21, m_arrNodes[40], m_arrNodes[41], m_arrCrSc[20], 0);
            m_arrMembers[21] = new BaseClasses.CMember(22, m_arrNodes[42], m_arrNodes[43], m_arrCrSc[21], 0);
            m_arrMembers[22] = new BaseClasses.CMember(23, m_arrNodes[44], m_arrNodes[45], m_arrCrSc[22], 0);
            m_arrMembers[23] = new BaseClasses.CMember(24, m_arrNodes[46], m_arrNodes[47], m_arrCrSc[23], 0);
            m_arrMembers[24] = new BaseClasses.CMember(25, m_arrNodes[48], m_arrNodes[49], m_arrCrSc[24], 0);

            //Sort by ID
            Array.Sort(m_arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values
            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };
            bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            // Pozn. Jednym z parametrov by malo byt pole ID uzlov v ktorych je zadefinovana tato podpora
            // objekt podpory bude len jeden a dotknute uzly budu vediet ze na ich podpora existuje a ake je konkretne ID jej nastaveni
            m_arrNSupports[0] = new BaseClasses.CNSupport(6, 1, m_arrNodes[0], bSupport1, 0);
            m_arrNSupports[1] = new BaseClasses.CNSupport(6, 2, m_arrNodes[2], bSupport2, 0);
            m_arrNSupports[2] = new BaseClasses.CNSupport(6, 3, m_arrNodes[5], bSupport3, 0);

            // Sort by ID
            Array.Sort(m_arrNSupports, new BaseClasses.CCompare_NSupportID());
        }
    }
}
