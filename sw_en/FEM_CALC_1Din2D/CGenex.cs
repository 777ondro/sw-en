using System;
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
        
        // Temporary - to fill genex data
        // Same as topological data

            // Nodes
            for (int i = 0; i < TopoModel.m_arrMembers.Length; i++)
                m_arrFemNodes[i] = (CFemNode)TopoModel.m_arrNodes[i];

            // Members
            for (int i = 0; i < TopoModel.m_arrMembers.Length; i++)
                m_arrFemMembers[i] = (CE_1D)TopoModel.m_arrMembers[i];
        

        
        }










    }
}
