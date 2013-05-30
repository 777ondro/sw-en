using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_05 : CExample
    {
        public BaseClasses.CModel m_TopoModel = new BaseClasses.CModel();
        /*
                public CNode[] m_TopoModel.m_arrNodes = new CNode[68];
                public CMember[] m_TopoModel.m_arrMembers = new CMember[101];
                public CNSupport[] arrSupports = new CNSupport[2];
                //public CNForce[] arrForces = new CNForce[35];
                int eNDOF = (int)ENDOF.e3DEnv;
         */

        public CExample_3D_05()
        {
            m_TopoModel.m_arrNodes = new BaseClasses.CNode[68];
            m_TopoModel.m_arrMembers = new CMember[101];
            m_TopoModel.m_arrMat = new CMat_00[1];
            m_TopoModel.m_arrCrSc = new CRSC.CCrSc[1];
            m_TopoModel.m_arrNSupports = new BaseClasses.CNSupport[2];
            //m_TopoModel.m_arrNLoads = new BaseClasses.CNLoad[35];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_TopoModel.m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_TopoModel.m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.6f, 0.4f);

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes
            m_TopoModel.m_arrNodes[00] = new CNode(01, 000000, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[01] = new CNode(02, 008126, 0000, 44646, 0);
            m_TopoModel.m_arrNodes[02] = new CNode(03, 011471, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[03] = new CNode(04, 014160, 0000, 41020, 0);
            m_TopoModel.m_arrNodes[04] = new CNode(05, 019344, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[05] = new CNode(06, 020317, 0000, 37607, 0);
            m_TopoModel.m_arrNodes[06] = new CNode(07, 026589, 0000, 34411, 0);
            m_TopoModel.m_arrNodes[07] = new CNode(08, 026906, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[08] = new CNode(09, 032969, 0000, 31436, 0);
            m_TopoModel.m_arrNodes[09] = new CNode(10, 034192, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[10] = new CNode(11, 039449, 0000, 28686, 0);
            m_TopoModel.m_arrNodes[11] = new CNode(12, 041234, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[12] = new CNode(13, 046021, 0000, 26163, 0);
            m_TopoModel.m_arrNodes[13] = new CNode(14, 048061, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[14] = new CNode(15, 052677, 0000, 23871, 0);
            m_TopoModel.m_arrNodes[15] = new CNode(16, 054697, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[16] = new CNode(17, 059409, 0000, 21813, 0);
            m_TopoModel.m_arrNodes[17] = new CNode(18, 061167, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[18] = new CNode(19, 066209, 0000, 19991, 0);
            m_TopoModel.m_arrNodes[19] = new CNode(20, 067492, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[20] = new CNode(21, 073068, 0000, 18407, 0);
            m_TopoModel.m_arrNodes[21] = new CNode(22, 073691, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[22] = new CNode(23, 079784, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[23] = new CNode(24, 079979, 0000, 17064, 0);
            m_TopoModel.m_arrNodes[24] = new CNode(25, 085786, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[25] = new CNode(26, 086931, 0000, 15963, 0);
            m_TopoModel.m_arrNodes[26] = new CNode(27, 091715, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[27] = new CNode(28, 093919, 0000, 15105, 0);
            m_TopoModel.m_arrNodes[28] = new CNode(29, 097586, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[29] = new CNode(30, 100931, 0000, 14491, 0);
            m_TopoModel.m_arrNodes[30] = new CNode(31, 103414, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[31] = new CNode(32, 107961, 0000, 14123, 0);
            m_TopoModel.m_arrNodes[32] = new CNode(33, 109214, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[33] = new CNode(34, 115000, 0000, 14000, 0);
            m_TopoModel.m_arrNodes[34] = new CNode(35, 115000, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[35] = new CNode(36, 120786, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[36] = new CNode(37, 122039, 0000, 14123, 0);
            m_TopoModel.m_arrNodes[37] = new CNode(38, 126586, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[38] = new CNode(39, 129069, 0000, 14491, 0);
            m_TopoModel.m_arrNodes[39] = new CNode(40, 132414, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[40] = new CNode(41, 136081, 0000, 15105, 0);
            m_TopoModel.m_arrNodes[41] = new CNode(42, 138285, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[42] = new CNode(43, 143069, 0000, 15963, 0);
            m_TopoModel.m_arrNodes[43] = new CNode(44, 144214, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[44] = new CNode(45, 150021, 0000, 17064, 0);
            m_TopoModel.m_arrNodes[45] = new CNode(46, 150216, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[46] = new CNode(47, 156309, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[47] = new CNode(48, 156932, 0000, 18407, 0);
            m_TopoModel.m_arrNodes[48] = new CNode(49, 162508, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[49] = new CNode(50, 163791, 0000, 19991, 0);
            m_TopoModel.m_arrNodes[50] = new CNode(51, 168833, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[51] = new CNode(52, 170591, 0000, 21813, 0);
            m_TopoModel.m_arrNodes[52] = new CNode(53, 175303, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[53] = new CNode(54, 177323, 0000, 23871, 0);
            m_TopoModel.m_arrNodes[54] = new CNode(55, 181939, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[55] = new CNode(56, 183979, 0000, 26163, 0);
            m_TopoModel.m_arrNodes[56] = new CNode(57, 188766, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[57] = new CNode(58, 190551, 0000, 28686, 0);
            m_TopoModel.m_arrNodes[58] = new CNode(59, 195808, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[59] = new CNode(60, 197031, 0000, 31436, 0);
            m_TopoModel.m_arrNodes[60] = new CNode(61, 203094, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[61] = new CNode(62, 203411, 0000, 34411, 0);
            m_TopoModel.m_arrNodes[62] = new CNode(63, 209683, 0000, 37607, 0);
            m_TopoModel.m_arrNodes[63] = new CNode(64, 210656, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[64] = new CNode(65, 215840, 0000, 41020, 0);
            m_TopoModel.m_arrNodes[65] = new CNode(66, 218529, 0000, 50000, 0);
            m_TopoModel.m_arrNodes[66] = new CNode(67, 221874, 0000, 44646, 0);
            m_TopoModel.m_arrNodes[67] = new CNode(68, 230000, 0000, 50000, 0);

            // Convert coordinates to meters
            foreach (CNode node in m_TopoModel.m_arrNodes)
            {
                node.FCoord_X /= 1000;
                node.FCoord_Y /= 1000;
                node.FCoord_Z /= 1000;
            }

            // Setridit pole podle ID
            Array.Sort(m_TopoModel.m_arrNodes, new CCompare_NodeID());

            // Members Automatic Generation
            // Members List - Members Array

            // Members
            m_TopoModel.m_arrMembers[000] = new CMember(001, m_TopoModel.m_arrNodes[00], m_TopoModel.m_arrNodes[01], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[001] = new CMember(002, m_TopoModel.m_arrNodes[00], m_TopoModel.m_arrNodes[02], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[002] = new CMember(003, m_TopoModel.m_arrNodes[02], m_TopoModel.m_arrNodes[01], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[003] = new CMember(004, m_TopoModel.m_arrNodes[01], m_TopoModel.m_arrNodes[03], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[004] = new CMember(005, m_TopoModel.m_arrNodes[02], m_TopoModel.m_arrNodes[04], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[005] = new CMember(006, m_TopoModel.m_arrNodes[04], m_TopoModel.m_arrNodes[03], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[006] = new CMember(007, m_TopoModel.m_arrNodes[03], m_TopoModel.m_arrNodes[05], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[007] = new CMember(008, m_TopoModel.m_arrNodes[04], m_TopoModel.m_arrNodes[07], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[008] = new CMember(009, m_TopoModel.m_arrNodes[05], m_TopoModel.m_arrNodes[06], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[009] = new CMember(010, m_TopoModel.m_arrNodes[07], m_TopoModel.m_arrNodes[05], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[010] = new CMember(011, m_TopoModel.m_arrNodes[06], m_TopoModel.m_arrNodes[08], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[011] = new CMember(012, m_TopoModel.m_arrNodes[06], m_TopoModel.m_arrNodes[09], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[012] = new CMember(013, m_TopoModel.m_arrNodes[07], m_TopoModel.m_arrNodes[09], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[013] = new CMember(014, m_TopoModel.m_arrNodes[08], m_TopoModel.m_arrNodes[10], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[014] = new CMember(015, m_TopoModel.m_arrNodes[08], m_TopoModel.m_arrNodes[11], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[015] = new CMember(016, m_TopoModel.m_arrNodes[09], m_TopoModel.m_arrNodes[11], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[016] = new CMember(017, m_TopoModel.m_arrNodes[10], m_TopoModel.m_arrNodes[12], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[017] = new CMember(018, m_TopoModel.m_arrNodes[10], m_TopoModel.m_arrNodes[13], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[018] = new CMember(019, m_TopoModel.m_arrNodes[11], m_TopoModel.m_arrNodes[13], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[019] = new CMember(020, m_TopoModel.m_arrNodes[12], m_TopoModel.m_arrNodes[14], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[020] = new CMember(021, m_TopoModel.m_arrNodes[12], m_TopoModel.m_arrNodes[15], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[021] = new CMember(022, m_TopoModel.m_arrNodes[13], m_TopoModel.m_arrNodes[15], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[022] = new CMember(023, m_TopoModel.m_arrNodes[14], m_TopoModel.m_arrNodes[16], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[023] = new CMember(024, m_TopoModel.m_arrNodes[14], m_TopoModel.m_arrNodes[17], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[024] = new CMember(025, m_TopoModel.m_arrNodes[15], m_TopoModel.m_arrNodes[17], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[025] = new CMember(026, m_TopoModel.m_arrNodes[16], m_TopoModel.m_arrNodes[18], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[026] = new CMember(027, m_TopoModel.m_arrNodes[16], m_TopoModel.m_arrNodes[19], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[027] = new CMember(028, m_TopoModel.m_arrNodes[17], m_TopoModel.m_arrNodes[19], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[028] = new CMember(029, m_TopoModel.m_arrNodes[18], m_TopoModel.m_arrNodes[20], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[029] = new CMember(030, m_TopoModel.m_arrNodes[18], m_TopoModel.m_arrNodes[21], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[030] = new CMember(031, m_TopoModel.m_arrNodes[19], m_TopoModel.m_arrNodes[21], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[031] = new CMember(032, m_TopoModel.m_arrNodes[20], m_TopoModel.m_arrNodes[22], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[032] = new CMember(033, m_TopoModel.m_arrNodes[20], m_TopoModel.m_arrNodes[23], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[033] = new CMember(034, m_TopoModel.m_arrNodes[21], m_TopoModel.m_arrNodes[22], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[034] = new CMember(035, m_TopoModel.m_arrNodes[22], m_TopoModel.m_arrNodes[24], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[035] = new CMember(036, m_TopoModel.m_arrNodes[23], m_TopoModel.m_arrNodes[24], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[036] = new CMember(037, m_TopoModel.m_arrNodes[23], m_TopoModel.m_arrNodes[25], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[037] = new CMember(038, m_TopoModel.m_arrNodes[24], m_TopoModel.m_arrNodes[26], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[038] = new CMember(039, m_TopoModel.m_arrNodes[25], m_TopoModel.m_arrNodes[26], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[039] = new CMember(040, m_TopoModel.m_arrNodes[25], m_TopoModel.m_arrNodes[27], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[040] = new CMember(041, m_TopoModel.m_arrNodes[26], m_TopoModel.m_arrNodes[28], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[041] = new CMember(042, m_TopoModel.m_arrNodes[27], m_TopoModel.m_arrNodes[28], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[042] = new CMember(043, m_TopoModel.m_arrNodes[27], m_TopoModel.m_arrNodes[29], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[043] = new CMember(044, m_TopoModel.m_arrNodes[28], m_TopoModel.m_arrNodes[30], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[044] = new CMember(045, m_TopoModel.m_arrNodes[29], m_TopoModel.m_arrNodes[30], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[045] = new CMember(046, m_TopoModel.m_arrNodes[29], m_TopoModel.m_arrNodes[31], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[046] = new CMember(047, m_TopoModel.m_arrNodes[30], m_TopoModel.m_arrNodes[32], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[047] = new CMember(048, m_TopoModel.m_arrNodes[31], m_TopoModel.m_arrNodes[32], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[048] = new CMember(049, m_TopoModel.m_arrNodes[31], m_TopoModel.m_arrNodes[33], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[049] = new CMember(050, m_TopoModel.m_arrNodes[32], m_TopoModel.m_arrNodes[34], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[050] = new CMember(051, m_TopoModel.m_arrNodes[33], m_TopoModel.m_arrNodes[34], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[051] = new CMember(052, m_TopoModel.m_arrNodes[34], m_TopoModel.m_arrNodes[35], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[052] = new CMember(053, m_TopoModel.m_arrNodes[33], m_TopoModel.m_arrNodes[36], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[053] = new CMember(054, m_TopoModel.m_arrNodes[35], m_TopoModel.m_arrNodes[36], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[054] = new CMember(055, m_TopoModel.m_arrNodes[35], m_TopoModel.m_arrNodes[37], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[055] = new CMember(056, m_TopoModel.m_arrNodes[36], m_TopoModel.m_arrNodes[38], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[056] = new CMember(057, m_TopoModel.m_arrNodes[37], m_TopoModel.m_arrNodes[38], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[057] = new CMember(058, m_TopoModel.m_arrNodes[37], m_TopoModel.m_arrNodes[39], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[058] = new CMember(059, m_TopoModel.m_arrNodes[38], m_TopoModel.m_arrNodes[40], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[059] = new CMember(060, m_TopoModel.m_arrNodes[39], m_TopoModel.m_arrNodes[40], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[060] = new CMember(061, m_TopoModel.m_arrNodes[39], m_TopoModel.m_arrNodes[41], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[061] = new CMember(062, m_TopoModel.m_arrNodes[40], m_TopoModel.m_arrNodes[42], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[062] = new CMember(063, m_TopoModel.m_arrNodes[41], m_TopoModel.m_arrNodes[42], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[063] = new CMember(064, m_TopoModel.m_arrNodes[41], m_TopoModel.m_arrNodes[43], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[064] = new CMember(065, m_TopoModel.m_arrNodes[42], m_TopoModel.m_arrNodes[44], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[065] = new CMember(066, m_TopoModel.m_arrNodes[43], m_TopoModel.m_arrNodes[44], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[066] = new CMember(067, m_TopoModel.m_arrNodes[43], m_TopoModel.m_arrNodes[45], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[067] = new CMember(068, m_TopoModel.m_arrNodes[45], m_TopoModel.m_arrNodes[46], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[068] = new CMember(069, m_TopoModel.m_arrNodes[44], m_TopoModel.m_arrNodes[47], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[069] = new CMember(070, m_TopoModel.m_arrNodes[45], m_TopoModel.m_arrNodes[47], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[070] = new CMember(071, m_TopoModel.m_arrNodes[46], m_TopoModel.m_arrNodes[48], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[071] = new CMember(072, m_TopoModel.m_arrNodes[46], m_TopoModel.m_arrNodes[49], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[072] = new CMember(073, m_TopoModel.m_arrNodes[47], m_TopoModel.m_arrNodes[49], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[073] = new CMember(074, m_TopoModel.m_arrNodes[48], m_TopoModel.m_arrNodes[50], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[074] = new CMember(075, m_TopoModel.m_arrNodes[48], m_TopoModel.m_arrNodes[51], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[075] = new CMember(076, m_TopoModel.m_arrNodes[49], m_TopoModel.m_arrNodes[51], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[076] = new CMember(077, m_TopoModel.m_arrNodes[50], m_TopoModel.m_arrNodes[52], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[077] = new CMember(078, m_TopoModel.m_arrNodes[50], m_TopoModel.m_arrNodes[53], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[078] = new CMember(079, m_TopoModel.m_arrNodes[51], m_TopoModel.m_arrNodes[53], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[079] = new CMember(080, m_TopoModel.m_arrNodes[52], m_TopoModel.m_arrNodes[54], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[080] = new CMember(081, m_TopoModel.m_arrNodes[52], m_TopoModel.m_arrNodes[55], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[081] = new CMember(082, m_TopoModel.m_arrNodes[53], m_TopoModel.m_arrNodes[55], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[082] = new CMember(083, m_TopoModel.m_arrNodes[54], m_TopoModel.m_arrNodes[56], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[083] = new CMember(084, m_TopoModel.m_arrNodes[54], m_TopoModel.m_arrNodes[57], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[084] = new CMember(085, m_TopoModel.m_arrNodes[55], m_TopoModel.m_arrNodes[57], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[085] = new CMember(086, m_TopoModel.m_arrNodes[56], m_TopoModel.m_arrNodes[58], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[086] = new CMember(087, m_TopoModel.m_arrNodes[56], m_TopoModel.m_arrNodes[59], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[087] = new CMember(088, m_TopoModel.m_arrNodes[57], m_TopoModel.m_arrNodes[59], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[088] = new CMember(089, m_TopoModel.m_arrNodes[58], m_TopoModel.m_arrNodes[60], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[089] = new CMember(090, m_TopoModel.m_arrNodes[58], m_TopoModel.m_arrNodes[61], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[090] = new CMember(091, m_TopoModel.m_arrNodes[59], m_TopoModel.m_arrNodes[61], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[091] = new CMember(092, m_TopoModel.m_arrNodes[62], m_TopoModel.m_arrNodes[60], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[092] = new CMember(093, m_TopoModel.m_arrNodes[61], m_TopoModel.m_arrNodes[62], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[093] = new CMember(094, m_TopoModel.m_arrNodes[60], m_TopoModel.m_arrNodes[63], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[094] = new CMember(095, m_TopoModel.m_arrNodes[62], m_TopoModel.m_arrNodes[64], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[095] = new CMember(096, m_TopoModel.m_arrNodes[64], m_TopoModel.m_arrNodes[63], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[096] = new CMember(097, m_TopoModel.m_arrNodes[63], m_TopoModel.m_arrNodes[65], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[097] = new CMember(098, m_TopoModel.m_arrNodes[64], m_TopoModel.m_arrNodes[66], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[098] = new CMember(099, m_TopoModel.m_arrNodes[66], m_TopoModel.m_arrNodes[65], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[099] = new CMember(100, m_TopoModel.m_arrNodes[65], m_TopoModel.m_arrNodes[67], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[100] = new CMember(101, m_TopoModel.m_arrNodes[66], m_TopoModel.m_arrNodes[67], m_TopoModel.m_arrCrSc[0], 0);



            // Setridit pole podle ID
            //Array.Sort(m_TopoModel.m_arrMembers, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, false, false };
            bool[] bSupport2 = { false, false, true, false, false, false };

            // Create Support Objects
            m_TopoModel.m_arrNSupports[0] = new CNSupport(6, 1, m_TopoModel.m_arrNodes[00], bSupport1, 0);
            m_TopoModel.m_arrNSupports[1] = new CNSupport(6, 2, m_TopoModel.m_arrNodes[67], bSupport2, 0);

            // Setridit pole podle ID
            Array.Sort(m_TopoModel.m_arrNSupports, new CCompare_NSupportID());

            // Member Releases / hinges - fill values

            // Set values
            bool?[] bMembRelase1 = { false, false, false, false, true, false };

            // Create Release / Hinge Objects
            m_TopoModel.m_arrMembers[02].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[02].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[05].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[05].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[09].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[09].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[11].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[11].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[14].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[14].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[17].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[17].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[20].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[20].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[23].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[23].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[26].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[26].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[29].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[29].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[31].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[31].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[35].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[35].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[38].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[38].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[41].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[41].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[44].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[44].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[47].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[47].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[50].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[50].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[53].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[53].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[56].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[56].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[59].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[59].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[62].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[62].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[65].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[65].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[69].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[69].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[71].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[71].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[74].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[74].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[77].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[77].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[80].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[80].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[83].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[83].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[86].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[86].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[89].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[89].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[91].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[91].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[95].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[95].NodeStart, bMembRelase1, 0);
            m_TopoModel.m_arrMembers[98].CnRelease1 = new CNRelease(6, m_TopoModel.m_arrMembers[98].NodeStart, bMembRelase1, 0);

            // Nodal Forces - fill values
            //arrForces[00] = new CNForce(m_TopoModel.m_arrNodes[00], -00.0f, 0.0f, -020.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[01] = new CNForce(m_TopoModel.m_arrNodes[02], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[02] = new CNForce(m_TopoModel.m_arrNodes[04], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[03] = new CNForce(m_TopoModel.m_arrNodes[07], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[04] = new CNForce(m_TopoModel.m_arrNodes[09], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[05] = new CNForce(m_TopoModel.m_arrNodes[11], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[06] = new CNForce(m_TopoModel.m_arrNodes[13], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[07] = new CNForce(m_TopoModel.m_arrNodes[15], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[08] = new CNForce(m_TopoModel.m_arrNodes[17], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[09] = new CNForce(m_TopoModel.m_arrNodes[19], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[10] = new CNForce(m_TopoModel.m_arrNodes[21], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[11] = new CNForce(m_TopoModel.m_arrNodes[22], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[12] = new CNForce(m_TopoModel.m_arrNodes[24], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[13] = new CNForce(m_TopoModel.m_arrNodes[26], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[14] = new CNForce(m_TopoModel.m_arrNodes[28], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[15] = new CNForce(m_TopoModel.m_arrNodes[30], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[16] = new CNForce(m_TopoModel.m_arrNodes[32], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[17] = new CNForce(m_TopoModel.m_arrNodes[34], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[18] = new CNForce(m_TopoModel.m_arrNodes[35], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[19] = new CNForce(m_TopoModel.m_arrNodes[37], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[20] = new CNForce(m_TopoModel.m_arrNodes[39], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[21] = new CNForce(m_TopoModel.m_arrNodes[41], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[22] = new CNForce(m_TopoModel.m_arrNodes[43], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[23] = new CNForce(m_TopoModel.m_arrNodes[45], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[24] = new CNForce(m_TopoModel.m_arrNodes[46], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[25] = new CNForce(m_TopoModel.m_arrNodes[48], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[26] = new CNForce(m_TopoModel.m_arrNodes[50], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[27] = new CNForce(m_TopoModel.m_arrNodes[52], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[28] = new CNForce(m_TopoModel.m_arrNodes[54], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[29] = new CNForce(m_TopoModel.m_arrNodes[56], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[30] = new CNForce(m_TopoModel.m_arrNodes[58], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[31] = new CNForce(m_TopoModel.m_arrNodes[60], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[32] = new CNForce(m_TopoModel.m_arrNodes[63], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[33] = new CNForce(m_TopoModel.m_arrNodes[65], -00.0f, 0.0f, -050.0f, m_TopoModel.m_arrCrSc[0], 0);
            //arrForces[34] = new CNForce(m_TopoModel.m_arrNodes[67], -00.0f, 0.0f, -020.0f, m_TopoModel.m_arrCrSc[0], 0);


        }


    }
}
