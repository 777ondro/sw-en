using System;
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













        }
    }
}
