﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_01 : CExample
    {
        /*
        public BaseClasses.CNode[] m_arrNodes = new BaseClasses.CNode[6];
        public CMember[] arrMembers = new CMember[9];
        public CMat_00[] arrMat = new CMat_00[5];
        public CRSC.CCrSc[] m_arrCrSc = new CRSC.CCrSc[3];
        public BaseClasses.CNSupport[] arrNSupports = new BaseClasses.CNSupport[3];
        public BaseClasses.CNLoad[] arrNLoads = new BaseClasses.CNLoad[3];
        */

        public CExample_3D_01()
        {
            m_eSLN = ESLN.e3DD_1D; // 1D members in 3D model
            m_eNDOF = (int)ENDOF.e3DEnv; // DOF in 3D
            m_eGCS = EGCS.eGCSLeftHanded; // Global coordinate system

            m_arrNodes = new BaseClasses.CNode[6];
            m_arrMembers = new CMember[9];
            m_arrMat = new CMat_00[1];
            m_arrCrSc = new CRSC.CCrSc[1];
            m_arrNSupports = new BaseClasses.CNSupport[3];
            //m_arrNLoads = new BaseClasses.CNLoad[3];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            //m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.1f, 0.05f); // solid square section
            //m_arrCrSc[0] = new CCrSc_0_22(0.2f, 0.05f, 12); // tube section
            //m_arrCrSc[0] = new CCrSc_3_07(1,0.2f, 0.05f, 0.005f, 0.005f, 0.003f); // rectangular hollow section
            //m_arrCrSc[0] = new CCrSc_0_00(0.2f, 20); // Solid Half Circle
            //m_arrCrSc[0] = new CCrSc_0_04(0.2f); // Triangular Prism / Equilateral
            //m_arrCrSc[0] = new CCrSc_0_24(0.2f, 0.05f); // Triangular Prism / Equilateral with Opening - nefunguje, rozdelit na vnutorne a vonkajsie pole bodov
            //m_arrCrSc[0] = new CCrSc_0_50(0.2f, 0.1f,0.015f,0.006f); // Doubly symmetric I section
            m_arrCrSc[0] = new CCrSc_0_52(0.2f, 0.1f, 0.015f, 0.006f, -0.05f); // Monosymmetric U/C section

            // Nodes
            // Nodes List - Nodes Array

            m_arrNodes[0] = new BaseClasses.CNode(1, 0.500f, 0, 2.500f, 0);
            m_arrNodes[1] = new BaseClasses.CNode(2, 2.500f, 0, 2.500f, 0);
            m_arrNodes[2] = new BaseClasses.CNode(3, 5.500f, 0, 2.500f, 0);
            m_arrNodes[3] = new BaseClasses.CNode(4, 0.500f, 0, 0.500f, 0);
            m_arrNodes[4] = new BaseClasses.CNode(5, 2.500f, 0, 0.500f, 0);
            m_arrNodes[5] = new BaseClasses.CNode(6, 5.500f, 0, 0.500f, 0);

            // Sort by ID
            Array.Sort(m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_arrMembers[0] = new BaseClasses.CMember(1, m_arrNodes[0], m_arrNodes[1], m_arrCrSc[0], 0);
            m_arrMembers[1] = new BaseClasses.CMember(2, m_arrNodes[1], m_arrNodes[2], m_arrCrSc[0], 0);
            m_arrMembers[2] = new BaseClasses.CMember(3, m_arrNodes[0], m_arrNodes[3], m_arrCrSc[0], 0);
            m_arrMembers[3] = new BaseClasses.CMember(4, m_arrNodes[1], m_arrNodes[4], m_arrCrSc[0], 0);
            m_arrMembers[4] = new BaseClasses.CMember(5, m_arrNodes[2], m_arrNodes[5], m_arrCrSc[0], 0);
            m_arrMembers[5] = new BaseClasses.CMember(6, m_arrNodes[3], m_arrNodes[4], m_arrCrSc[0], 0);
            m_arrMembers[6] = new BaseClasses.CMember(7, m_arrNodes[4], m_arrNodes[5], m_arrCrSc[0], 0);
            m_arrMembers[7] = new BaseClasses.CMember(8, m_arrNodes[1], m_arrNodes[3], m_arrCrSc[0], 0);
            m_arrMembers[8] = new BaseClasses.CMember(9, m_arrNodes[1], m_arrNodes[5], m_arrCrSc[0], 0);

            //Sort by ID
            Array.Sort(m_arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values
            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };
            bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            // Pozn. Jednym z parametrov by malo byt pole ID uzlov v ktorych je zadefinovana tato podpora
            // objekt podpory bude len jeden a dotknute uzly budu vediet ze na ich podpora existuje a ake je konkretne ID jej nastaveni
            m_arrNSupports[0] = new BaseClasses.CNSupport(6, 1, m_arrNodes[0], bSupport1, 0);
            m_arrNSupports[1] = new BaseClasses.CNSupport(6, 2, m_arrNodes[2], bSupport2, 0);
            m_arrNSupports[2] = new BaseClasses.CNSupport(6, 3, m_arrNodes[5], bSupport3, 0);

            // Sort by ID
            Array.Sort(m_arrNSupports, new BaseClasses.CCompare_NSupportID());
        }
    }
}
