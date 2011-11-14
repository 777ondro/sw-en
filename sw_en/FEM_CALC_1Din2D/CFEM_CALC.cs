using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;
using MATH;
using MATERIAL;
using CRSC;

namespace FEM_CALC_1Din2D
{
    public class CFEM_CALC
    {
        CModel TopoModel;
        CGenex FEMModel;

        public CFEM_CALC()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // TEMPORARY EXAMPLE DATA for 2D solution

            // Use basic SI units

            // Load
            float fF1 = 45000f;   // Unit [N]
            float fF2 = 55000f;   // Unit [N]
            float fM = 60000000f;   // Unit [Nm]
            float fq = 16f;   // Unit [N/m]

            // Geometry
            float fa = 2.8f,
                  fb = 5.6f,
                  fc = 4.2f;     // Unit [m]

            // MODEL

            // Create topological model and allocate memory
            TopoModel = new CModel("Test1",1,1,5,4,3,1,3,1,1);

            // Materials
            CMat_00 Mat0 = new CMat_00();
            Mat0.m_fE= 2.1e+11f;            // Unit [Pa]
            Mat0.m_fNu = 0.3f;              // Unit [-]
            Mat0.m_fG = 0.8076923e+11f;     // Unit [Pa]

            TopoModel.m_arrMat[0] = Mat0;

            // Cross-section
            CCrSc CrSc0 = new CCrSc();
            CrSc0.FA_g = 0.12f;   // Unit [m^2]
            CrSc0.FI_y = 0.0016f; // Unit [m^4]
            CrSc0.FI_z = 0.0016f; // Unit [m^4]

            TopoModel.m_arrCrSc[0] = CrSc0;

            // Nodes
            // Node 1
            CNode Node0 = new CNode();
            Node0.INode_ID = 1;
            Node0.FCoord_X = fa + fb;
            Node0.FCoord_Y = -fc;
            Node0.FCoord_Z = 0f;

            TopoModel.m_arrNodes[0] = Node0;

            // Node 2
            CNode Node1 = new CNode();
            Node1.INode_ID = 2;
            Node1.FCoord_X = fb;
            Node1.FCoord_Y = 0f;
            Node1.FCoord_Z = 0f;

            TopoModel.m_arrNodes[1] = Node1;

            // Node 3
            CNode Node2 = new CNode();
            Node2.INode_ID = 3;
            Node2.FCoord_X = 0f;
            Node2.FCoord_Y = 0f;
            Node2.FCoord_Z = 0f;

            TopoModel.m_arrNodes[2] = Node2;

            // Node 4
            CNode Node3 = new CNode();
            Node3.INode_ID = 4;
            Node3.FCoord_X = 0f;
            Node3.FCoord_Y = fa;
            Node3.FCoord_Z = 0f;

            TopoModel.m_arrNodes[3] = Node3;

            // Node 5
            CNode Node4 = new CNode();
            Node4.INode_ID = 5;
            Node4.FCoord_X = 0f;
            Node4.FCoord_Y = -fc;
            Node4.FCoord_Z = 0f;

            TopoModel.m_arrNodes[4] = Node4;

            // Members
            // Member 1 - 1-2
            CMember Member0 = new CMember();
            Member0.IMember_ID = 1;
            Member0.INode1 = TopoModel.m_arrNodes[0];
            Member0.INode2 = TopoModel.m_arrNodes[1];

            TopoModel.m_arrMembers[0] = Member0;

            // Member 2 - 2-3
            CMember Member1 = new CMember();
            Member1.IMember_ID = 2;
            Member1.INode1 = TopoModel.m_arrNodes[1];
            Member1.INode2 = TopoModel.m_arrNodes[2];

            TopoModel.m_arrMembers[1] = Member1;

            // Member 3 - 3-4
            CMember Member2 = new CMember();
            Member2.IMember_ID = 3;
            Member2.INode1 = TopoModel.m_arrNodes[2];
            Member2.INode2 = TopoModel.m_arrNodes[3];

            TopoModel.m_arrMembers[2] = Member2;

            // Member 4 - 3-5
            CMember Member3 = new CMember();
            Member3.IMember_ID = 4;
            Member3.INode1 = TopoModel.m_arrNodes[2];
            Member3.INode2 = TopoModel.m_arrNodes[4];

            TopoModel.m_arrMembers[3] = Member3;

            // Nodal Supports
            // Support 1 - NodeIDs: 1,4
            CNSupport NSupport0 = new CNSupport();
            NSupport0.ISupport_ID = 1;
            NSupport0.m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            NSupport0.m_bRestrain[1] = true;
            NSupport0.m_bRestrain[2] = true;
            NSupport0.m_bRestrain[3] = true;
            NSupport0.m_bRestrain[4] = true;
            NSupport0.m_bRestrain[5] = true;
            NSupport0.m_iNodeCollection = new int[2];
            NSupport0.m_iNodeCollection[0] = 1;
            NSupport0.m_iNodeCollection[1] = 4;

            TopoModel.m_arrNSupports[0] = NSupport0;

            // Support 2 - NodeIDs: 3
            CNSupport NSupport1 = new CNSupport();
            NSupport1.ISupport_ID = 2;
            NSupport1.m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            NSupport1.m_bRestrain[1] = false;
            NSupport1.m_bRestrain[2] = true;
            NSupport1.m_bRestrain[3] = true;
            NSupport1.m_bRestrain[4] = true;
            NSupport1.m_bRestrain[5] = false;
            NSupport1.m_iNodeCollection = new int[1];
            NSupport1.m_iNodeCollection[0] = 3;

            TopoModel.m_arrNSupports[1] = NSupport1;

            // Support 3 - NodeIDs: 5
            CNSupport NSupport2 = new CNSupport();
            NSupport2.ISupport_ID = 3;
            NSupport2.m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            NSupport2.m_bRestrain[1] = true;
            NSupport2.m_bRestrain[2] = true;
            NSupport2.m_bRestrain[3] = false;
            NSupport2.m_bRestrain[4] = false;
            NSupport2.m_bRestrain[5] = true;
            NSupport2.m_iNodeCollection = new int[1];
            NSupport2.m_iNodeCollection[0] = 5;

            TopoModel.m_arrNSupports[2] = NSupport2;

            // Nodal loads
            // Load 1 - NodeIDs: 2
            CNLoad NLoad0 = new CNLoad();
            NLoad0.INLoad_ID = 1;
            NLoad0.NLoadType = ENLoadType.eNLT_Fy;
            NLoad0.m_iNodeCollection = new int[1];
            NLoad0.m_iNodeCollection[0] = 2;
            NLoad0.Value = fF2; // Positive

            TopoModel.m_arrNLoads[0] = NLoad0;

            // Member loads
            // Load 1 - MemberIDs: 1

            float fAlpha_1 = (236.3099f/360f) * 2 * MathF.fPI;  // Radians
            float fF1x = fF1 * (float)Math.Sin(fAlpha_1);       // Force in local coordinate system of member
            float fF1y= fF1 * (float)Math.Cos(fAlpha_1);        // Force in local coordinate system of member

            CMLoad_11 MLoad_F1 = new CMLoad_11(fF1x, 0.5f * TopoModel.m_arrMembers[0].FLength);
            MLoad_F1.IMLoad_ID = 1;
            MLoad_F1.MLoadType = EMLoadType1.eMLT_FS_H;
            MLoad_F1.m_iMemberCollection = new int[1];
            MLoad_F1.m_iMemberCollection[0] = 1;

            TopoModel.m_arrMLoads[0] = MLoad_F1;

            // Load 2 - MemberIDs: 2
            CMLoad_21 MLoad_q = new CMLoad_21(fq,EMLoadDirPCC1.eMLD_PCC_V);
            MLoad_q.IMLoad_ID = 2;
            MLoad_q.MLoadType = EMLoadType1.eMLT_QUF_W;
            MLoad_q.m_iMemberCollection = new int[1];
            MLoad_q.m_iMemberCollection[0] = 3;

            TopoModel.m_arrMLoads[1] = MLoad_q;

            // Load 3 - MemberIDs: 4
            CMLoad_11 MLoad_M = new CMLoad_11(fM, 0.5f * TopoModel.m_arrMembers[3].FLength);
            MLoad_M.IMLoad_ID = 3;
            MLoad_M.MLoadType = EMLoadType1.eMLT_FS_H;
            MLoad_M.m_iMemberCollection = new int[1];
            MLoad_M.m_iMemberCollection[0] = 4;

            TopoModel.m_arrMLoads[2] = MLoad_M;

            // Load Cases
            // Load Case 1
            CLoadCase LoadCase0 = new CLoadCase();
            LoadCase0.ILoadCase_ID = 1;

            TopoModel.m_arrLoadCases[0] = LoadCase0;

            // Load Combinations
            // Load Combination 1
            CLoadCombination LoadComb0 = new CLoadCombination();
            LoadComb0.ILoadComb_ID = 1;

            TopoModel.m_arrLoadCombs[0] = LoadComb0;













        }
    }
}
