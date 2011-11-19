﻿using System;
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
                for (int j = 0; j < m_arrFemMembers.Length; j++)
                {
                    if (m_arrFemNodes[i].INode_ID == m_arrFemMembers[j].m_NodeStart.INode_ID || m_arrFemNodes[i].INode_ID == m_arrFemMembers[j].m_NodeEnd.INode_ID) // Is node ID same as member start or end node ID
                    {
                        m_arrFemNodes[i].m_iMemberCollection = new System.Collections.ArrayList(); // Allocate collection memory
                        m_arrFemNodes[i].m_iMemberCollection.Add(m_arrFemMembers[j].MemberID); // Add FEMmember ID to the FEM node list
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
                        if (m_arrFemNodes[i].m_iMemberCollection.Contains(m_arrFemMembers[j].ID)) // if Member ID is in the list
                        {
                            iMember1_index = j; // Set 1st

                            // 2nd member index
                            for (int k = m_arrFemMembers.Length - j; k < m_arrFemMembers.Length; k++) // Search for second
                            {
                                if (m_arrFemNodes[i].m_iMemberCollection.Contains(m_arrFemMembers[k].ID))
                                    iMember2_index = j;
                            }
                        }
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
        }
    }
}
