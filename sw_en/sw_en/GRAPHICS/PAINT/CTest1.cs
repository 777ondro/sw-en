using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CENEX
{
    public class CTest1
    {
        public CNode[] arrNodes = new CNode[6];
        public CLine[] arrLines = new CLine[9];
        public CNSupport[] arrSupports = new CNSupport[3];
        public CNForce[] arrForces = new CNForce[3];
        // public CCrSc_I objCrSc = new CCrSc_I(200,100,10,5);
        // public CCrSc_I objCrSc = new CCrSc_I(200, 100, 150, 10,15,5,70);
        // public CCrSc_U objCrSc = new CCrSc_U(200, 100, 10, 5, 30);
        // public CCrSc_U objCrSc = new CCrSc_U(200, 100, 150, 10, 15, 5, 30, 70);
        // public CCrSc_HL objCrSc = new CCrSc_HL(200, 100, 10, 5);
        // public CCrSc_L objCrSc = new CCrSc_L(200, 100, 10, 5,30,50);
        // public CCrSc_T objCrSc = new CCrSc_T(200, 100, 10, 5,130);
        public CCrSc_T objCrSc = new CCrSc_T(200, 100, 30, 10, 5, 20, 130);
        // public CCrSc_Z objCrSc = new CCrSc_Z(200, 100, 10, 5);
        // public CCrSc_Z objCrSc = new CCrSc_Z(200, 100, 150, 10, 15, 5, 30, 70);

        public CTest1()
        {
            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            arrNodes[0] = new CNode(1, 500, 0, 2500, 0);
            arrNodes[1] = new CNode(2, 2500, 0, 2500, 0);
            arrNodes[2] = new CNode(3, 5500, 0, 2500, 0);
            arrNodes[3] = new CNode(4, 500, 0, 500, 0);
            arrNodes[4] = new CNode(5, 2500, 0, 500, 0);
            arrNodes[5] = new CNode(6, 5500, 0, 500, 0);

            // Setridit pole podle ID
            Array.Sort(arrNodes, new CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            arrLines[0] = new CLine(1, arrNodes[0], arrNodes[1], 0);
            arrLines[1] = new CLine(2, arrNodes[1], arrNodes[2], 0);
            arrLines[2] = new CLine(3, arrNodes[0], arrNodes[3], 0);
            arrLines[3] = new CLine(4, arrNodes[1], arrNodes[4], 0);
            arrLines[4] = new CLine(5, arrNodes[2], arrNodes[5], 0);
            arrLines[5] = new CLine(6, arrNodes[3], arrNodes[4], 0);
            arrLines[6] = new CLine(7, arrNodes[4], arrNodes[5], 0);
            arrLines[7] = new CLine(8, arrNodes[1], arrNodes[3], 0);
            arrLines[8] = new CLine(9, arrNodes[1], arrNodes[5], 0);

            // Setridit pole podle ID
            Array.Sort(arrLines, new CCompare_LineID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };
            bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            arrSupports[0] = new CNSupport(1, arrNodes[0], bSupport1, 0);
            arrSupports[1] = new CNSupport(2, arrNodes[2], bSupport2, 0);
            arrSupports[2] = new CNSupport(3, arrNodes[5], bSupport3, 0);

            // Setridit pole podle ID
            Array.Sort(arrSupports, new CCompare_SupportID());

            // Member Releases / hinges - fill values

            // Set values
            //bool[] bMembRelase1 = { true, false, false, false, false, false };
            //bool[] bMembRelase2 = { false, false, true, false, false, false };
            //bool[] bMembRelase3 = { false, false, false, false, true, false };
            bool[] bMembRelase4 = { true, false, true, false, true, false };


            // Create Release / Hinge Objects
            arrLines[7].m_iMR = new CMembRelease(0, bMembRelase4, 0);
            arrLines[8].m_iMR = new CMembRelease(0, bMembRelase4, 0);

            // Nodal Forces - fill values

            arrForces[0] = new CNForce(arrNodes[1], 40.0f, 0.0f, -50.0f, 0);
            arrForces[1] = new CNForce(arrNodes[4], -60.0f, 0.0f, 0.0f, 0);
            arrForces[2] = new CNForce(arrNodes[5], 0.0f, 0.0f, 80.0f, 0);
        }
    }
}
