using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_80 : CExample
    {
        public CExample_3D_80()
        {
            m_eSLN = ESLN.e4DD_3D; // 3D objects in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrGOPoints = new BaseClasses.GraphObj.CPoint[12];
            m_arrGOLines = new BaseClasses.GraphObj.CLine[21];
            m_arrGOAreas = new BaseClasses.GraphObj.CArea[0];
            m_arrGOVolumes = new BaseClasses.GraphObj.CVolume[0];

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

            // Upper Chord Nodes
            m_arrNodes[00] = new CNode(01, 0, 0, 0, 0);
            m_arrNodes[01] = new CNode(02, 1, 0, 0, 0);
            m_arrNodes[02] = new CNode(03, 2, 0, 0, 0);
            m_arrNodes[03] = new CNode(04, 3, 0, 0, 0);
            m_arrNodes[04] = new CNode(05, 4, 0, 0, 0);
            m_arrNodes[05] = new CNode(06, 5, 0, 0, 0);
            m_arrNodes[06] = new CNode(07, 6, 0, 0, 0);

            // Bottom Chord Nodes
            m_arrNodes[07] = new CNode(08, 1, 0, 4, 0);
            m_arrNodes[08] = new CNode(09, 2, 0, 6, 0);
            m_arrNodes[09] = new CNode(10, 3, 0, 7, 0);
            m_arrNodes[10] = new CNode(11, 4, 0, 6, 0);
            m_arrNodes[11] = new CNode(12, 5, 0, 4, 0);

            // Setridit pole podle ID
            Array.Sort(m_arrNodes, new CCompare_NodeID());

            // Lines Automatic Generation
            // Lines List - Lines Array

            // Upper Chord Lines
            m_arrMembers[00] = new CMember(01, m_arrNodes[0], m_arrNodes[1], m_arrCrSc[0], 0);
            m_arrMembers[01] = new CMember(02, m_arrNodes[1], m_arrNodes[2], m_arrCrSc[0], 0);
            m_arrMembers[02] = new CMember(03, m_arrNodes[2], m_arrNodes[3], m_arrCrSc[0], 0);
            m_arrMembers[03] = new CMember(04, m_arrNodes[3], m_arrNodes[4], m_arrCrSc[0], 0);
            m_arrMembers[04] = new CMember(05, m_arrNodes[4], m_arrNodes[5], m_arrCrSc[0], 0);
            m_arrMembers[05] = new CMember(06, m_arrNodes[5], m_arrNodes[6], m_arrCrSc[0], 0);
            // Bottom Chord Lines
            m_arrMembers[06] = new CMember(07, m_arrNodes[00], m_arrNodes[07], m_arrCrSc[0], 0);
            m_arrMembers[07] = new CMember(08, m_arrNodes[07], m_arrNodes[08], m_arrCrSc[0], 0);
            m_arrMembers[08] = new CMember(09, m_arrNodes[08], m_arrNodes[09], m_arrCrSc[0], 0);
            m_arrMembers[09] = new CMember(10, m_arrNodes[09], m_arrNodes[10], m_arrCrSc[0], 0);
            m_arrMembers[10] = new CMember(11, m_arrNodes[10], m_arrNodes[11], m_arrCrSc[0], 0);
            m_arrMembers[11] = new CMember(12, m_arrNodes[11], m_arrNodes[06], m_arrCrSc[0], 0);
            // Diagonal Lines
            m_arrMembers[12] = new CMember(13, m_arrNodes[01], m_arrNodes[07], m_arrCrSc[0], 0);
            m_arrMembers[13] = new CMember(14, m_arrNodes[01], m_arrNodes[08], m_arrCrSc[0], 0);
            m_arrMembers[14] = new CMember(15, m_arrNodes[02], m_arrNodes[08], m_arrCrSc[0], 0);
            m_arrMembers[15] = new CMember(16, m_arrNodes[02], m_arrNodes[09], m_arrCrSc[0], 0);
            m_arrMembers[16] = new CMember(17, m_arrNodes[03], m_arrNodes[09], m_arrCrSc[0], 0);
            m_arrMembers[17] = new CMember(18, m_arrNodes[04], m_arrNodes[09], m_arrCrSc[0], 0);
            m_arrMembers[18] = new CMember(19, m_arrNodes[04], m_arrNodes[10], m_arrCrSc[0], 0);
            m_arrMembers[19] = new CMember(20, m_arrNodes[05], m_arrNodes[10], m_arrCrSc[0], 0);
            m_arrMembers[20] = new CMember(21, m_arrNodes[05], m_arrNodes[11], m_arrCrSc[0], 0);

            // Setridit pole podle ID
            //Array.Sort(m_arrMembers, new CCompare_LineID());

        }
    }
}
