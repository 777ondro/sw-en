using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using BaseClasses.GraphObj;
using MATERIAL;
using CRSC;
using System.Windows.Media;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_80 : CExample
    {
        public CExample_3D_80()
        {
            m_eSLN = ESLN.e4DD_3D; // 3D objects in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrGOPoints = new BaseClasses.GraphObj.CPoint[49];
            //m_arrGOLines = new BaseClasses.GraphObj.CLine[21];
            m_arrGOAreas = new BaseClasses.GraphObj.CArea[0];
            m_arrGOVolumes = new BaseClasses.GraphObj.CVolume[49];

            m_arrMat = new CMat_00[1];
            //m_arrCrSc = new CRSC.CCrSc[1];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            //m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.1f, 0.05f);

            // Nodes Automatic Generation
            // Nodes List - Nodes Array

            // Ground
            // Level 0 [-1.000]

            m_arrGOPoints[00] = new CPoint(01, -50, -50, -1, 0);

            // Level 1 [+0.000]

            m_arrGOPoints[01] = new CPoint(02, 0, 0, 0, 0);
            m_arrGOPoints[02] = new CPoint(03, 1, 0, 0, 0);
            m_arrGOPoints[03] = new CPoint(04, 2, 0, 0, 0);
            m_arrGOPoints[04] = new CPoint(05, 3, 0, 0, 0);
            m_arrGOPoints[05] = new CPoint(06, 4, 0, 0, 0);
            m_arrGOPoints[06] = new CPoint(07, 5, 0, 0, 0);
            m_arrGOPoints[07] = new CPoint(08, 6, 0, 0, 0);
            m_arrGOPoints[08] = new CPoint(09, 7, 0, 0, 0);
            m_arrGOPoints[09] = new CPoint(10, 8, 0, 0, 0);
            m_arrGOPoints[10] = new CPoint(11, 9, 0, 0, 0);
            m_arrGOPoints[11] = new CPoint(12, 10, 0, 0, 0);
            m_arrGOPoints[12] = new CPoint(13, 11, 0, 0, 0);

            m_arrGOPoints[13] = new CPoint(14, 11.5f, 0.5f, 0, 0);
            m_arrGOPoints[14] = new CPoint(15, 11.5f, 1, 0, 0);
            m_arrGOPoints[15] = new CPoint(16, 11.5f, 2, 0, 0);
            m_arrGOPoints[16] = new CPoint(17, 11.5f, 3, 0, 0);
            m_arrGOPoints[17] = new CPoint(18, 11.5f, 4, 0, 0);
            m_arrGOPoints[18] = new CPoint(19, 11.5f, 5, 0, 0);
            m_arrGOPoints[19] = new CPoint(20, 11.5f, 6, 0, 0);
            m_arrGOPoints[20] = new CPoint(21, 11.5f, 7, 0, 0);
            m_arrGOPoints[21] = new CPoint(22, 11.5f, 8, 0, 0);
            m_arrGOPoints[22] = new CPoint(23, 11.5f, 9, 0, 0);
            m_arrGOPoints[23] = new CPoint(24, 11.5f, 10, 0, 0);
            m_arrGOPoints[24] = new CPoint(25, 11.5f, 11, 0, 0);

            m_arrGOPoints[25] = new CPoint(26, 11, 11.5f, 0, 0);
            m_arrGOPoints[26] = new CPoint(27, 10, 11.5f, 0, 0);
            m_arrGOPoints[27] = new CPoint(28, 09, 11.5f, 0, 0);
            m_arrGOPoints[28] = new CPoint(29, 08, 11.5f, 0, 0);
            m_arrGOPoints[29] = new CPoint(30, 07, 11.5f, 0, 0);
            m_arrGOPoints[30] = new CPoint(31, 06, 11.5f, 0, 0);
            m_arrGOPoints[31] = new CPoint(32, 05, 11.5f, 0, 0);
            m_arrGOPoints[32] = new CPoint(33, 04, 11.5f, 0, 0);
            m_arrGOPoints[33] = new CPoint(34, 03, 11.5f, 0, 0);
            m_arrGOPoints[34] = new CPoint(35, 02, 11.5f, 0, 0);
            m_arrGOPoints[35] = new CPoint(36, 01, 11.5f, 0, 0);
            m_arrGOPoints[36] = new CPoint(37, 00, 11.5f, 0, 0);

            m_arrGOPoints[37] = new CPoint(38, 0, 11, 0, 0);
            m_arrGOPoints[38] = new CPoint(39, 0, 10, 0, 0);
            m_arrGOPoints[39] = new CPoint(40, 0, 09, 0, 0);
            m_arrGOPoints[40] = new CPoint(41, 0, 08, 0, 0);
            m_arrGOPoints[41] = new CPoint(42, 0, 07, 0, 0);
            m_arrGOPoints[42] = new CPoint(43, 0, 06, 0, 0);
            m_arrGOPoints[43] = new CPoint(44, 0, 05, 0, 0);
            m_arrGOPoints[44] = new CPoint(45, 0, 04, 0, 0);
            m_arrGOPoints[45] = new CPoint(46, 0, 03, 0, 0);
            m_arrGOPoints[46] = new CPoint(47, 0, 02, 0, 0);
            m_arrGOPoints[47] = new CPoint(48, 0, 01, 0, 0);
            m_arrGOPoints[48] = new CPoint(49, 0, 0.5f, 0, 0);

















            // Setridit pole podle ID
            Array.Sort(m_arrGOPoints, new CCompare_PointID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            /*
            int[] iLinesArray_000 = new int[2];
            int[] iLinesArray_001 = new int[2];
            int[] iLinesArray_002 = new int[2];
            int[] iLinesArray_003 = new int[2];
            int[] iLinesArray_004 = new int[2];
            int[] iLinesArray_005 = new int[2];
            int[] iLinesArray_006 = new int[2];
            int[] iLinesArray_007 = new int[2];
            int[] iLinesArray_008 = new int[2];
            int[] iLinesArray_009 = new int[2];
            int[] iLinesArray_010 = new int[2];
            int[] iLinesArray_011 = new int[2];
            int[] iLinesArray_012 = new int[2];
            int[] iLinesArray_013 = new int[2];
            int[] iLinesArray_014 = new int[2];
            int[] iLinesArray_015 = new int[2];
            int[] iLinesArray_016 = new int[2];
            int[] iLinesArray_017 = new int[2];
            int[] iLinesArray_018 = new int[2];
            int[] iLinesArray_019 = new int[2];
            int[] iLinesArray_020 = new int[2];


            iLinesArray_000[0] = 0;
            iLinesArray_001[0] = 1;
            iLinesArray_002[0] = 2;
            iLinesArray_003[0] = 3;
            iLinesArray_004[0] = 4;
            iLinesArray_005[0] = 5;
            iLinesArray_006[0] = 00;
            iLinesArray_007[0] = 07;
            iLinesArray_008[0] = 08;
            iLinesArray_009[0] = 09;
            iLinesArray_010[0] = 10;
            iLinesArray_011[0] = 11;
            iLinesArray_012[0] = 01;
            iLinesArray_013[0] = 01;
            iLinesArray_014[0] = 02;
            iLinesArray_015[0] = 02;
            iLinesArray_016[0] = 03;
            iLinesArray_017[0] = 04;
            iLinesArray_018[0] = 04;
            iLinesArray_019[0] = 05;
            iLinesArray_020[0] = 05;

            iLinesArray_000[1] = 1;
            iLinesArray_001[1] = 2;
            iLinesArray_002[1] = 3;
            iLinesArray_003[1] = 4;
            iLinesArray_004[1] = 5;
            iLinesArray_005[1] = 6;
            iLinesArray_006[1] = 07;
            iLinesArray_007[1] = 08;
            iLinesArray_008[1] = 09;
            iLinesArray_009[1] = 10;
            iLinesArray_010[1] = 11;
            iLinesArray_011[1] = 06;
            iLinesArray_012[1] = 07;
            iLinesArray_013[1] = 08;
            iLinesArray_014[1] = 08;
            iLinesArray_015[1] = 09;
            iLinesArray_016[1] = 09;
            iLinesArray_017[1] = 09;
            iLinesArray_018[1] = 10;
            iLinesArray_019[1] = 10;
            iLinesArray_020[1] = 11;

            m_arrGOLines[00] = new CLine(01, iLinesArray_000, 0);
            m_arrGOLines[01] = new CLine(02, iLinesArray_001, 0);
            m_arrGOLines[02] = new CLine(03, iLinesArray_002, 0);
            m_arrGOLines[03] = new CLine(04, iLinesArray_003, 0);
            m_arrGOLines[04] = new CLine(05, iLinesArray_004, 0);
            m_arrGOLines[05] = new CLine(06, iLinesArray_005, 0);
            m_arrGOLines[06] = new CLine(07, iLinesArray_006, 0);
            m_arrGOLines[07] = new CLine(08, iLinesArray_007, 0);
            m_arrGOLines[08] = new CLine(09, iLinesArray_008, 0);
            m_arrGOLines[09] = new CLine(10, iLinesArray_009, 0);
            m_arrGOLines[10] = new CLine(11, iLinesArray_010, 0);
            m_arrGOLines[11] = new CLine(12, iLinesArray_011, 0);
            m_arrGOLines[12] = new CLine(13, iLinesArray_012, 0);
            m_arrGOLines[13] = new CLine(14, iLinesArray_013, 0);
            m_arrGOLines[14] = new CLine(15, iLinesArray_014, 0);
            m_arrGOLines[15] = new CLine(16, iLinesArray_015, 0);
            m_arrGOLines[16] = new CLine(17, iLinesArray_016, 0);
            m_arrGOLines[17] = new CLine(18, iLinesArray_017, 0);
            m_arrGOLines[18] = new CLine(19, iLinesArray_018, 0);
            m_arrGOLines[19] = new CLine(20, iLinesArray_019, 0);
            m_arrGOLines[20] = new CLine(21, iLinesArray_020, 0);

            // Setridit pole podle ID
            //Array.Sort(m_arrLines, new CCompare_LineID());
            */

            /*
            int[] iVolumePointsArray_000 = new int[8];
            int[] iVolumePointsArray_001 = new int[8];
            */

            Color vColor = Color.FromRgb(0, 51, 0);
            float fvOpacity = 0.95f;

            // Ground
            // Level 0 [-1.000]
            m_arrGOVolumes[000] = new CVolume(001, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[000], 100, 100, 1, vColor, fvOpacity, 0);

            // Level 1 [+0.000]
            vColor = Color.FromRgb(255, 255, 153);

            m_arrGOVolumes[001] = new CVolume(002, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[001], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[002] = new CVolume(003, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[002], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[003] = new CVolume(004, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[003], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[004] = new CVolume(005, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[004], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[005] = new CVolume(006, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[005], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[006] = new CVolume(007, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[006], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[007] = new CVolume(008, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[007], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[008] = new CVolume(009, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[008], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[009] = new CVolume(010, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[009], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[010] = new CVolume(011, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[010], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[011] = new CVolume(012, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[011], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[012] = new CVolume(013, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[012], 1, 0.5f, 1, vColor, fvOpacity, 0);

            m_arrGOVolumes[013] = new CVolume(014, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[013], 0.5f, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[014] = new CVolume(015, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[014], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[015] = new CVolume(016, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[015], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[016] = new CVolume(017, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[016], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[017] = new CVolume(018, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[017], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[018] = new CVolume(019, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[018], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[019] = new CVolume(020, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[019], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[020] = new CVolume(021, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[020], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[021] = new CVolume(022, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[021], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[022] = new CVolume(023, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[022], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[023] = new CVolume(024, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[023], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[024] = new CVolume(025, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[024], 0.5f, 0.5f, 1, vColor, fvOpacity, 0);

            m_arrGOVolumes[025] = new CVolume(026, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[025], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[026] = new CVolume(027, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[026], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[027] = new CVolume(028, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[027], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[028] = new CVolume(029, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[028], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[029] = new CVolume(030, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[029], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[030] = new CVolume(031, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[030], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[031] = new CVolume(032, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[031], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[032] = new CVolume(033, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[032], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[033] = new CVolume(034, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[033], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[034] = new CVolume(035, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[034], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[035] = new CVolume(036, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[035], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[036] = new CVolume(037, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[036], 1, 0.5f, 1, vColor, fvOpacity, 0);

            m_arrGOVolumes[037] = new CVolume(038, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[037], 0.5f, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[038] = new CVolume(039, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[038], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[039] = new CVolume(040, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[039], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[040] = new CVolume(041, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[040], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[041] = new CVolume(042, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[041], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[042] = new CVolume(043, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[042], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[043] = new CVolume(044, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[043], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[044] = new CVolume(045, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[044], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[045] = new CVolume(046, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[045], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[046] = new CVolume(047, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[046], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[047] = new CVolume(048, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[047], 0.5f, 1, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[048] = new CVolume(049, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[048], 0.5f, 0.5f, 1, vColor, fvOpacity, 0);



























































        }
    }
}
