using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using MATERIAL;
using CRSC;

namespace sw_en_GUI.EXAMPLES._3D
{
    class CExample_3D_11:CExample
    {
        public BaseClasses.CModel m_TopoModel = new BaseClasses.CModel();

        public CExample_3D_11()
        {
            m_TopoModel.m_arrNodes = new BaseClasses.CNode[4];
            m_TopoModel.m_arrMembers = new CMember[3];
            m_TopoModel.m_arrMat = new CMat_00[1];
            m_TopoModel.m_arrCrSc = new CRSC.CCrSc[1];
            m_TopoModel.m_arrNSupports = new BaseClasses.CNSupport[2];
            m_TopoModel.m_arrMLoads = new BaseClasses.CMLoad[3];

            // Materials
            // Materials List - Materials Array - Fill Data of Materials Array
            m_TopoModel.m_arrMat[0] = new CMat_03_00();

            // Cross-sections
            // CrSc List - CrSc Array - Fill Data of Cross-sections Array
            m_TopoModel.m_arrCrSc[0] = new CRSC.CCrSc_3_00(0, 8, 0.300f, 0.125f, 0.0162f, 0.0108f, 0.0108f, 0.0065f, 0.2416f); // I 300 section
            m_TopoModel.m_arrCrSc[0].FI_t = 5.69e-07f;
            m_TopoModel.m_arrCrSc[0].FI_y = 9.79e-05f;
            m_TopoModel.m_arrCrSc[0].FI_z = 4.49e-06f;
            m_TopoModel.m_arrCrSc[0].FA_g = 6.90e-03f;
            m_TopoModel.m_arrCrSc[0].FA_vy = 4.01e-03f;
            m_TopoModel.m_arrCrSc[0].FA_vz = 2.89e-03f;

            // Nodes
            // Nodes List - Nodes Array

            // Geometry
            float fGeom_a = 4f,
                  fGeom_b = 5f,
                  fGeom_c = 3.5f;     // Unit [m]

            m_TopoModel.m_arrNodes[0] = new BaseClasses.CNode(1, fGeom_a,        0,        0, 0);
            m_TopoModel.m_arrNodes[1] = new BaseClasses.CNode(2,       0,        0,        0, 0);
            m_TopoModel.m_arrNodes[2] = new BaseClasses.CNode(3, fGeom_a,        0, -fGeom_c, 0);
            m_TopoModel.m_arrNodes[3] = new BaseClasses.CNode(4, fGeom_a, -fGeom_b,        0, 0);

            // Sort by ID
            Array.Sort(m_TopoModel.m_arrNodes, new BaseClasses.CCompare_NodeID());

            // Members
            // Members List - Members Array

            m_TopoModel.m_arrMembers[0] = new BaseClasses.CMember(1, m_TopoModel.m_arrNodes[0], m_TopoModel.m_arrNodes[1], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[1] = new BaseClasses.CMember(2, m_TopoModel.m_arrNodes[0], m_TopoModel.m_arrNodes[2], m_TopoModel.m_arrCrSc[0], 0);
            m_TopoModel.m_arrMembers[2] = new BaseClasses.CMember(3, m_TopoModel.m_arrNodes[0], m_TopoModel.m_arrNodes[3], m_TopoModel.m_arrCrSc[0], 0);

            // Nodal Supports - fill values

            // Restraints - list of node degreess of freedom
            // UX, UY, UZ, RX, RY, RZ
            // false - 0 - free DOF
            // true - 1 - restrained (rigid)

            // Set values
            bool[] bSupport1 = { true, true, true, true, true, true};
            bool[] bSupport2 = { true, true, true, false, false, false };

            // Create Support Objects
            m_TopoModel.m_arrNSupports[0] = new CNSupport(6, 1, m_TopoModel.m_arrNodes[1], bSupport1, 0);
            m_TopoModel.m_arrNSupports[1] = new CNSupport(6, 2, m_TopoModel.m_arrNodes[2], bSupport2, 0);

            // Loads
            m_TopoModel.m_arrMLoads[0] = new CMLoad_21(5000f);  // q - whole member
            m_TopoModel.m_arrMLoads[1] = new CMLoad_12(17000f); // F - in the middle of member
            m_TopoModel.m_arrMLoads[2] = new CMLoad_12(20000f); // M - in the middle of member



        
        }
    }
}
