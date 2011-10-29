using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CENEX
{
    public class CTest5
    {


        public CNode[] arrNodes = new CNode[68];
        public CLine[] arrLines = new CLine[101];
        public CNSupport[] arrSupports = new CNSupport[2];
        public CNForce[] arrForces = new CNForce[35];

        public CTest5()
        {
            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes
            arrNodes[00] = new CNode(01, 000000, 0000, 50000, 0);
            arrNodes[01] = new CNode(02, 008126, 0000, 44646, 0);
            arrNodes[02] = new CNode(03, 011471, 0000, 50000, 0);
            arrNodes[03] = new CNode(04, 014160, 0000, 41020, 0);
            arrNodes[04] = new CNode(05, 019344, 0000, 50000, 0);
            arrNodes[05] = new CNode(06, 020317, 0000, 37607, 0);
            arrNodes[06] = new CNode(07, 026589, 0000, 34411, 0);
            arrNodes[07] = new CNode(08, 026906, 0000, 50000, 0);
            arrNodes[08] = new CNode(09, 032969, 0000, 31436, 0);
            arrNodes[09] = new CNode(10, 034192, 0000, 50000, 0);
            arrNodes[10] = new CNode(11, 039449, 0000, 28686, 0);
            arrNodes[11] = new CNode(12, 041234, 0000, 50000, 0);
            arrNodes[12] = new CNode(13, 046021, 0000, 26163, 0);
            arrNodes[13] = new CNode(14, 048061, 0000, 50000, 0);
            arrNodes[14] = new CNode(15, 052677, 0000, 23871, 0);
            arrNodes[15] = new CNode(16, 054697, 0000, 50000, 0);
            arrNodes[16] = new CNode(17, 059409, 0000, 21813, 0);
            arrNodes[17] = new CNode(18, 061167, 0000, 50000, 0);
            arrNodes[18] = new CNode(19, 066209, 0000, 19991, 0);
            arrNodes[19] = new CNode(20, 067492, 0000, 50000, 0);
            arrNodes[20] = new CNode(21, 073068, 0000, 18407, 0);
            arrNodes[21] = new CNode(22, 073691, 0000, 50000, 0);
            arrNodes[22] = new CNode(23, 079784, 0000, 50000, 0);
            arrNodes[23] = new CNode(24, 079979, 0000, 17064, 0);
            arrNodes[24] = new CNode(25, 085786, 0000, 50000, 0);
            arrNodes[25] = new CNode(26, 086931, 0000, 15963, 0);
            arrNodes[26] = new CNode(27, 091715, 0000, 50000, 0);
            arrNodes[27] = new CNode(28, 093919, 0000, 15105, 0);
            arrNodes[28] = new CNode(29, 097586, 0000, 50000, 0);
            arrNodes[29] = new CNode(30, 100931, 0000, 14491, 0);
            arrNodes[30] = new CNode(31, 103414, 0000, 50000, 0);
            arrNodes[31] = new CNode(32, 107961, 0000, 14123, 0);
            arrNodes[32] = new CNode(33, 109214, 0000, 50000, 0);
            arrNodes[33] = new CNode(34, 115000, 0000, 14000, 0);
            arrNodes[34] = new CNode(35, 115000, 0000, 50000, 0);
            arrNodes[35] = new CNode(36, 120786, 0000, 50000, 0);
            arrNodes[36] = new CNode(37, 122039, 0000, 14123, 0);
            arrNodes[37] = new CNode(38, 126586, 0000, 50000, 0);
            arrNodes[38] = new CNode(39, 129069, 0000, 14491, 0);
            arrNodes[39] = new CNode(40, 132414, 0000, 50000, 0);
            arrNodes[40] = new CNode(41, 136081, 0000, 15105, 0);
            arrNodes[41] = new CNode(42, 138285, 0000, 50000, 0);
            arrNodes[42] = new CNode(43, 143069, 0000, 15963, 0);
            arrNodes[43] = new CNode(44, 144214, 0000, 50000, 0);
            arrNodes[44] = new CNode(45, 150021, 0000, 17064, 0);
            arrNodes[45] = new CNode(46, 150216, 0000, 50000, 0);
            arrNodes[46] = new CNode(47, 156309, 0000, 50000, 0);
            arrNodes[47] = new CNode(48, 156932, 0000, 18407, 0);
            arrNodes[48] = new CNode(49, 162508, 0000, 50000, 0);
            arrNodes[49] = new CNode(50, 163791, 0000, 19991, 0);
            arrNodes[50] = new CNode(51, 168833, 0000, 50000, 0);
            arrNodes[51] = new CNode(52, 170591, 0000, 21813, 0);
            arrNodes[52] = new CNode(53, 175303, 0000, 50000, 0);
            arrNodes[53] = new CNode(54, 177323, 0000, 23871, 0);
            arrNodes[54] = new CNode(55, 181939, 0000, 50000, 0);
            arrNodes[55] = new CNode(56, 183979, 0000, 26163, 0);
            arrNodes[56] = new CNode(57, 188766, 0000, 50000, 0);
            arrNodes[57] = new CNode(58, 190551, 0000, 28686, 0);
            arrNodes[58] = new CNode(59, 195808, 0000, 50000, 0);
            arrNodes[59] = new CNode(60, 197031, 0000, 31436, 0);
            arrNodes[60] = new CNode(61, 203094, 0000, 50000, 0);
            arrNodes[61] = new CNode(62, 203411, 0000, 34411, 0);
            arrNodes[62] = new CNode(63, 209683, 0000, 37607, 0);
            arrNodes[63] = new CNode(64, 210656, 0000, 50000, 0);
            arrNodes[64] = new CNode(65, 215840, 0000, 41020, 0);
            arrNodes[65] = new CNode(66, 218529, 0000, 50000, 0);
            arrNodes[66] = new CNode(67, 221874, 0000, 44646, 0);
            arrNodes[67] = new CNode(68, 230000, 0000, 50000, 0);

            // Setridit pole podle ID
            Array.Sort(arrNodes, new CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            // Lines
            arrLines[000] = new CLine(001, arrNodes[00], arrNodes[01], 0);
            arrLines[001] = new CLine(002, arrNodes[00], arrNodes[02], 0);
            arrLines[002] = new CLine(003, arrNodes[02], arrNodes[01], 0);
            arrLines[003] = new CLine(004, arrNodes[01], arrNodes[03], 0);
            arrLines[004] = new CLine(005, arrNodes[02], arrNodes[04], 0);
            arrLines[005] = new CLine(006, arrNodes[04], arrNodes[03], 0);
            arrLines[006] = new CLine(007, arrNodes[03], arrNodes[05], 0);
            arrLines[007] = new CLine(008, arrNodes[04], arrNodes[07], 0);
            arrLines[008] = new CLine(009, arrNodes[05], arrNodes[06], 0);
            arrLines[009] = new CLine(010, arrNodes[07], arrNodes[05], 0);
            arrLines[010] = new CLine(011, arrNodes[06], arrNodes[08], 0);
            arrLines[011] = new CLine(012, arrNodes[06], arrNodes[09], 0);
            arrLines[012] = new CLine(013, arrNodes[07], arrNodes[09], 0);
            arrLines[013] = new CLine(014, arrNodes[08], arrNodes[10], 0);
            arrLines[014] = new CLine(015, arrNodes[08], arrNodes[11], 0);
            arrLines[015] = new CLine(016, arrNodes[09], arrNodes[11], 0);
            arrLines[016] = new CLine(017, arrNodes[10], arrNodes[12], 0);
            arrLines[017] = new CLine(018, arrNodes[10], arrNodes[13], 0);
            arrLines[018] = new CLine(019, arrNodes[11], arrNodes[13], 0);
            arrLines[019] = new CLine(020, arrNodes[12], arrNodes[14], 0);
            arrLines[020] = new CLine(021, arrNodes[12], arrNodes[15], 0);
            arrLines[021] = new CLine(022, arrNodes[13], arrNodes[15], 0);
            arrLines[022] = new CLine(023, arrNodes[14], arrNodes[16], 0);
            arrLines[023] = new CLine(024, arrNodes[14], arrNodes[17], 0);
            arrLines[024] = new CLine(025, arrNodes[15], arrNodes[17], 0);
            arrLines[025] = new CLine(026, arrNodes[16], arrNodes[18], 0);
            arrLines[026] = new CLine(027, arrNodes[16], arrNodes[19], 0);
            arrLines[027] = new CLine(028, arrNodes[17], arrNodes[19], 0);
            arrLines[028] = new CLine(029, arrNodes[18], arrNodes[20], 0);
            arrLines[029] = new CLine(030, arrNodes[18], arrNodes[21], 0);
            arrLines[030] = new CLine(031, arrNodes[19], arrNodes[21], 0);
            arrLines[031] = new CLine(032, arrNodes[20], arrNodes[22], 0);
            arrLines[032] = new CLine(033, arrNodes[20], arrNodes[23], 0);
            arrLines[033] = new CLine(034, arrNodes[21], arrNodes[22], 0);
            arrLines[034] = new CLine(035, arrNodes[22], arrNodes[24], 0);
            arrLines[035] = new CLine(036, arrNodes[23], arrNodes[24], 0);
            arrLines[036] = new CLine(037, arrNodes[23], arrNodes[25], 0);
            arrLines[037] = new CLine(038, arrNodes[24], arrNodes[26], 0);
            arrLines[038] = new CLine(039, arrNodes[25], arrNodes[26], 0);
            arrLines[039] = new CLine(040, arrNodes[25], arrNodes[27], 0);
            arrLines[040] = new CLine(041, arrNodes[26], arrNodes[28], 0);
            arrLines[041] = new CLine(042, arrNodes[27], arrNodes[28], 0);
            arrLines[042] = new CLine(043, arrNodes[27], arrNodes[29], 0);
            arrLines[043] = new CLine(044, arrNodes[28], arrNodes[30], 0);
            arrLines[044] = new CLine(045, arrNodes[29], arrNodes[30], 0);
            arrLines[045] = new CLine(046, arrNodes[29], arrNodes[31], 0);
            arrLines[046] = new CLine(047, arrNodes[30], arrNodes[32], 0);
            arrLines[047] = new CLine(048, arrNodes[31], arrNodes[32], 0);
            arrLines[048] = new CLine(049, arrNodes[31], arrNodes[33], 0);
            arrLines[049] = new CLine(050, arrNodes[32], arrNodes[34], 0);
            arrLines[050] = new CLine(051, arrNodes[33], arrNodes[34], 0);
            arrLines[051] = new CLine(052, arrNodes[34], arrNodes[35], 0);
            arrLines[052] = new CLine(053, arrNodes[33], arrNodes[36], 0);
            arrLines[053] = new CLine(054, arrNodes[35], arrNodes[36], 0);
            arrLines[054] = new CLine(055, arrNodes[35], arrNodes[37], 0);
            arrLines[055] = new CLine(056, arrNodes[36], arrNodes[38], 0);
            arrLines[056] = new CLine(057, arrNodes[37], arrNodes[38], 0);
            arrLines[057] = new CLine(058, arrNodes[37], arrNodes[39], 0);
            arrLines[058] = new CLine(059, arrNodes[38], arrNodes[40], 0);
            arrLines[059] = new CLine(060, arrNodes[39], arrNodes[40], 0);
            arrLines[060] = new CLine(061, arrNodes[39], arrNodes[41], 0);
            arrLines[061] = new CLine(062, arrNodes[40], arrNodes[42], 0);
            arrLines[062] = new CLine(063, arrNodes[41], arrNodes[42], 0);
            arrLines[063] = new CLine(064, arrNodes[41], arrNodes[43], 0);
            arrLines[064] = new CLine(065, arrNodes[42], arrNodes[44], 0);
            arrLines[065] = new CLine(066, arrNodes[43], arrNodes[44], 0);
            arrLines[066] = new CLine(067, arrNodes[43], arrNodes[45], 0);
            arrLines[067] = new CLine(068, arrNodes[45], arrNodes[46], 0);
            arrLines[068] = new CLine(069, arrNodes[44], arrNodes[47], 0);
            arrLines[069] = new CLine(070, arrNodes[45], arrNodes[47], 0);
            arrLines[070] = new CLine(071, arrNodes[46], arrNodes[48], 0);
            arrLines[071] = new CLine(072, arrNodes[46], arrNodes[49], 0);
            arrLines[072] = new CLine(073, arrNodes[47], arrNodes[49], 0);
            arrLines[073] = new CLine(074, arrNodes[48], arrNodes[50], 0);
            arrLines[074] = new CLine(075, arrNodes[48], arrNodes[51], 0);
            arrLines[075] = new CLine(076, arrNodes[49], arrNodes[51], 0);
            arrLines[076] = new CLine(077, arrNodes[50], arrNodes[52], 0);
            arrLines[077] = new CLine(078, arrNodes[50], arrNodes[53], 0);
            arrLines[078] = new CLine(079, arrNodes[51], arrNodes[53], 0);
            arrLines[079] = new CLine(080, arrNodes[52], arrNodes[54], 0);
            arrLines[080] = new CLine(081, arrNodes[52], arrNodes[55], 0);
            arrLines[081] = new CLine(082, arrNodes[53], arrNodes[55], 0);
            arrLines[082] = new CLine(083, arrNodes[54], arrNodes[56], 0);
            arrLines[083] = new CLine(084, arrNodes[54], arrNodes[57], 0);
            arrLines[084] = new CLine(085, arrNodes[55], arrNodes[57], 0);
            arrLines[085] = new CLine(086, arrNodes[56], arrNodes[58], 0);
            arrLines[086] = new CLine(087, arrNodes[56], arrNodes[59], 0);
            arrLines[087] = new CLine(088, arrNodes[57], arrNodes[59], 0);
            arrLines[088] = new CLine(089, arrNodes[58], arrNodes[60], 0);
            arrLines[089] = new CLine(090, arrNodes[58], arrNodes[61], 0);
            arrLines[090] = new CLine(091, arrNodes[59], arrNodes[61], 0);
            arrLines[091] = new CLine(092, arrNodes[62], arrNodes[60], 0);
            arrLines[092] = new CLine(093, arrNodes[61], arrNodes[62], 0);
            arrLines[093] = new CLine(094, arrNodes[60], arrNodes[63], 0);
            arrLines[094] = new CLine(095, arrNodes[62], arrNodes[64], 0);
            arrLines[095] = new CLine(096, arrNodes[64], arrNodes[63], 0);
            arrLines[096] = new CLine(097, arrNodes[63], arrNodes[65], 0);
            arrLines[097] = new CLine(098, arrNodes[64], arrNodes[66], 0);
            arrLines[098] = new CLine(099, arrNodes[66], arrNodes[65], 0);
            arrLines[099] = new CLine(100, arrNodes[65], arrNodes[67], 0);
            arrLines[100] = new CLine(101, arrNodes[66], arrNodes[67], 0);



            // Setridit pole podle ID
            Array.Sort(arrLines, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, false, false };
            bool[] bSupport2 = { false, false, true, false, false, false };

            // Create Support Objects
            arrSupports[0] = new CNSupport(1, arrNodes[00], bSupport1, 0);
            arrSupports[1] = new CNSupport(2, arrNodes[67], bSupport2, 0);

            // Setridit pole podle ID
            Array.Sort(arrSupports, new CCompare_NSupportID());

            // Member Releases / hinges - fill values

            // Set values
            bool[] bMembRelase1 = { false, false, false, false, true, false };

            // Create Release / Hinge Objects
            arrLines[02].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[05].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[09].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[11].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[14].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[17].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[20].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[23].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[26].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[29].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[31].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[35].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[38].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[41].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[44].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[47].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[50].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[53].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[56].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[59].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[62].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[65].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[69].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[71].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[74].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[77].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[80].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[83].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[86].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[89].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[91].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[95].m_iMR = new CMembRelease(0, bMembRelase1, 0);
            arrLines[98].m_iMR = new CMembRelease(0, bMembRelase1, 0);

            // Nodal Forces - fill values
            arrForces[00] = new CNForce(arrNodes[00], -00.0f, 0.0f, -020.0f, 0);
            arrForces[01] = new CNForce(arrNodes[02], -00.0f, 0.0f, -050.0f, 0);
            arrForces[02] = new CNForce(arrNodes[04], -00.0f, 0.0f, -050.0f, 0);
            arrForces[03] = new CNForce(arrNodes[07], -00.0f, 0.0f, -050.0f, 0);
            arrForces[04] = new CNForce(arrNodes[09], -00.0f, 0.0f, -050.0f, 0);
            arrForces[05] = new CNForce(arrNodes[11], -00.0f, 0.0f, -050.0f, 0);
            arrForces[06] = new CNForce(arrNodes[13], -00.0f, 0.0f, -050.0f, 0);
            arrForces[07] = new CNForce(arrNodes[15], -00.0f, 0.0f, -050.0f, 0);
            arrForces[08] = new CNForce(arrNodes[17], -00.0f, 0.0f, -050.0f, 0);
            arrForces[09] = new CNForce(arrNodes[19], -00.0f, 0.0f, -050.0f, 0);
            arrForces[10] = new CNForce(arrNodes[21], -00.0f, 0.0f, -050.0f, 0);
            arrForces[11] = new CNForce(arrNodes[22], -00.0f, 0.0f, -050.0f, 0);
            arrForces[12] = new CNForce(arrNodes[24], -00.0f, 0.0f, -050.0f, 0);
            arrForces[13] = new CNForce(arrNodes[26], -00.0f, 0.0f, -050.0f, 0);
            arrForces[14] = new CNForce(arrNodes[28], -00.0f, 0.0f, -050.0f, 0);
            arrForces[15] = new CNForce(arrNodes[30], -00.0f, 0.0f, -050.0f, 0);
            arrForces[16] = new CNForce(arrNodes[32], -00.0f, 0.0f, -050.0f, 0);
            arrForces[17] = new CNForce(arrNodes[34], -00.0f, 0.0f, -050.0f, 0);
            arrForces[18] = new CNForce(arrNodes[35], -00.0f, 0.0f, -050.0f, 0);
            arrForces[19] = new CNForce(arrNodes[37], -00.0f, 0.0f, -050.0f, 0);
            arrForces[20] = new CNForce(arrNodes[39], -00.0f, 0.0f, -050.0f, 0);
            arrForces[21] = new CNForce(arrNodes[41], -00.0f, 0.0f, -050.0f, 0);
            arrForces[22] = new CNForce(arrNodes[43], -00.0f, 0.0f, -050.0f, 0);
            arrForces[23] = new CNForce(arrNodes[45], -00.0f, 0.0f, -050.0f, 0);
            arrForces[24] = new CNForce(arrNodes[46], -00.0f, 0.0f, -050.0f, 0);
            arrForces[25] = new CNForce(arrNodes[48], -00.0f, 0.0f, -050.0f, 0);
            arrForces[26] = new CNForce(arrNodes[50], -00.0f, 0.0f, -050.0f, 0);
            arrForces[27] = new CNForce(arrNodes[52], -00.0f, 0.0f, -050.0f, 0);
            arrForces[28] = new CNForce(arrNodes[54], -00.0f, 0.0f, -050.0f, 0);
            arrForces[29] = new CNForce(arrNodes[56], -00.0f, 0.0f, -050.0f, 0);
            arrForces[30] = new CNForce(arrNodes[58], -00.0f, 0.0f, -050.0f, 0);
            arrForces[31] = new CNForce(arrNodes[60], -00.0f, 0.0f, -050.0f, 0);
            arrForces[32] = new CNForce(arrNodes[63], -00.0f, 0.0f, -050.0f, 0);
            arrForces[33] = new CNForce(arrNodes[65], -00.0f, 0.0f, -050.0f, 0);
            arrForces[34] = new CNForce(arrNodes[67], -00.0f, 0.0f, -020.0f, 0);
        }
    }
}