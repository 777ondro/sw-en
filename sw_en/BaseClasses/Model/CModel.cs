using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClasses;
using CRSC;

namespace CENEX
{
    // Main model class

    // List of model objects is included

    [Serializable]
    public class CModel
    {
        // General project data

        public string m_sProjectName;
        public string m_sConstObjectName;
        public string m_sFileName;

        // Physical Model / Structural data

        // Materials used/defined in current model
        public CMat_00[] m_arrMat = new CMat_00[1];
        // Cross-sections used/ defined in current model
        public CCrSc[] m_arrCrSc = new CCrSc[1];

        // Topological nodes (not FEM)
        public CNode[] m_arrNodes = new CNode[1];
        // 1D Elements (not FEM)
        public CMember [] m_arrMembers = new CMember[1];
        // Supports
        public CNSupport[] m_arrNSupports = new CNSupport[1];
        // Loads
        public CNLoad[] m_arrNForces = new CNLoad[1];

        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        //----------------------------------------------------------------------------
        public CModel() { }
        public CModel(string sFileName)
        {
            m_sFileName = sFileName;
        }
        public CModel(string sProjectName, string sConstObjectName, string sFileName)
        {
            m_sProjectName = sProjectName;
            m_sConstObjectName = sConstObjectName;
            m_sFileName = sFileName;
        }

    }
}
