using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CENEX
{
    public class CTest3
    {

        public CNode[] arrNodes = new CNode[18];
        public CLine[] arrLines = new CLine[23];
        public CNSupport[] arrSupports = new CNSupport[4];
        public CNForce[] arrForces = new CNForce[3];

                public CTest3()
        {
            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // 1-level
            arrNodes[00] = new CNode(01,  500, 0,  00, 0);
            arrNodes[01] = new CNode(02, 1000, 0,  00, 0);
            arrNodes[02] = new CNode(03, 1500, 0,  00, 0);
            arrNodes[03] = new CNode(04, 1750, 0,  00, 0);
            // 2-level
            arrNodes[04] = new CNode(05,  350, 0, 300, 0);
            arrNodes[05] = new CNode(06,  500, 0, 300, 0);
            arrNodes[06] = new CNode(07, 1000, 0, 300, 0);
            arrNodes[07] = new CNode(08, 1500, 0, 300, 0);
            arrNodes[08] = new CNode(09, 1800, 0, 300, 0);
            // 3-level
            arrNodes[09] = new CNode(10,  200, 0, 600, 0);
            arrNodes[10] = new CNode(11,  500, 0, 600, 0);
            arrNodes[11] = new CNode(12, 1000, 0, 600, 0);
            arrNodes[12] = new CNode(13, 1500, 0, 600, 0);
            arrNodes[13] = new CNode(14, 1950, 0, 600, 0);
            // 4-level
            arrNodes[14] = new CNode(15,   50, 0, 900, 0);
            arrNodes[15] = new CNode(16,  500, 0, 900, 0);
            arrNodes[16] = new CNode(17, 1000, 0, 900, 0);
            arrNodes[17] = new CNode(18, 1500, 0, 900, 0);

            // Setridit pole podle ID
            Array.Sort(arrNodes, new CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            // 1-level - horizontal beams
            arrLines[00] = new CLine(01, arrNodes[00], arrNodes[01], 0);
            arrLines[01] = new CLine(02, arrNodes[01], arrNodes[02], 0);
            arrLines[02] = new CLine(03, arrNodes[02], arrNodes[03], 0);
            // 1-columns
            arrLines[03] = new CLine(04, arrNodes[00], arrNodes[04], 0);
            arrLines[04] = new CLine(05, arrNodes[00], arrNodes[05], 0);
            arrLines[05] = new CLine(06, arrNodes[01], arrNodes[06], 0);
            arrLines[06] = new CLine(07, arrNodes[02], arrNodes[07], 0);
            // 2-level - horizontal beams
            arrLines[07] = new CLine(08, arrNodes[04], arrNodes[05], 0);
            arrLines[08] = new CLine(09, arrNodes[05], arrNodes[06], 0);
            arrLines[09] = new CLine(10, arrNodes[06], arrNodes[07], 0);
            arrLines[10] = new CLine(11, arrNodes[07], arrNodes[08], 0);
            // 2-columns
            arrLines[11] = new CLine(12, arrNodes[04], arrNodes[09], 0);
            arrLines[12] = new CLine(13, arrNodes[05], arrNodes[10], 0);
            arrLines[13] = new CLine(14, arrNodes[06], arrNodes[11], 0);
            arrLines[14] = new CLine(15, arrNodes[07], arrNodes[12], 0);
            // 3-level - horizontal beams
            arrLines[15] = new CLine(16, arrNodes[09], arrNodes[10], 0);
            arrLines[16] = new CLine(17, arrNodes[10], arrNodes[11], 0);
            arrLines[17] = new CLine(18, arrNodes[11], arrNodes[12], 0);
            arrLines[18] = new CLine(19, arrNodes[12], arrNodes[13], 0);
            // 3-columns
            arrLines[19] = new CLine(20, arrNodes[09], arrNodes[14], 0);
            arrLines[20] = new CLine(21, arrNodes[10], arrNodes[15], 0);
            arrLines[21] = new CLine(22, arrNodes[11], arrNodes[16], 0);
            arrLines[22] = new CLine(23, arrNodes[12], arrNodes[17], 0);

            // Setridit pole podle ID
            Array.Sort(arrLines, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            //bool[] bSupport2 = { true, false, true, false, false, false };
            //bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            arrSupports[0] = new CNSupport(1, arrNodes[14], bSupport1, 0);
            arrSupports[1] = new CNSupport(2, arrNodes[15], bSupport1, 0);
            arrSupports[2] = new CNSupport(3, arrNodes[16], bSupport1, 0);
            arrSupports[3] = new CNSupport(4, arrNodes[17], bSupport1, 0);

            // Setridit pole podle ID
            Array.Sort(arrSupports, new CCompare_SupportID());

            // Member Releases / hinges - fill values

            // Set values
            //bool[] bMembRelase1 = { true, false, false, false, false, false };
            //bool[] bMembRelase2 = { false, false, true, false, false, false };
            //bool[] bMembRelase3 = { false, false, false, false, true, false };
            bool[] bMembRelase4 = { false, false, false, false, true, false };


            // Create Release / Hinge Objects
            arrLines[03].m_iMR = new CMembRelease(0, bMembRelase4, 0);
            arrLines[11].m_iMR = new CMembRelease(0, bMembRelase4, 0);
            arrLines[19].m_iMR = new CMembRelease(0, bMembRelase4, 0);

            // Nodal Forces - fill values

            arrForces[0] = new CNForce(arrNodes[3],  -40.0f, 0.0f, -050.0f, 0);
            arrForces[1] = new CNForce(arrNodes[8],  -30.0f, 0.0f, -100.0f, 0);
            arrForces[2] = new CNForce(arrNodes[13], -20.0f, 0.0f, -100.0f, 0);
        }
    }
}
