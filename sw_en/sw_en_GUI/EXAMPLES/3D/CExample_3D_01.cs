using System;
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
        public BaseClasses.CModel m_TopoModel = new BaseClasses.CModel();
        /*
        public BaseClasses.CNode[] m_TopoModel.m_arrNodes = new BaseClasses.CNode[6];
        public CMember[] arrMembers = new CMember[9];
        public CMat_00[] arrMat = new CMat_00[5];
        public CRSC.CCrSc[] m_TopoModel.m_arrCrSc = new CRSC.CCrSc[3];
        public BaseClasses.CNSupport[] arrNSupports = new BaseClasses.CNSupport[3];
        public BaseClasses.CNLoad[] arrNLoads = new BaseClasses.CNLoad[3];
        */

        public CExample_3D_01()
        {
            m_TopoModel.m_arrNodes = new BaseClasses.CNode[6];
            m_TopoModel.m_arrMembers = new CMember[9];
            m_TopoModel.m_arrMat = new CMat_00[1];
            m_TopoModel.m_arrCrSc = new CRSC.CCrSc[1];
            m_TopoModel.m_arrNSupports = new BaseClasses.CNSupport[3];
            //m_TopoModel.m_arrNLoads = new BaseClasses.CNLoad[3];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_TopoModel.m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_TopoModel.m_arrCrSc[0] = new CRSC.CCrSc_0_05(0.1f, 0.05f);

            // Nodes
            // Nodes List - Nodes Array

            m_TopoModel.m_arrNodes[0] = new BaseClasses.CNode(1, 0.500f, 0, 2.500f, 0);
            m_TopoModel.m_arrNodes[1] = new BaseClasses.CNode(2, 2.500f, 0, 2.500f, 0);
            m_TopoModel.m_arrNodes[2] = new BaseClasses.CNode(3, 5.500f, 0, 2.500f, 0);
            m_TopoModel.m_arrNodes[3] = new BaseClasses.CNode(4, 0.500f, 0, 0.500f, 0);
            m_TopoModel.m_arrNodes[4] = new BaseClasses.CNode(5, 2.500f, 0, 0.500f, 0);
            m_TopoModel.m_arrNodes[5] = new BaseClasses.CNode(6, 5.500f, 0, 0.500f, 0);

            // Sort by ID
            Array.Sort(m_TopoModel.m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_TopoModel.m_arrMembers[0] = new BaseClasses.CMember(1, m_TopoModel.m_arrNodes[0], m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[1] = new BaseClasses.CMember(2, m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrNodes[2], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[2] = new BaseClasses.CMember(3, m_TopoModel.m_arrNodes[0], m_TopoModel.m_arrNodes[3], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[3] = new BaseClasses.CMember(4, m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrNodes[4], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[4] = new BaseClasses.CMember(5, m_TopoModel.m_arrNodes[2], m_TopoModel.m_arrNodes[5], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[5] = new BaseClasses.CMember(6, m_TopoModel.m_arrNodes[3], m_TopoModel.m_arrNodes[4], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[6] = new BaseClasses.CMember(7, m_TopoModel.m_arrNodes[4], m_TopoModel.m_arrNodes[5], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[7] = new BaseClasses.CMember(8, m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrNodes[3], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[8] = new BaseClasses.CMember(9, m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrNodes[5], m_TopoModel.m_arrCrSc[0], 0);

            //Sort by ID
            Array.Sort(m_TopoModel.m_arrMembers, new BaseClasses.CCompare_MemberID());

            // Nodal Supports - fill values
            // Set values
            bool[] bSupport1 = { true, false, true, false, true, false };
            bool[] bSupport2 = { false, false, true, false, true, false };
            bool[] bSupport3 = { true, false, false, false, false, false };

            // Create Support Objects
            // Pozn. Jednym z parametrov by malo byt pole ID uzlov v ktorych je zadefinovana tato podpora
            // objekt podpory bude len jeden a dotknute uzly budu vediet ze na ich podpora existuje a ake je konkretne ID jej nastaveni
            m_TopoModel.m_arrNSupports[0] = new BaseClasses.CNSupport(6, 1, m_TopoModel.m_arrNodes[0], bSupport1, 0);
            m_TopoModel.m_arrNSupports[1] = new BaseClasses.CNSupport(6, 2, m_TopoModel.m_arrNodes[2], bSupport2, 0);
            m_TopoModel.m_arrNSupports[2] = new BaseClasses.CNSupport(6, 3, m_TopoModel.m_arrNodes[5], bSupport3, 0);

            // Sort by ID
            Array.Sort(m_TopoModel.m_arrNSupports, new BaseClasses.CCompare_NSupportID());
        }
    }
}
