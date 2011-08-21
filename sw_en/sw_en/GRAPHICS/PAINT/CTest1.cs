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
        public CMat[] arrMat = new CMat[1];
        public CCrSc[] arrCrSc = new CCrSc[1];
        public CNSupport[] arrSupports = new CNSupport[3];
        public CNForce[] arrForces = new CNForce[3];

        // Materials - vsetky by mali byt v poli arrMat hoci to mozu byt ine objekty
        CMat objMat1 = new CMat();

        // Cross Sections - vsetky by mali byt v poli arrCrSc, hoci sa jedna o objekty inych tried, neviem ci sa to da tak urobit

         public CCrSc_0_00 objCrScSolid = new CCrSc_0_00(100);
        // public CCrSc_0_01 objCrScSolid = new CCrSc_0_01(100);
        // public CCrSc_0_02 objCrScSolid = new CCrSc_0_02(50);
        // public CCrSc_0_03 objCrSc = new CCrSc_0_03(100, 50);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(100);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(100, 40);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(-100, -40, 100,0, 0,50);
        public CCrSc_0_05 objCrScWF = new CCrSc_0_05(100, 50);
        // public CCrSc_0_22 objCrScHollow = new CCrSc_0_22(200, 10);
        // public CCrSc_0_23 objCrScHollow = new CCrSc_0_23(200, 100, 5);
        // public CCrSc_0_24 objCrScHollow = new CCrSc_0_24(100, 10);
        // public CCrSc_0_25 objCrScHollow = new CCrSc_HL(200, 100, 10, 5);
        // public CCrSc_0_26 objCrScHollow = new CCrSc_0_26(200,10);
        // public CCrSc_0_27 objCrScHollow = new CCrSc_0_27(200, 10);
        public CCrSc_0_28 objCrScHollow = new CCrSc_0_28(200, 10);
        // public CCrSc_0_50 objCrScSolid = new CCrSc_0_50(200,100,10,5);
        // public CCrSc_0_50 objCrScSolid = new CCrSc_0_50(200, 100, 150, 10, 15, 5, 70);
        // public CCrSc_0_52 objCrScSolid = new CCrSc_0_52(200, 100, 10, 5, 30);
        // public CCrSc_0_52 objCrScSolid = new CCrSc_0_52(200, 100, 150, 10, 15, 5, 30, 70);
        // public CCrSc_0_54 objCrScSolid = new CCrSc_0_54(200, 100, 10, 5,30,50);
        // public CCrSc_0_56 objCrScSolid = new CCrSc_0_56(200, 100, 10, 5,130);
        // public CCrSc_0_56 objCrScSolid = new CCrSc_0_56(200, 100, 30, 10, 5, 20, 130);
        // public CCrSc_0_58 objCrScSolid = new CCrSc_0_58(200, 100, 10, 5);
        // public CCrSc_0_58 objCrScSolid = new CCrSc_0_58(200, 100, 150, 10, 15, 5, 30, 70);
        // public CCrSc_0_60 objCrScSolid = new CCrSc_0_60(200,100,10);
        // public CCrSc_0_61 objCrScSolid = new CCrSc_0_61(100,10);

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

            // Fill Data of Materials Array
            arrMat[0]= objMat1;

            // Set CrSc Materials
            /*
            arrCrSc[0].m_Mat.m_fE = arrMat[0].m_fE;
            arrCrSc[0].m_Mat.m_fG = arrMat[0].m_fG;
            arrCrSc[0].m_Mat.m_fAlpha_T = arrMat[0].m_fAlpha_T;
            arrCrSc[0].m_Mat.m_fnu = arrMat[0].m_fnu;
             */
            objCrScHollow.m_Mat.m_fE = arrMat[0].m_fE;
            objCrScHollow.m_Mat.m_fG = arrMat[0].m_fG;
            objCrScHollow.m_Mat.m_fAlpha_T = arrMat[0].m_fAlpha_T;
            objCrScHollow.m_Mat.m_fnu = arrMat[0].m_fnu;

            // Fill Data of Cross-sections Array
            arrCrSc[0] = objCrScHollow;

            // Lines Automatic Generation
            // Lines List - Lines Array

            arrLines[0] = new CLine(1, arrNodes[0], arrNodes[1], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[1] = new CLine(2, arrNodes[1], arrNodes[2], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[2] = new CLine(3, arrNodes[0], arrNodes[3], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[3] = new CLine(4, arrNodes[1], arrNodes[4], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[4] = new CLine(5, arrNodes[2], arrNodes[5], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[5] = new CLine(6, arrNodes[3], arrNodes[4], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[6] = new CLine(7, arrNodes[4], arrNodes[5], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[7] = new CLine(8, arrNodes[1], arrNodes[3], objCrScHollow /*arrCrSc[0]*/, 0);
            arrLines[8] = new CLine(9, arrNodes[1], arrNodes[5], objCrScHollow /*arrCrSc[0]*/, 0);

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
