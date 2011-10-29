using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BaseClasses;

namespace CENEX
{
    public class CTest1
    {
        public BaseClasses.CNode[] arrNodes = new BaseClasses.CNode[6];
        public CMember[] arrMembers = new CMember[9];
        public CMat_00[] arrMat = new CMat_00[5];
        public CRSC.CCrSc[] arrCrSc = new CRSC.CCrSc[3];
        public BaseClasses.CNSupport[] arrSupports = new BaseClasses.CNSupport[3];
        public BaseClasses.CNLoad[] arrForces = new BaseClasses.CNLoad[3];

        // Cross Sections - vsetky by mali byt v poli arrCrSc, hoci sa jedna o objekty inych tried, neviem ci sa to da tak urobit

        // Temporary objecs for testing

        // public CCrSc_0_00 objCrScSolid = new CCrSc_0_00(100);
        // public CCrSc_0_01 objCrScSolid = new CCrSc_0_01(100);
        // public CCrSc_0_02 objCrScSolid = new CCrSc_0_02(50);
        // public CCrSc_0_03 objCrSc = new CCrSc_0_03(100, 50);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(100);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(100, 40);
        // public CCrSc_0_04 objCrSc = new CCrSc_0_04(-100, -40, 100,0, 0,50);
        public CCrSc_0_05 objCrScWF = new CCrSc_0_05(100, 50);
        // public CCrSc_0_20 objCrScHollow = new CCrSc_0_20(200, 10);
        // public CCrSc_0_22 objCrScHollow = new CCrSc_0_22(200, 10);
        // public CCrSc_0_23 objCrScHollow = new CCrSc_0_23(200, 100, 5);
        // public CCrSc_0_24 objCrScHollow = new CCrSc_0_24(100, 10);
        // public CCrSc_0_25 objCrScHollow = new CCrSc_HL(200, 100, 10, 5);
        // public CCrSc_0_26 objCrScHollow = new CCrSc_0_26(200,10);
        // public CCrSc_0_27 objCrScHollow = new CCrSc_0_27(200, 10);
        // public CCrSc_0_28 objCrScHollow = new CCrSc_0_28(200, 10);
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
        // public CCrSc_3_00 objCrScSolid = new CCrSc_3_00(200,90,11.3f,7.5f,7.5f,4.5f,159.1f);
        // public CCrSc_3_02 objCrScSolid = new CCrSc_3_02(300, 100, 16, 10, 10, 8, 232, 27);
        // public CCrSc_3_03 objCrScSolid = new CCrSc_3_03(150, 10, 15, 10, 50);
        public CCrSc_3_04 objCrScSolid = new CCrSc_3_04(250, 150, 20, 15, 20, 50, 75);
        // public CCrSc_3_05 objCrScHollow = new CCrSc_3_05(200,10);
        // public CCrSc_3_06 objCrScHollow = new CCrSc_3_06(200, 100, 20);
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(0, 200, 100, 10, 30); // Both radii, coincident centres
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(1, 200, 100, 10, 5, 30); // Both radii, incoincident centres
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(2, 500, 300, 20, 50); // Outside radius = 0
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(3, 500, 300, 30); // Outside radius = 0, coincident centres
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(0, 400, 150, 20); // Both radii, coincident centres
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(2, 500, 300, 20); // Outside radius = 0
        public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(3, 500, 300, 30); // Inside radius = 0, coincident centres
        // public CCrSc_3_07 objCrScHollow = new CCrSc_3_07(5, 400, 150, 20); // No radii, Outside radius = 0, Inside radius = 0 

        public CTest1()
        {
            // Materials
            // Fill Data of Materials Array

            arrMat[0] = new CMat_00(); // Vytvarat priamo pre konkretne typy materialov (uzivatelsky, ocel, beton, drevo, hlinik)
            arrMat[1] = new CMat_02_00();
            arrMat[2] = new CMat_03_00();
            arrMat[3] = new CMat_05_00();
            arrMat[4] = new CMat_09_00();

            // CrSc
            // Vytvarat len ODKAZY na objekty "ref", aby sa zbytocne nevytvarali lokalne kopie

            objCrScHollow.m_Mat = arrMat[0]; // Temporary

            // Fill Data of Cross-sections Array

            arrCrSc[0] = new CRSC.CCrSc();
            arrCrSc[1] = new CRSC.CCrSc();
            arrCrSc[2] = new CRSC.CCrSc();

            //arrCrSc[0] = new CRSC.CCrSc_3_07(3, 500, 300, 30);
            //arrCrSc[1] = new CRSC.CCrSc_0_05(100, 50);
            //arrCrSc[2] = new CRSC.CCrSc_3_04(250, 150, 20, 15, 20, 50, 75);

            arrCrSc[0].m_Mat = arrMat[0];
            arrCrSc[1].m_Mat = arrMat[1];
            arrCrSc[2].m_Mat = arrMat[4];

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            arrNodes[0] = new BaseClasses.CNode(1, 500, 0, 2500, 0);
            arrNodes[1] = new BaseClasses.CNode(2, 2500, 0, 2500, 0);
            arrNodes[2] = new BaseClasses.CNode(3, 5500, 0, 2500, 0);
            arrNodes[3] = new BaseClasses.CNode(4, 500, 0, 500, 0);
            arrNodes[4] = new BaseClasses.CNode(5, 2500, 0, 500, 0);
            arrNodes[5] = new BaseClasses.CNode(6, 5500, 0, 500, 0);

            // Setridit pole podle ID
            Array.Sort(arrNodes, new BaseClasses.CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            arrMembers[0] = new BaseClasses.CMember(1, arrNodes[0], arrNodes[1], arrCrSc[0], 0);
            arrMembers[1] = new BaseClasses.CMember(2, arrNodes[1], arrNodes[2], arrCrSc[0], 0);
            arrMembers[2] = new BaseClasses.CMember(3, arrNodes[0], arrNodes[3], arrCrSc[1], 0);
            arrMembers[3] = new BaseClasses.CMember(4, arrNodes[1], arrNodes[4], arrCrSc[0], 0);
            arrMembers[4] = new BaseClasses.CMember(5, arrNodes[2], arrNodes[5], arrCrSc[0], 0);
            arrMembers[5] = new BaseClasses.CMember(6, arrNodes[3], arrNodes[4], arrCrSc[2], 0);
            arrMembers[6] = new BaseClasses.CMember(7, arrNodes[4], arrNodes[5], arrCrSc[0], 0);
            arrMembers[7] = new BaseClasses.CMember(8, arrNodes[1], arrNodes[3], arrCrSc[0], 0);
            arrMembers[8] = new BaseClasses.CMember(9, arrNodes[1], arrNodes[5], arrCrSc[1], 0);

            // Setridit pole podle ID
            Array.Sort(arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values

            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };
            bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            arrSupports[0] = new BaseClasses.CNSupport(1, arrNodes[0], bSupport1, 0);
            arrSupports[1] = new BaseClasses.CNSupport(2, arrNodes[2], bSupport2, 0);
            arrSupports[2] = new BaseClasses.CNSupport(3, arrNodes[5], bSupport3, 0);

            // Setridit pole podle ID
            Array.Sort(arrSupports, new BaseClasses.CCompare_NSupportID());

            // Member Releases / hinges - fill values

            // Set values
            //bool[] bMembRelase1 = { true, false, false, false, false, false };
            //bool[] bMembRelase2 = { false, false, true, false, false, false };
            //bool[] bMembRelase3 = { false, false, false, false, true, false };
            bool [] bMembRelase4 = { true, false, true, false, true, false };


            // Create Release / Hinge Objects
            arrMembers[7].CnRelease1 = new BaseClasses.CNRelease(bMembRelase4, 0);
            arrMembers[8].CnRelease2 = new BaseClasses.CNRelease(bMembRelase4, 0);

            // Nodal Forces - fill values

            arrForces[0] = new BaseClasses.CNLoad(arrNodes[1], ENLoadType.eNLT_Fx, 40.0f, 0);
            arrForces[1] = new BaseClasses.CNLoad(arrNodes[4], ENLoadType.eNLT_Fx, -60.0f, 0);
            arrForces[2] = new BaseClasses.CNLoad(arrNodes[5], ENLoadType.eNLT_Fz, 80.0f, 0);
        }
    }
}
