using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATH;
using BaseClasses;
using CENEX;
using CRSC;
using MATERIAL;

namespace FEM_CALC_1Din2D
{
    public class CTest_2:CModel
    {
        public CModel TopoModel; // Create topological model

        public CTest_2()
        {

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEMPORARY EXAMPLE DATA for 2D solution

            // Use basic SI units

            // Sobota, J. > Statika stavebnych konstrukcii 2

            // Load
            float fq = 20f;   // Unit [N/m]

            // Geometry
            float fa = 3.0f,
                  fb = 4.0f,
                  fc = 1.0f,
                  fd = 2.0f,
                  fe = 2.5f;// Unit [m]

            // MODEL

            // Create topological model and allocate memory
            TopoModel = new CModel("Test2", ESLN.e2DD_1D, 3, EGCS.eGCSLeftHanded, 1, 3, 5, 4, 1, 0, 0, 1, 1, 1);

            // Materials
            CMat_00 Mat0 = new CMat_00();

            TopoModel.m_arrMat[0] = Mat0;

            // Cross-section
            CCrSc CrSc0 = new CCrSc_0_00();  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            CrSc0.FA_g = 1.18095238095E-02f; // Unit [m^2]
            CrSc0.FI_y = 9.52380952381E-05f; // Unit [m^4]
            CrSc0.m_Mat = TopoModel.m_arrMat[0]; // Set CrSc Material

            CCrSc CrSc1 = new CCrSc_0_00();  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            CrSc0.FA_g = 1.48571428571E-02f; // Unit [m^2]
            CrSc0.FI_y = 1.90476190476E-04f; // Unit [m^4]
            CrSc0.m_Mat = TopoModel.m_arrMat[0]; // Set CrSc Material

            CCrSc CrSc2 = new CCrSc_0_00();  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            CrSc0.FA_g = 1.35238095238E-02f; // Unit [m^2]
            CrSc0.FI_y = 1.42857142857E-07f; // Unit [m^4]
            CrSc0.m_Mat = TopoModel.m_arrMat[0]; // Set CrSc Material

            TopoModel.m_arrCrSc[0] = CrSc0;
            TopoModel.m_arrCrSc[1] = CrSc1;
            TopoModel.m_arrCrSc[2] = CrSc2;

            // Nodes
            // Node 1
            CNode Node0 = new CNode();
            Node0.INode_ID = 1;
            Node0.FCoord_X = 0f;
            Node0.FCoord_Y = 0f;
            Node0.FCoord_Z = 0f;

            TopoModel.m_arrNodes[0] = Node0;

            // Node 2
            CNode Node1 = new CNode();
            Node1.INode_ID = 2;
            Node1.FCoord_X = 0f;
            Node1.FCoord_Y = - fc - fd;
            Node1.FCoord_Z = 0f;

            TopoModel.m_arrNodes[1] = Node1;

            // Node 3
            CNode Node2 = new CNode();
            Node2.INode_ID = 3;
            Node2.FCoord_X = fa;
            Node2.FCoord_Y = - fc - fd - fe;
            Node2.FCoord_Z = 0f;

            TopoModel.m_arrNodes[2] = Node2;

            // Node 4
            CNode Node3 = new CNode();
            Node3.INode_ID = 4;
            Node3.FCoord_X = fa + fb;
            Node3.FCoord_Y = -fc - fd - fe;
            Node3.FCoord_Z = 0f;

            TopoModel.m_arrNodes[3] = Node3;

            // Node 5
            CNode Node4 = new CNode();
            Node4.INode_ID = 5;
            Node4.FCoord_X = fa + fb;
            Node4.FCoord_Y = fc;
            Node4.FCoord_Z = 0f;

            TopoModel.m_arrNodes[4] = Node4;

            // Members
            // Member 1 - 1-2
            CMember Member0 = new CMember();
            Member0.IMember_ID = 1;
            Member0.NodeStart = TopoModel.m_arrNodes[0];
            Member0.NodeEnd = TopoModel.m_arrNodes[1];
            Member0.CrScStart = TopoModel.m_arrCrSc[0];
            Member0.Fill_Basic();

            TopoModel.m_arrMembers[0] = Member0;

            // Member 2 - 2-3
            CMember Member1 = new CMember();
            Member1.IMember_ID = 2;
            Member1.NodeStart = TopoModel.m_arrNodes[1];
            Member1.NodeEnd = TopoModel.m_arrNodes[2];
            Member1.CrScStart = TopoModel.m_arrCrSc[1];
            Member1.Fill_Basic();

            TopoModel.m_arrMembers[1] = Member1;

            // Member 3 - 3-4
            CMember Member2 = new CMember();
            Member2.IMember_ID = 3;
            Member2.NodeStart = TopoModel.m_arrNodes[2];
            Member2.NodeEnd = TopoModel.m_arrNodes[3];
            Member2.CrScStart = TopoModel.m_arrCrSc[1];
            Member2.Fill_Basic();

            TopoModel.m_arrMembers[2] = Member2;

            // Member 4 - 4-5
            CMember Member3 = new CMember();
            Member3.IMember_ID = 4;
            Member3.NodeStart = TopoModel.m_arrNodes[3];
            Member3.NodeEnd = TopoModel.m_arrNodes[4];
            Member3.CrScStart = TopoModel.m_arrCrSc[2];
            Member3.Fill_Basic();

            TopoModel.m_arrMembers[3] = Member3;

            // Nodal Supports
            // Support 1 - NodeIDs: 1,5
            CNSupport NSupport0 = new CNSupport(TopoModel.m_eNDOF);
            NSupport0.ISupport_ID = 1;
            NSupport0.m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            NSupport0.m_bRestrain[1] = true;
            NSupport0.m_bRestrain[2] = true;
            NSupport0.m_iNodeCollection = new int[2];
            NSupport0.m_iNodeCollection[0] = 1;
            NSupport0.m_iNodeCollection[1] = 5;

            TopoModel.m_arrNSupports[0] = NSupport0;

            // Member loads

            // Load 1 - MemberIDs: 3
            CMLoad_21 MLoad_q = new CMLoad_21(fq);
            MLoad_q.ID = 1;
            MLoad_q.MLoadTypeDistr = EMLoadTypeDistr.eMLT_QUF_W_21;
            MLoad_q.MLoadType = EMLoadType.eMLT_F;
            MLoad_q.EDirPPC = EMLoadDirPCC1.eMLD_PCC_FYU_MZV;
            MLoad_q.IMemberCollection = new int[1];
            MLoad_q.IMemberCollection[0] = 3;

            TopoModel.m_arrMLoads[0] = MLoad_q;

            // Load Cases
            // Load Case 1
            CLoadCase LoadCase0 = new CLoadCase();
            LoadCase0.ID = 1;

            TopoModel.m_arrLoadCases[0] = LoadCase0;

            // Load Combinations
            // Load Combination 1
            CLoadCombination LoadComb0 = new CLoadCombination();
            LoadComb0.ID = 1;

            TopoModel.m_arrLoadCombs[0] = LoadComb0;
        }
    }
}
