using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CENEX
{
    class CTest6
    {


        public CNode[] arrNodes = new CNode[62];
        public CLine[] arrLines = new CLine[102];
        public CNSupport[] arrSupports = new CNSupport[19];
        public CNForce[] arrForces = new CNForce[1];

        public CTest6()
        {
            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Nodes
            arrNodes[0] = new CNode(1, 0, -20000, 3000, 0);
            arrNodes[1] = new CNode(2, 0, -20000, 6000, 0);
            arrNodes[2] = new CNode(3, 0, -20000, 9000, 0);
            arrNodes[3] = new CNode(4, 0, -15000, 3000, 0);
            arrNodes[4] = new CNode(5, 0, -15000, 6000, 0);
            arrNodes[5] = new CNode(6, 0, -15000, 9000, 0);
            arrNodes[6] = new CNode(7, 0, -10000, 3000, 0);
            arrNodes[7] = new CNode(8, 0, -10000, 9000, 0);
            arrNodes[8] = new CNode(9, 0, -5000, 3000, 0);
            arrNodes[9] = new CNode(10, 0, -5000, 9000, 0);
            arrNodes[10] = new CNode(11, 0, 0, 3000, 0);
            arrNodes[11] = new CNode(12, 0, 0, 9000, 0);
            arrNodes[12] = new CNode(13, 3000, -15000, 2739, 0);
            arrNodes[13] = new CNode(14, 3000, -10000, 2739, 0);
            arrNodes[14] = new CNode(15, 3000, -5000, 2739, 0);
            arrNodes[15] = new CNode(16, 3000, 0, 2739, 0);
            arrNodes[16] = new CNode(17, 6250, -20000, 2454, 0);
            arrNodes[17] = new CNode(18, 6250, -20000, 6000, 0);
            arrNodes[18] = new CNode(19, 6250, -20000, 9000, 0);
            arrNodes[19] = new CNode(20, 6250, -15000, 2454, 0);
            arrNodes[20] = new CNode(21, 6250, -15000, 6000, 0);
            arrNodes[21] = new CNode(22, 6250, -15000, 9000, 0);
            arrNodes[22] = new CNode(23, 6250, -10000, 2454, 0);
            arrNodes[23] = new CNode(24, 6250, -5000, 2454, 0);
            arrNodes[24] = new CNode(25, 6250, 0, 2454, 0);
            arrNodes[25] = new CNode(26, 6250, 0, 9000, 0);
            arrNodes[26] = new CNode(27, 12500, -20000, 1906, 0);
            arrNodes[27] = new CNode(28, 12500, -20000, 6000, 0);
            arrNodes[28] = new CNode(29, 12500, -20000, 9000, 0);
            arrNodes[29] = new CNode(30, 12500, -15000, 1906, 0);
            arrNodes[30] = new CNode(31, 12500, -15000, 6000, 0);
            arrNodes[31] = new CNode(32, 12500, -15000, 9000, 0);
            arrNodes[32] = new CNode(33, 12500, -10000, 1906, 0);
            arrNodes[33] = new CNode(34, 12500, -5000, 1906, 0);
            arrNodes[34] = new CNode(35, 12500, 0, 1906, 0);
            arrNodes[35] = new CNode(36, 12500, 0, 9000, 0);
            arrNodes[36] = new CNode(37, 18750, -20000, 2454, 0);
            arrNodes[37] = new CNode(38, 18750, -20000, 6000, 0);
            arrNodes[38] = new CNode(39, 18750, -20000, 9000, 0);
            arrNodes[39] = new CNode(40, 18750, -15000, 2454, 0);
            arrNodes[40] = new CNode(41, 18750, -15000, 6000, 0);
            arrNodes[41] = new CNode(42, 18750, -15000, 9000, 0);
            arrNodes[42] = new CNode(43, 18750, -10000, 2454, 0);
            arrNodes[43] = new CNode(44, 18750, -5000, 2454, 0);
            arrNodes[44] = new CNode(45, 18750, 0, 2454, 0);
            arrNodes[45] = new CNode(46, 18750, 0, 9000, 0);
            arrNodes[46] = new CNode(47, 22000, -15000, 2739, 0);
            arrNodes[47] = new CNode(48, 22000, -10000, 2739, 0);
            arrNodes[48] = new CNode(49, 22000, -5000, 2739, 0);
            arrNodes[49] = new CNode(50, 22000, 0, 2739, 0);
            arrNodes[50] = new CNode(51, 25000, -20000, 3000, 0);
            arrNodes[51] = new CNode(52, 25000, -20000, 6000, 0);
            arrNodes[52] = new CNode(53, 25000, -20000, 9000, 0);
            arrNodes[53] = new CNode(54, 25000, -15000, 3000, 0);
            arrNodes[54] = new CNode(55, 25000, -15000, 6000, 0);
            arrNodes[55] = new CNode(56, 25000, -15000, 9000, 0);
            arrNodes[56] = new CNode(57, 25000, -10000, 3000, 0);
            arrNodes[57] = new CNode(58, 25000, -10000, 9000, 0);
            arrNodes[58] = new CNode(59, 25000, -5000, 3000, 0);
            arrNodes[59] = new CNode(60, 25000, -5000, 9000, 0);
            arrNodes[60] = new CNode(61, 25000, 0, 3000, 0);
            arrNodes[61] = new CNode(62, 25000, 0, 9000, 0);


            // Setridit pole podle ID
            Array.Sort(arrNodes, new CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            // Lines
            arrLines[0] = new CLine(1, arrNodes[1], arrNodes[0], 0);
            arrLines[1] = new CLine(2, arrNodes[2], arrNodes[1], 0);
            arrLines[2] = new CLine(3, arrNodes[3], arrNodes[0], 0);
            arrLines[3] = new CLine(4, arrNodes[4], arrNodes[1], 0);
            arrLines[4] = new CLine(5, arrNodes[4], arrNodes[3], 0);
            arrLines[5] = new CLine(6, arrNodes[5], arrNodes[4], 0);
            arrLines[6] = new CLine(7, arrNodes[6], arrNodes[3], 0);
            arrLines[7] = new CLine(8, arrNodes[7], arrNodes[6], 0);
            arrLines[8] = new CLine(9, arrNodes[8], arrNodes[6], 0);
            arrLines[9] = new CLine(10, arrNodes[7], arrNodes[8], 0);
            arrLines[10] = new CLine(11, arrNodes[9], arrNodes[6], 0);
            arrLines[11] = new CLine(12, arrNodes[9], arrNodes[8], 0);
            arrLines[12] = new CLine(13, arrNodes[10], arrNodes[8], 0);
            arrLines[13] = new CLine(14, arrNodes[11], arrNodes[10], 0);
            arrLines[14] = new CLine(15, arrNodes[3], arrNodes[12], 0);
            arrLines[15] = new CLine(16, arrNodes[6], arrNodes[13], 0);
            arrLines[16] = new CLine(17, arrNodes[8], arrNodes[14], 0);
            arrLines[17] = new CLine(18, arrNodes[10], arrNodes[15], 0);
            arrLines[18] = new CLine(19, arrNodes[0], arrNodes[16], 0);
            arrLines[19] = new CLine(20, arrNodes[1], arrNodes[17], 0);
            arrLines[20] = new CLine(21, arrNodes[4], arrNodes[20], 0);
            arrLines[21] = new CLine(22, arrNodes[23], arrNodes[6], 0);
            arrLines[22] = new CLine(23, arrNodes[8], arrNodes[22], 0);
            arrLines[23] = new CLine(24, arrNodes[12], arrNodes[19], 0);
            arrLines[24] = new CLine(25, arrNodes[13], arrNodes[22], 0);
            arrLines[25] = new CLine(26, arrNodes[14], arrNodes[23], 0);
            arrLines[26] = new CLine(27, arrNodes[15], arrNodes[24], 0);
            arrLines[27] = new CLine(28, arrNodes[17], arrNodes[16], 0);
            arrLines[28] = new CLine(29, arrNodes[18], arrNodes[17], 0);
            arrLines[29] = new CLine(30, arrNodes[19], arrNodes[16], 0);
            arrLines[30] = new CLine(31, arrNodes[20], arrNodes[17], 0);
            arrLines[31] = new CLine(32, arrNodes[20], arrNodes[19], 0);
            arrLines[32] = new CLine(33, arrNodes[21], arrNodes[20], 0);
            arrLines[33] = new CLine(34, arrNodes[22], arrNodes[19], 0);
            arrLines[34] = new CLine(35, arrNodes[23], arrNodes[22], 0);
            arrLines[35] = new CLine(36, arrNodes[24], arrNodes[23], 0);
            arrLines[36] = new CLine(37, arrNodes[25], arrNodes[24], 0);
            arrLines[37] = new CLine(38, arrNodes[16], arrNodes[26], 0);
            arrLines[38] = new CLine(39, arrNodes[17], arrNodes[27], 0);
            arrLines[39] = new CLine(40, arrNodes[19], arrNodes[29], 0);
            arrLines[40] = new CLine(41, arrNodes[20], arrNodes[30], 0);
            arrLines[41] = new CLine(42, arrNodes[22], arrNodes[32], 0);
            arrLines[42] = new CLine(43, arrNodes[33], arrNodes[22], 0);
            arrLines[43] = new CLine(44, arrNodes[23], arrNodes[32], 0);
            arrLines[44] = new CLine(45, arrNodes[23], arrNodes[33], 0);
            arrLines[45] = new CLine(46, arrNodes[24], arrNodes[34], 0);
            arrLines[46] = new CLine(47, arrNodes[27], arrNodes[26], 0);
            arrLines[47] = new CLine(48, arrNodes[28], arrNodes[27], 0);
            arrLines[48] = new CLine(49, arrNodes[29], arrNodes[26], 0);
            arrLines[49] = new CLine(50, arrNodes[30], arrNodes[27], 0);
            arrLines[50] = new CLine(51, arrNodes[30], arrNodes[29], 0);
            arrLines[51] = new CLine(52, arrNodes[31], arrNodes[30], 0);
            arrLines[52] = new CLine(53, arrNodes[32], arrNodes[29], 0);
            arrLines[53] = new CLine(54, arrNodes[33], arrNodes[32], 0);
            arrLines[54] = new CLine(55, arrNodes[34], arrNodes[33], 0);
            arrLines[55] = new CLine(56, arrNodes[35], arrNodes[34], 0);
            arrLines[56] = new CLine(57, arrNodes[26], arrNodes[36], 0);
            arrLines[57] = new CLine(58, arrNodes[27], arrNodes[37], 0);
            arrLines[58] = new CLine(59, arrNodes[29], arrNodes[39], 0);
            arrLines[59] = new CLine(60, arrNodes[30], arrNodes[40], 0);
            arrLines[60] = new CLine(61, arrNodes[32], arrNodes[42], 0);
            arrLines[61] = new CLine(62, arrNodes[43], arrNodes[32], 0);
            arrLines[62] = new CLine(63, arrNodes[33], arrNodes[42], 0);
            arrLines[63] = new CLine(64, arrNodes[33], arrNodes[43], 0);
            arrLines[64] = new CLine(65, arrNodes[34], arrNodes[44], 0);
            arrLines[65] = new CLine(66, arrNodes[37], arrNodes[36], 0);
            arrLines[66] = new CLine(67, arrNodes[38], arrNodes[37], 0);
            arrLines[67] = new CLine(68, arrNodes[39], arrNodes[36], 0);
            arrLines[68] = new CLine(69, arrNodes[40], arrNodes[37], 0);
            arrLines[69] = new CLine(70, arrNodes[40], arrNodes[39], 0);
            arrLines[70] = new CLine(71, arrNodes[41], arrNodes[40], 0);
            arrLines[71] = new CLine(72, arrNodes[42], arrNodes[39], 0);
            arrLines[72] = new CLine(73, arrNodes[43], arrNodes[42], 0);
            arrLines[73] = new CLine(74, arrNodes[44], arrNodes[43], 0);
            arrLines[74] = new CLine(75, arrNodes[45], arrNodes[44], 0);
            arrLines[75] = new CLine(76, arrNodes[39], arrNodes[46], 0);
            arrLines[76] = new CLine(77, arrNodes[42], arrNodes[47], 0);
            arrLines[77] = new CLine(78, arrNodes[43], arrNodes[48], 0);
            arrLines[78] = new CLine(79, arrNodes[44], arrNodes[49], 0);
            arrLines[79] = new CLine(80, arrNodes[36], arrNodes[50], 0);
            arrLines[80] = new CLine(81, arrNodes[37], arrNodes[51], 0);
            arrLines[81] = new CLine(82, arrNodes[40], arrNodes[54], 0);
            arrLines[82] = new CLine(83, arrNodes[43], arrNodes[56], 0);
            arrLines[83] = new CLine(84, arrNodes[58], arrNodes[42], 0);
            arrLines[84] = new CLine(85, arrNodes[46], arrNodes[53], 0);
            arrLines[85] = new CLine(86, arrNodes[47], arrNodes[56], 0);
            arrLines[86] = new CLine(87, arrNodes[48], arrNodes[58], 0);
            arrLines[87] = new CLine(88, arrNodes[49], arrNodes[60], 0);
            arrLines[88] = new CLine(89, arrNodes[51], arrNodes[50], 0);
            arrLines[89] = new CLine(90, arrNodes[52], arrNodes[51], 0);
            arrLines[90] = new CLine(91, arrNodes[53], arrNodes[50], 0);
            arrLines[91] = new CLine(92, arrNodes[54], arrNodes[51], 0);
            arrLines[92] = new CLine(93, arrNodes[54], arrNodes[53], 0);
            arrLines[93] = new CLine(94, arrNodes[55], arrNodes[54], 0);
            arrLines[94] = new CLine(95, arrNodes[56], arrNodes[53], 0);
            arrLines[95] = new CLine(96, arrNodes[57], arrNodes[56], 0);
            arrLines[96] = new CLine(97, arrNodes[58], arrNodes[56], 0);
            arrLines[97] = new CLine(98, arrNodes[58], arrNodes[57], 0);
            arrLines[98] = new CLine(99, arrNodes[56], arrNodes[59], 0);
            arrLines[99] = new CLine(100, arrNodes[59], arrNodes[58], 0);
            arrLines[100] = new CLine(101, arrNodes[60], arrNodes[58], 0);
            arrLines[101] = new CLine(102, arrNodes[61], arrNodes[60], 0);



            // Setridit pole podle ID
            Array.Sort(arrLines, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, false, false };
            bool[] bSupport2 = { false, false, true, false, false, false };

            // Create Support Objects
            arrSupports[0] = new CNSupport(1, arrNodes[11], bSupport1, 0);
            arrSupports[1] = new CNSupport(2, arrNodes[61], bSupport1, 0);
            arrSupports[2] = new CNSupport(3, arrNodes[9], bSupport1, 0);
            arrSupports[3] = new CNSupport(4, arrNodes[59], bSupport1, 0);
            arrSupports[4] = new CNSupport(5, arrNodes[7], bSupport1, 0);
            arrSupports[5] = new CNSupport(6, arrNodes[57], bSupport1, 0);
            arrSupports[6] = new CNSupport(7, arrNodes[5], bSupport1, 0);
            arrSupports[7] = new CNSupport(8, arrNodes[21], bSupport1, 0);
            arrSupports[8] = new CNSupport(9, arrNodes[31], bSupport1, 0);
            arrSupports[9] = new CNSupport(10, arrNodes[41], bSupport1, 0);
            arrSupports[10] = new CNSupport(11, arrNodes[55], bSupport1, 0);
            arrSupports[11] = new CNSupport(12, arrNodes[2], bSupport1, 0);
            arrSupports[12] = new CNSupport(13, arrNodes[18], bSupport1, 0);
            arrSupports[13] = new CNSupport(14, arrNodes[28], bSupport1, 0);
            arrSupports[14] = new CNSupport(15, arrNodes[38], bSupport1, 0);
            arrSupports[15] = new CNSupport(16, arrNodes[52], bSupport1, 0);
            arrSupports[16] = new CNSupport(17, arrNodes[25], bSupport1, 0);
            arrSupports[17] = new CNSupport(18, arrNodes[35], bSupport1, 0);
            arrSupports[18] = new CNSupport(19, arrNodes[45], bSupport1, 0);


            // Setridit pole podle ID
            Array.Sort(arrSupports, new CCompare_NSupportID());

            // Member Releases / hinges - fill values

            // Set values
            bool[] bMembRelase1 = { false, false, false, false, true, false };

            // Create Release / Hinge Objects
            arrLines[00].m_iMR = new CMembRelease(0, bMembRelase1, 0);

            // Nodal Forces - fill values
            arrForces[00] = new CNForce(arrNodes[00], -00.0f, 0.0f, -020.0f, 0);

        }
    }
}