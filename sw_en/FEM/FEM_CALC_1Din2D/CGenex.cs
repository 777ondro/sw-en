using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CENEX;


namespace FEM_CALC_1Din2D
{
    public class CGenex
    {
        // Trieda generuje z konstrukcneho modelu (uzly, pruty, segmenty, linie) siet 1D FEM elementov (uzly, pruty)
        // Stacilo by aby tieto objekty obsahovali odkazy na existujuce topologicke uzly / segmenty / pruty, nemusi sa vsetko kopirovat

        // Pole FEM uzlov
        public CFemNode[] m_arrFemNodes;
        // Pole 1D FEM prvkov
        public CE_1D[] m_arrFemMembers;

        public CGenex(CModel TopoModel)
        {
            // Allocate memory

            m_arrFemNodes = new CFemNode[TopoModel.m_arrNodes.Length];
            m_arrFemMembers = new CE_1D[TopoModel.m_arrMembers.Length];

            // Temporary - to fill genex data
            // Same as topological data

            GenerateMesh1D(TopoModel);
        }

        public void GenerateMesh1D(CModel TopoModel)
        {
            // Generate FEM nodes
            // Nodes
            for (int i = 0; i < TopoModel.m_arrNodes.Length; i++)
            {
                // Create FEM node
                CFemNode TempNode = new CFemNode(TopoModel.m_arrNodes[i]);
                m_arrFemNodes[i] = TempNode;

                // Set FEM node DOF

                // Get nodal support
                // Search if node is in list of supported nodes for each nodal support
                for (int i2 = 0; i2 < TopoModel.m_arrNSupports.Length; i2++) // Check all nodal supports
                {
                    for (int j = 0; j < TopoModel.m_arrNSupports[i2].m_iNodeCollection.Length; j++) // Check list of nodes (Nodes IDs collection)
                    {
                        if (m_arrFemNodes[i].INode_ID == TopoModel.m_arrNSupports[i2].m_iNodeCollection[j])
                        {
                            // DOF of nodes are free - zero rigidity of restraints  false as default
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eUX] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e2D_DOF.eUX]; // !!! 2D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eUY] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e2D_DOF.eUY]; // !!! 2D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e2D_DOF.eRZ] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e2D_DOF.eRZ]; // !!! 2D Environment enum
                        }
                    }
                }
            }

            // Generate FEM members
            // Members
            for (int i = 0; i < TopoModel.m_arrMembers.Length; i++)
            {
                CE_1D TempMember = new CE_1D(TopoModel.m_arrMembers[i], m_arrFemNodes);
                m_arrFemMembers[i] = TempMember;

                // Set FEM Member DOF
                if (TopoModel.m_arrNReleases != null) // Some releases exist
                {
                    for (int j = 0; j < TopoModel.m_arrNReleases.Length; j++) // Check each release
                    {
                        for (int k = 0; k < TopoModel.m_arrNReleases[j].m_iMembCollection.Length; k++) //  Check each member in collection
                        {
                            if (TopoModel.m_arrNReleases[j].m_iMembCollection[k] == m_arrFemMembers[i].MemberID) // if release exists on member
                            {
                                if (TopoModel.m_arrMembers[i].CnRelease1 != null)
                                    m_arrFemMembers[i].CnRelease1 = TopoModel.m_arrMembers[i].CnRelease1;
                                if (TopoModel.m_arrMembers[i].CnRelease2 != null)
                                    m_arrFemMembers[i].CnRelease2 = TopoModel.m_arrMembers[i].CnRelease2;
                            }
                        }
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Additional data of nodes depending on generated members

            for (int i = 0; i < m_arrFemNodes.Length; i++)
            {
                m_arrFemNodes[i].m_iMemberCollection = new System.Collections.ArrayList(); // Allocate collection memory

                for (int j = 0; j < m_arrFemMembers.Length; j++)
                {
                    if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[j].m_NodeStart.INode_ID || m_arrFemNodes[i].INode_ID == m_arrFemMembers[j].m_NodeEnd.INode_ID) // Is node ID same as member start or end node ID
                    {
                        m_arrFemNodes[i].m_iMemberCollection.Add(m_arrFemMembers[j].m_Member.IMember_ID); // Add FEMmember ID to the FEM node list
                    }
                }
            }

            //  If only two members are connected in one node and if release exists at that node, copy this release from one member to the another
            for (int i = 0; i < m_arrFemNodes.Length; i++)
            {
                if (m_arrFemNodes[i].m_iMemberCollection != null && m_arrFemNodes[i].m_iMemberCollection.Count == 2) // Node is connected to two members
                { 
                    // We know member ID, so we can get index of members in list
                    int iMember1_index = -1;
                    int iMember2_index = -1;

                    for (int j = 0; j < m_arrFemMembers.Length; j++)
                    {
                        // 1st member index
                        if (iMember1_index < 0 && m_arrFemNodes[i].m_iMemberCollection.Contains(m_arrFemMembers[j].m_Member.IMember_ID)) // if Member ID is in the list
                        {
                            iMember1_index = j; // Set 1st
                        }

                        if(iMember1_index > -1) // Index was defined, we can break cycle
                          break;
                    }

                    // 2nd member index
                    for (int k = iMember1_index + 1; k < m_arrFemMembers.Length; k++) // Search for second only in interval between first founded member and last member
                    {
                        if (iMember2_index < 0 && m_arrFemNodes[i].m_iMemberCollection.Contains(m_arrFemMembers[k].m_Member.IMember_ID)) // if Member ID is in the list interval
                        {
                            iMember2_index = k;
                        }

                        if (iMember2_index > -1) // Index was defined, we can break cycle
                            break;
                    }


                   // If relases exist, they are neccesary to define DOF of both members, therefore copy release of one member to the another one
                   if(m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode1.INode_ID && m_arrFemMembers[iMember1_index].CnRelease1 != null)
                   {
                          if(m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode1.INode_ID && m_arrFemMembers[iMember2_index].CnRelease1 == null)
                             m_arrFemMembers[iMember2_index].CnRelease1.m_bRestrain = m_arrFemMembers[iMember1_index].CnRelease1.m_bRestrain;
                         else if(m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode2.INode_ID && m_arrFemMembers[iMember2_index].CnRelease2 == null)
                             m_arrFemMembers[iMember2_index].CnRelease2.m_bRestrain = m_arrFemMembers[iMember1_index].CnRelease1.m_bRestrain;
                   }
                   else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode2.INode_ID && m_arrFemMembers[iMember1_index].CnRelease2 != null)
                   {
                       if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode1.INode_ID && m_arrFemMembers[iMember2_index].CnRelease1 == null)
                           m_arrFemMembers[iMember2_index].CnRelease1.m_bRestrain = m_arrFemMembers[iMember1_index].CnRelease2.m_bRestrain;
                       else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode2.INode_ID && m_arrFemMembers[iMember2_index].CnRelease2 == null)
                           m_arrFemMembers[iMember2_index].CnRelease2.m_bRestrain = m_arrFemMembers[iMember1_index].CnRelease2.m_bRestrain;
                   }
                   else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode1.INode_ID && m_arrFemMembers[iMember2_index].CnRelease1 != null)
                   {
                       if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode1.INode_ID && m_arrFemMembers[iMember1_index].CnRelease1 == null)
                           m_arrFemMembers[iMember1_index].CnRelease1.m_bRestrain = m_arrFemMembers[iMember2_index].CnRelease1.m_bRestrain;
                       else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode2.INode_ID && m_arrFemMembers[iMember1_index].CnRelease2 == null)
                           m_arrFemMembers[iMember1_index].CnRelease2.m_bRestrain = m_arrFemMembers[iMember2_index].CnRelease1.m_bRestrain;
                   }
                   else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember2_index].INode2.INode_ID && m_arrFemMembers[iMember2_index].CnRelease2 != null)
                   {
                       if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode1.INode_ID && m_arrFemMembers[iMember1_index].CnRelease1 == null)
                           m_arrFemMembers[iMember1_index].CnRelease1.m_bRestrain = m_arrFemMembers[iMember2_index].CnRelease2.m_bRestrain;
                       else if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[iMember1_index].INode2.INode_ID && m_arrFemMembers[iMember1_index].CnRelease2 == null)
                           m_arrFemMembers[iMember1_index].CnRelease2.m_bRestrain = m_arrFemMembers[iMember2_index].CnRelease2.m_bRestrain;
                   }
                }
            }

            // Additional data of members
            // Fill Members stifeness matrices
            for (int i = 0; i < m_arrFemMembers.Length; i++)
            {
                m_arrFemMembers[i].FillBasic3_StiffMatrices();
            }



            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // External Loads
            // Fill vectors

            // Nodal loads
            // Fill vector of external load for each node in list
            // Node could be included in all lists only once, more than one nodal load in one node is not allowed
            for (int i = 0; i < TopoModel.m_arrNLoads.Length; i++) // Each load
            {
                if (TopoModel.m_arrNLoads[i].INodeCollection != null) // Check if some nodes are in list
                {
                    for (int j = 0; j < TopoModel.m_arrNLoads[i].INodeCollection.Length; j++) // Each node in collection
                    {
                        // Set load vector values
                        for (int k = 0; k < m_arrFemNodes.Length; k++) // Neefektivne prechadzat zbytocne vsetky uzly kedze pozname konkretne ID zatazenych
                        {
                            if (TopoModel.m_arrNLoads[i].INodeCollection.Contains(TopoModel.m_arrNodes[k].INode_ID)) // If node ID is same as collection item
                            {

                                m_arrFemNodes[k].m_ArrDirNodeLoad[(int)e2D_E_F.eFY] = TopoModel.m_arrNLoads[i].Value_FX;
                                m_arrFemNodes[k].m_ArrDirNodeLoad[(int)e2D_E_F.eFY] = TopoModel.m_arrNLoads[i].Value_FY;
                                m_arrFemNodes[k].m_ArrDirNodeLoad[(int)e2D_E_F.eMZ] = TopoModel.m_arrNLoads[i].Value_MZ;

                                //for (int l = 0; l < m_arrFemNodes[k].m_ArrDirNodeLoad.Length; l++) // For all items in vector array
                                //{
                                //    m_arrFemNodes[k].m_ArrDirNodeLoad[l] = TopoModel.m_arrNLoads[i].Value_FX; // General !!!
                                //}

                            }
                        }
                    }
                }
            }


            // Member loads

            // Summation of loads applied on one member 
            // There can by more loads on one member, member could be in various loads lists (but only once in one list)

            for (int i = 0; i < TopoModel.m_arrMLoads.Length; i++) // Each load
            {
                if (TopoModel.m_arrMLoads[i].IMemberCollection != null) // Check if some members are in list
                {
                    for (int j = 0; j < TopoModel.m_arrMLoads[i].IMemberCollection.Length; j++) // Each node in collection
                    {
                        // Set end forces due to member load 
                        for (int k = 0; k < m_arrFemMembers.Length; k++) // Neefektivne prechadzat zbytocne vsetky pruty kedze pozname konkretne ID zatazenych
                        {
                            if (TopoModel.m_arrMLoads[i].IMemberCollection.Contains(TopoModel.m_arrMembers[k].IMember_ID)) // If member ID is same as collection item
                            {

                                float fTemp_1 = 0f, fTemp_2 = 0f; // Auxialiary for not used components

                                // Fill external forces vectors 

                                switch (TopoModel.m_arrMLoads[i].EDirPPC) // Load direction in principal coordinate system XX / YU / ZV
                                {
                                    case EMLoadDirPCC1.eMLD_PCC_FXX_MXX: // Axial load on member
                                        {
                                            // DOF RX can't be released - always rigid
                                            switch (m_arrFemMembers[k].m_eSuppType[(int)EM_PCS_DIR1.eUXRX])
                                            {
                                                // Type of supports is already defined  but I check it once more in body of function !!!

                                                // XX - direction both sides supported displacement
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_00:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00_0_:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__00:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0__0_:
                                                    {

                                                        // Type ObjLoadType = typeof(TopoModel.m_arrMLoads[i]);


                                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[i],
                                                            m_arrFemMembers[k],
                                                            m_arrFemMembers[k].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                                            ref TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                                            ref TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                                            ref fTemp_1,
                                                            ref fTemp_2);
                                                        break;
                                                    }
                                                // XX - direction start supported, end free displacement
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00__0:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_00___:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl_0____:
                                                    {
                                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[i],
                                                        m_arrFemMembers[k],
                                                        m_arrFemMembers[k].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                                        ref TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                                        ref TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                                        ref fTemp_1,
                                                        ref fTemp_2);

                                                        break;
                                                    }
                                                // XX - direction start free displacement, end supported
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0_00:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____00:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl____0_:
                                                    {
                                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[i],
                                                m_arrFemMembers[k],
                                                m_arrFemMembers[k].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                                ref TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                                ref TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                                ref fTemp_1,
                                                ref fTemp_2);

                                                        break;
                                                    }
                                                // XX - direction start free displacement, end free displacement
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl__0__0:
                                                case FEM_CALC_BASE.Enums.EElemSuppType.eEl______:
                                                default:
                                                    {
                                                        FEM_CALC_BASE.CMLoadPart objMLoadPart = new FEM_CALC_BASE.CMLoadPart(TopoModel.m_arrMLoads[i],
                                                            m_arrFemMembers[k],
                                                            m_arrFemMembers[k].m_eSuppType[(int)EM_PCS_DIR1.eUXRX],
                                                            ref TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX],
                                                            ref TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX],
                                                            ref fTemp_1,
                                                            ref fTemp_2);

                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case EMLoadDirPCC1.eMLD_PCC_YU:
                                        {

                                            break;
                                        }
                                    case EMLoadDirPCC1.eMLD_PCC_ZV:
                                        {


                                            break;
                                        }
                                    default: // Exception
                                        {

                                            break;
                                        }
                                }




                                // Primary end forces due member loading in local coordinate system LCS

                                // Start Node
                                m_arrFemMembers[k].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eFX] += TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFX];
                                m_arrFemMembers[k].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eFY] += TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eFY];
                                m_arrFemMembers[k].m_VElemPEF_GCS_StNode.FVectorItems[(int)e2D_E_F.eMZ] += TopoModel.m_arrMLoads[i].V_EIF_MembStart.FVectorItems[(int)e2D_E_F.eMZ];

                                // End Node
                                m_arrFemMembers[k].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eFX] += TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFX];
                                m_arrFemMembers[k].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eFY] += TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eFY];
                                m_arrFemMembers[k].m_VElemPEF_GCS_EnNode.FVectorItems[(int)e2D_E_F.eMZ] += TopoModel.m_arrMLoads[i].V_EIF_MembEnd.FVectorItems[(int)e2D_E_F.eMZ];
                            }
                        }
                    }
                }
            }
        } // End of generate








    }
}
