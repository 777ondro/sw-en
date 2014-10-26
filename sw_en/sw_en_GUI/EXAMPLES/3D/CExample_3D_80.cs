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

            m_arrGOPoints = new BaseClasses.GraphObj.CPoint[13];
            //m_arrGOLines = new BaseClasses.GraphObj.CLine[21];
            m_arrGOAreas = new BaseClasses.GraphObj.CArea[0];
            m_arrGOVolumes = new BaseClasses.GraphObj.CVolume[13];

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

            m_arrGOVolumes[001] = new CVolume(001, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[001], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[002] = new CVolume(002, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[002], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[003] = new CVolume(003, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[003], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[004] = new CVolume(004, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[004], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[005] = new CVolume(005, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[005], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[006] = new CVolume(006, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[006], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[007] = new CVolume(007, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[007], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[008] = new CVolume(008, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[008], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[009] = new CVolume(009, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[009], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[010] = new CVolume(010, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[010], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[011] = new CVolume(011, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[011], 1, 0.5f, 1, vColor, fvOpacity, 0);
            m_arrGOVolumes[012] = new CVolume(012, EVolumeShapeType.eShape3DPrism_8Edges, m_arrGOPoints[012], 1, 0.5f, 1, vColor, fvOpacity, 0);
        }
    }
}
