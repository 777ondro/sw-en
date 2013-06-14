﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CENEX;

namespace FEM_CALC_1Din3D
{
    class CGenex
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
                        if (m_arrFemNodes[i].ID == TopoModel.m_arrNSupports[i2].m_iNodeCollection[j])
                        {
                            // DOF of nodes are free - zero rigidity of restraints  false as default
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eUX] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eUX]; // !!! 3D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eUY] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eUY]; // !!! 3D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eUZ] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eUZ]; // !!! 3D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eRX] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eRX]; // !!! 3D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eRY] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eRY]; // !!! 3D Environment enum
                            m_arrFemNodes[i].m_ArrNodeDOF[(int)e3D_DOF.eRZ] = TopoModel.m_arrNSupports[i2].m_bRestrain[(int)e3D_DOF.eRZ]; // !!! 3D Environment enum
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
                            if (TopoModel.m_arrNReleases[j].m_iMembCollection[k] == m_arrFemMembers[i].ID) // if release exists on member
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
                    if (m_arrFemNodes[i].ID == m_arrFemMembers[j].NodeStart.ID || m_arrFemNodes[i].ID == m_arrFemMembers[j].NodeEnd.ID) // Is node ID same as member start or end node ID
                    {
                        m_arrFemNodes[i].m_iMemberCollection.Add(m_arrFemMembers[j].ID); // Add FEMmember ID to the FEM node list
                    }
                }
            }


            // Releases !!!! - see 2D solution


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
            if (!object.ReferenceEquals(null, TopoModel.m_arrNLoads))
            {
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

                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eFY] = TopoModel.m_arrNLoads[i].Value_FX;
                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eFY] = TopoModel.m_arrNLoads[i].Value_FY;
                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eFZ] = TopoModel.m_arrNLoads[i].Value_FZ;
                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eMX] = TopoModel.m_arrNLoads[i].Value_MX;
                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eMY] = TopoModel.m_arrNLoads[i].Value_MY;
                                    m_arrFemNodes[k].m_VDirNodeLoad.FVectorItems[(int)e3D_E_F.eMZ] = TopoModel.m_arrNLoads[i].Value_MZ;
                                }
                            }
                        }
                    }
                }
            }

            // Member loads

            // Set primary end forces only due to member loads in local coordinate system LCS
            // Summation of loads applied on one member 
            // There can by more loads on one member, member could be in various loads lists (but only once in one list)

            if (!object.ReferenceEquals(null, TopoModel.m_arrMLoads))
            {
                for (int i = 0; i < TopoModel.m_arrMLoads.Length; i++) // Each load
                {
                    if (TopoModel.m_arrMLoads[i].IMemberCollection != null) // Check if some members are in list
                    {
                        for (int j = 0; j < TopoModel.m_arrMLoads[i].IMemberCollection.Length; j++) // Each node in collection
                        {
                            // Set end forces due to member load
                            for (int k = 0; k < m_arrFemMembers.Length; k++) // Neefektivne prechadzat zbytocne vsetky pruty kedze pozname konkretne ID zatazenych
                            {
                                // Temporary value of end forces due to particular external force
                                float fTemp_A_UXRX = 0f, fTemp_B_UXRX = 0f, fTemp_Ma_UXRX = 0f, fTemp_Mb_UXRX = 0f;
                                float fTemp_A_UYRZ = 0f, fTemp_B_UYRZ = 0f, fTemp_Ma_UYRZ = 0f, fTemp_Mb_UYRZ = 0f;
                                float fTemp_A_UZRY = 0f, fTemp_B_UZRY = 0f, fTemp_Ma_UZRY = 0f, fTemp_Mb_UZRY = 0f;

                                // Fill end forces due to external forces vectors for member particular load index i and member index k
                                // Depends on load dirrection and member support type
                                // Use member PRINCIPAL coordinate system
                                CMLoadPart objAux = new CMLoadPart(TopoModel, m_arrFemMembers, i, k,
                                    out fTemp_A_UXRX, out fTemp_Ma_UXRX, out fTemp_A_UYRZ, out fTemp_Ma_UYRZ, out fTemp_A_UZRY, out fTemp_Ma_UZRY,
                                    out fTemp_B_UXRX, out fTemp_Ma_UXRX, out fTemp_B_UYRZ, out fTemp_Mb_UYRZ,out fTemp_B_UZRY, out fTemp_Mb_UZRY
                                );

                                // Add values of temperary end forces due to particular load to the end forces items of vector
                                // Primary end forces due member loading in local coordinate system LCS

                                // Start Node
                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eFX] += fTemp_A_UXRX;
                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eMX] += fTemp_Ma_UXRX;

                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eFY] += fTemp_A_UYRZ; // !!! Signs - nutne skontrolovat znamienka podla smeru lokalnzch osi a orientacie zatazenia
                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eMZ] += fTemp_Ma_UYRZ;

                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eFZ] += fTemp_A_UZRY; // !!! Signs - nutne skontrolovat znamienka podla smeru lokalnzch osi a orientacie zatazenia
                                m_arrFemMembers[k].m_VElemPEF_LCS_StNode.FVectorItems[(int)e3D_E_F.eMY] += fTemp_Ma_UZRY;

                                // End Node
                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eFX] += fTemp_B_UXRX;
                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eMX] += fTemp_Ma_UXRX;

                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eFY] += -fTemp_B_UYRZ;  // Zmena znamienka pre silu Vb na konci pruta, znamienko je opacne nez u reakcie
                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eMZ] += fTemp_Mb_UYRZ;

                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eFZ] += -fTemp_B_UZRY;  // Zmena znamienka pre silu Vb na konci pruta, znamienko je opacne nez u reakcie
                                m_arrFemMembers[k].m_VElemPEF_LCS_EnNode.FVectorItems[(int)e3D_E_F.eMY] += fTemp_Mb_UZRY;
                            }
                        }
                    }
                }
            }


            // Set primary end forces only due to member loads in global coordinate system GCS

            foreach (CE_1D Elem in m_arrFemMembers)
            {
                Elem.SetGetPEF_GCS();
            }


        } // End of generate
    }
}
