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
        // Collection of references to objects

        // Materials used/defined in current model
        public CMat_00[] m_arrMat = new CMat_00[1];
        // Cross-sections used/ defined in current model
        public CCrSc[] m_arrCrSc = new CCrSc[1];

        // Topological nodes (not FEM)
        // Note !!!
        // Type of object collections - some dynamically allocated which can be resized - stack, queue, vector ????

        public CNode[] m_arrNodes = new CNode[1];
        // 1D Elements (not FEM)
        public CMember [] m_arrMembers = new CMember[1];
        // Nodal Supports
        public CNSupport[] m_arrNSupports = new CNSupport[1];
        // Nodal Loads
        public CNLoad[] m_arrNLoads = new CNLoad[1];
        // Member Loads
        public CMLoad[] m_arrMLoads = new CMLoad[1];

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
