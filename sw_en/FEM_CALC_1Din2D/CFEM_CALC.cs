using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CENEX;
using BaseClasses;
using MATH;

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
            TopoModel.m_arrMat[0].m_fE= 2.1e+11f;            // Unit [Pa]
            TopoModel.m_arrMat[0].m_fNu = 0.3f;              // Unit [-]
            TopoModel.m_arrMat[0].m_fG = 0.8076923e+11f;     // Unit [Pa]

            // Cross-section
            TopoModel.m_arrCrSc[0].FA_g = 0.12f;   // Unit [m^2]
            TopoModel.m_arrCrSc[0].FI_y = 0.0016f; // Unit [m^4]
            TopoModel.m_arrCrSc[0].FI_z = 0.0016f; // Unit [m^4]


            // Nodes
            // Node 1
            TopoModel.m_arrNodes[0].INode_ID = 1;
            TopoModel.m_arrNodes[0].FCoord_X = fa + fb;
            TopoModel.m_arrNodes[0].FCoord_Y = -fc;
            TopoModel.m_arrNodes[0].FCoord_Z = 0f;

            // Node 2
            TopoModel.m_arrNodes[1].INode_ID = 2;
            TopoModel.m_arrNodes[1].FCoord_X = fb;
            TopoModel.m_arrNodes[1].FCoord_Y = 0f;
            TopoModel.m_arrNodes[1].FCoord_Z = 0f;

            // Node 3
            TopoModel.m_arrNodes[2].INode_ID = 3;
            TopoModel.m_arrNodes[2].FCoord_X = 0f;
            TopoModel.m_arrNodes[2].FCoord_Y = 0f;
            TopoModel.m_arrNodes[2].FCoord_Z = 0f;

            // Node 4
            TopoModel.m_arrNodes[3].INode_ID = 4;
            TopoModel.m_arrNodes[3].FCoord_X = 0f;
            TopoModel.m_arrNodes[3].FCoord_Y = fa;
            TopoModel.m_arrNodes[3].FCoord_Z = 0f;

            // Node 5
            TopoModel.m_arrNodes[4].INode_ID = 5;
            TopoModel.m_arrNodes[4].FCoord_X = 0f;
            TopoModel.m_arrNodes[4].FCoord_Y = -fc;
            TopoModel.m_arrNodes[4].FCoord_Z = 0f;

            // Members
            // Member 1 - 1-2
            TopoModel.m_arrMembers[0].IMember_ID = 1;
            TopoModel.m_arrMembers[0].INode1 = TopoModel.m_arrNodes[0];
            TopoModel.m_arrMembers[0].INode2 = TopoModel.m_arrNodes[1];

            // Member 2 - 2-3
            TopoModel.m_arrMembers[0].IMember_ID = 2;
            TopoModel.m_arrMembers[1].INode1 = TopoModel.m_arrNodes[1];
            TopoModel.m_arrMembers[1].INode2 = TopoModel.m_arrNodes[2];

            // Member 3 - 3-4
            TopoModel.m_arrMembers[0].IMember_ID = 3;
            TopoModel.m_arrMembers[2].INode1 = TopoModel.m_arrNodes[2];
            TopoModel.m_arrMembers[2].INode2 = TopoModel.m_arrNodes[3];

            // Member 4 - 3-5
            TopoModel.m_arrMembers[0].IMember_ID = 4;
            TopoModel.m_arrMembers[3].INode1 = TopoModel.m_arrNodes[2];
            TopoModel.m_arrMembers[3].INode2 = TopoModel.m_arrNodes[4];

            // Nodal Supports
            // Support 1 - NodeIDs: 1,4
            TopoModel.m_arrNSupports[0].ISupport_ID = 1;
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[0].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[0].m_iNodeCollection = new int[2];
            TopoModel.m_arrNSupports[0].m_iNodeCollection[0] = 1;
            TopoModel.m_arrNSupports[0].m_iNodeCollection[1] = 4;

            // Support 2 - NodeIDs: 3
            TopoModel.m_arrNSupports[1].ISupport_ID = 2;
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = false;
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[1].m_bRestrain[0] = false;
            TopoModel.m_arrNSupports[1].m_iNodeCollection = new int[1];
            TopoModel.m_arrNSupports[1].m_iNodeCollection[0] = 3;

            // Support 3 - NodeIDs: 5
            TopoModel.m_arrNSupports[2].ISupport_ID = 3;
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = true; // true - 1 restraint (infinity) / false - 0 - free (zero rigidity)
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = false;
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = false;
            TopoModel.m_arrNSupports[2].m_bRestrain[0] = true;
            TopoModel.m_arrNSupports[2].m_iNodeCollection = new int[1];
            TopoModel.m_arrNSupports[2].m_iNodeCollection[0] = 5;

            // Nodal loads
            // Load 1 - NodeIDs: 2
            TopoModel.m_arrNLoads[0].INLoad_ID = 1;
            TopoModel.m_arrNLoads[0].NLoadType = ENLoadType.eNLT_Fy;
            TopoModel.m_arrNLoads[0].m_iNodeCollection = new int[1];
            TopoModel.m_arrNLoads[0].m_iNodeCollection[0] = 2;
            TopoModel.m_arrNLoads[0].Value = fF2; // Positive

            // Member loads
            // Load 1 - MemberIDs: 1

            float fAlpha_1 = (236.3099f/360f) * 2 * MathF.fPI;  // Radians
            float fF1x = fF1 * (float)Math.Sin(fAlpha_1);       // Force in local coordinate system of member
            float fF1y= fF1 * (float)Math.Cos(fAlpha_1);        // Force in local coordinate system of member

            CMLoad_11 MLoad_F1 = new CMLoad_11(fF1x, 0.5f * TopoModel.m_arrMembers[0].FLength);
            TopoModel.m_arrMLoads[0].IMLoad_ID = 1;
            TopoModel.m_arrMLoads[0].MLoadType = EMLoadType1.eMLT_FS_H;
            TopoModel.m_arrMLoads[0].m_iMemberCollection = new int[1];
            TopoModel.m_arrMLoads[0].m_iMemberCollection[0] = 1;

            // Load 2 - MemberIDs: 2
            CMLoad_21 MLoad_q = new CMLoad_21(fq,EMLoadDirPCC1.eMLD_PCC_V);
            TopoModel.m_arrMLoads[1].IMLoad_ID = 2;
            TopoModel.m_arrMLoads[1].MLoadType = EMLoadType1.eMLT_QUF_W;
            TopoModel.m_arrMLoads[1].m_iMemberCollection = new int[1];
            TopoModel.m_arrMLoads[1].m_iMemberCollection[0] = 3;

            // Load 3 - MemberIDs: 4
            CMLoad_11 MLoad_M = new CMLoad_11(fM, 0.5f * TopoModel.m_arrMembers[3].FLength);
            TopoModel.m_arrMLoads[2].IMLoad_ID = 3;
            TopoModel.m_arrMLoads[2].MLoadType = EMLoadType1.eMLT_FS_H;
            TopoModel.m_arrMLoads[2].m_iMemberCollection = new int[1];
            TopoModel.m_arrMLoads[2].m_iMemberCollection[0] = 4;

            // Load Cases
            // Load Case 1
            TopoModel.m_arrLoadCases[0].ILoadCase_ID = 1;

            // Load Combinations
            // Load Combination 1
            TopoModel.m_arrLoadCombs[0].ILoadComb_ID = 1;

        }
    }
}
